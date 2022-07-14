using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;

namespace AnalyzerTemplate
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AnalyzerTryOutCodeFixProvider)), Shared]
    public class AnalyzerTryOutCodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(AnalyzerTryOutAnalyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            var diagnostic = context.Diagnostics.First();
            
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<MethodDeclarationSyntax>().First();
    
            context.RegisterCodeFix(
                CodeAction.Create(
                    title: CodeFixResources.CodeFixTitle,
                    createChangedDocument: c => MakeBoolTryMethod(context.Document, declaration, c),
                    equivalenceKey: nameof(CodeFixResources.CodeFixTitle)),
                diagnostic);
        }

        private async Task<Document> MakeBoolTryMethod(Document document, MethodDeclarationSyntax statementSyntax,
            CancellationToken cancellationToken)
        {
            var tree = await document.GetSyntaxTreeAsync(cancellationToken).ConfigureAwait(false);
            var root = await tree.GetRootAsync(cancellationToken) as CompilationUnitSyntax;
            var metStatements = statementSyntax.Body.Statements;
            var body = new StatementSyntax[metStatements.Count+1];
            int i = 0;
            foreach (var statement in metStatements)
            {
                if (statement is IfStatementSyntax || statement is ReturnStatementSyntax)
                {
                    if (statement is IfStatementSyntax)
                    {
                        var ifStatement = statement as IfStatementSyntax;
                        var ifStatementBody = ifStatement.Statement as BlockSyntax;
                        var returnEl = ifStatementBody.Statements[0] as ReturnStatementSyntax;
                        var bodyEl = SyntaxFactory.ExpressionStatement(
                            SyntaxFactory.AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                SyntaxFactory.IdentifierName("res"),
                                SyntaxFactory.IdentifierName(returnEl.Expression.ToString())));
                        var boolReturn = SyntaxFactory.ReturnStatement(
                            SyntaxFactory.LiteralExpression(
                                SyntaxKind.TrueLiteralExpression));

                        body[i] = SyntaxFactory.IfStatement(ifStatement.Condition, SyntaxFactory.Block(bodyEl, boolReturn));
                    }
                    else
                    {
                        var returnEl = statement as ReturnStatementSyntax;
                        body[i] = SyntaxFactory.ExpressionStatement(
                            SyntaxFactory.AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                SyntaxFactory.IdentifierName("res"),
                                SyntaxFactory.IdentifierName(returnEl.Expression.ToString())));
                        ++i;
                        body[i] = SyntaxFactory.ReturnStatement(
                            SyntaxFactory.LiteralExpression(
                                SyntaxKind.TrueLiteralExpression));
                    }
                }
                else
                {
                    body[i] = statement;
                }

                ++i;
            }

            var parameters = new ParameterSyntax[statementSyntax.ParameterList.Parameters.Count+1];
            int ind = 0;
            foreach (var param in statementSyntax.ParameterList.Parameters)
            {
                parameters[ind] = param;
                ++ind;
            }
            parameters[ind] = SyntaxFactory.Parameter(
                    SyntaxFactory.Identifier("res"))
                .WithModifiers(
                    SyntaxFactory.TokenList(
                        SyntaxFactory.Token(SyntaxKind.OutKeyword)))
                .WithType(SyntaxFactory.IdentifierName(statementSyntax.ReturnType.ToString()));
            var newMet = SyntaxFactory.MethodDeclaration(SyntaxFactory.PredefinedType(
                        SyntaxFactory.Token(SyntaxKind.BoolKeyword)),
                    SyntaxFactory.Identifier(statementSyntax.Identifier.ToString()))
                .WithModifiers(
                    SyntaxFactory.TokenList(
                        SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                .AddParameterListParameters(parameters)
                .WithBody(SyntaxFactory.Block(body));
            root = root.ReplaceNode(statementSyntax, newMet);
            return document.WithSyntaxRoot(root);
        }
    }
}
