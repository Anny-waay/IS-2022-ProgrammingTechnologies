using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace AnalyzerTemplate
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AnalyzerElseIfAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "AnalyzerTemplate";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeElseIf, SyntaxKind.IfStatement);
        }

        private void AnalyzeElseIf(SyntaxNodeAnalysisContext context)
        {
            var ifNode = (IfStatementSyntax) context.Node;
            try
            {
                var elseStatement = ifNode.Else.Statement as BlockSyntax;
                if (elseStatement.Statements.Count == 1 && elseStatement.Statements[0] is IfStatementSyntax)
                {
                    try
                    {
                        var newIf = elseStatement.Statements[0] as IfStatementSyntax;
                        var moreElse = newIf.Else.Statement;
                    }
                    catch (NullReferenceException)
                    {
                        var diagnostic = Diagnostic.Create(Rule, ifNode.GetLocation());
                        context.ReportDiagnostic(diagnostic); 
                    }
                }
            }
            catch(NullReferenceException)
            {
            }
        }
    }
}
