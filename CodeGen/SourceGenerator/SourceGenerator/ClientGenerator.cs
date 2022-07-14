using JavaParser;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SourceGenerator;

[Generator]
public class ClientGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
    }

    private StatementSyntax[] GenerateGetRequest(string url)
    {
        StatementSyntax[] getStatements = new StatementSyntax[6];
        getStatements[0] =
            SyntaxFactory.LocalDeclarationStatement(
                SyntaxFactory.VariableDeclaration(
                        SyntaxFactory.IdentifierName("HttpWebRequest"))
                    .AddVariables(
                        SyntaxFactory.VariableDeclarator(
                                SyntaxFactory.Identifier("req"))
                            .WithInitializer(
                                SyntaxFactory.EqualsValueClause(
                                    SyntaxFactory.InvocationExpression(
                                            SyntaxFactory.MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                SyntaxFactory.IdentifierName("HttpWebRequest"),
                                                SyntaxFactory.IdentifierName("CreateHttp")))
                                        .WithArgumentList(
                                            SyntaxFactory.ArgumentList(
                                                SyntaxFactory.SingletonSeparatedList(
                                                    SyntaxFactory.Argument(
                                                        SyntaxFactory.InterpolatedStringExpression(
                                                                SyntaxFactory.Token(SyntaxKind.InterpolatedStringStartToken))
                                                            .WithContents(
                                                                SyntaxFactory.List(
                                                                    new InterpolatedStringContentSyntax[]
                                                                    {
                                                                        SyntaxFactory.InterpolatedStringText()
                                                                            .WithTextToken(
                                                                                SyntaxFactory.Token(
                                                                                    SyntaxFactory.TriviaList(),
                                                                                    SyntaxKind
                                                                                        .InterpolatedStringTextToken,
                                                                                    url,
                                                                                    url,
                                                                                    SyntaxFactory.TriviaList()))
                                                                    }))))))))));
        getStatements[1] =
            SyntaxFactory.LocalDeclarationStatement(
                SyntaxFactory.VariableDeclaration(
                        SyntaxFactory.IdentifierName("Stream"))
                    .AddVariables(
                        SyntaxFactory.VariableDeclarator(
                                SyntaxFactory.Identifier("stream"))
                            .WithInitializer(
                                SyntaxFactory.EqualsValueClause(
                                    SyntaxFactory.InvocationExpression(
                                        SyntaxFactory.MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            SyntaxFactory.InvocationExpression(
                                                SyntaxFactory.MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                    SyntaxFactory.IdentifierName("req"),
                                                    SyntaxFactory.IdentifierName("GetResponse"))),
                                            SyntaxFactory.IdentifierName("GetResponseStream")))))));
        
        getStatements[2] =
            SyntaxFactory.LocalDeclarationStatement(
                SyntaxFactory.VariableDeclaration(
                        SyntaxFactory.IdentifierName("StreamReader"))
                    .AddVariables(
                        SyntaxFactory.VariableDeclarator(
                                SyntaxFactory.Identifier("streamReader"))
                            .WithInitializer(
                                SyntaxFactory.EqualsValueClause(
                                    SyntaxFactory.ObjectCreationExpression(
                                            SyntaxFactory.IdentifierName("StreamReader"))
                                        .WithArgumentList(
                                            SyntaxFactory.ArgumentList(
                                                SyntaxFactory.SingletonSeparatedList(
                                                    SyntaxFactory.Argument(
                                                        SyntaxFactory.IdentifierName("stream")))))))));
        
        getStatements[3] =
            SyntaxFactory.LocalDeclarationStatement(
                SyntaxFactory.VariableDeclaration(
                        SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.StringKeyword)))
                    .AddVariables(
                        SyntaxFactory.VariableDeclarator(
                                SyntaxFactory.Identifier("output"))
                            .WithInitializer(
                                SyntaxFactory.EqualsValueClause(
                                    SyntaxFactory.InvocationExpression(
                                        SyntaxFactory.MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            SyntaxFactory.IdentifierName("streamReader"),
                                            SyntaxFactory.IdentifierName("ReadToEnd")))))));
        
        getStatements[4] =
            SyntaxFactory.ExpressionStatement(
                SyntaxFactory.InvocationExpression(
                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("streamReader"),
                        SyntaxFactory.IdentifierName("Close"))));
        
        getStatements[5] =
            SyntaxFactory.ReturnStatement(
                SyntaxFactory.IdentifierName("output"));
        
        return getStatements;
    }

    private StatementSyntax[] GeneratePostRequest(string url, string argName)
    {
        StatementSyntax[] postStatements = new StatementSyntax[7];
        
        postStatements[0] =
            SyntaxFactory.LocalDeclarationStatement(
                SyntaxFactory.VariableDeclaration(
                        SyntaxFactory.PredefinedType(
                            SyntaxFactory.Token(SyntaxKind.StringKeyword)))
                    .WithVariables(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.VariableDeclarator(
                                    SyntaxFactory.Identifier("json"))
                                .WithInitializer(
                                    SyntaxFactory.EqualsValueClause(
                                        SyntaxFactory.InvocationExpression(
                                                SyntaxFactory.MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                    SyntaxFactory.IdentifierName("JsonSerializer"),
                                                    SyntaxFactory.IdentifierName("Serialize")))
                                            .WithArgumentList(
                                                SyntaxFactory.ArgumentList(
                                                    SyntaxFactory.SingletonSeparatedList(
                                                        SyntaxFactory.Argument(
                                                            SyntaxFactory.IdentifierName(argName))))))))));
        
        postStatements[1] =
            SyntaxFactory.LocalDeclarationStatement(
                SyntaxFactory.VariableDeclaration(
                        SyntaxFactory.IdentifierName("var"))
                    .WithVariables(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.VariableDeclarator(
                                    SyntaxFactory.Identifier("httpRequest"))
                                .WithInitializer(
                                    SyntaxFactory.EqualsValueClause(
                                        SyntaxFactory.InvocationExpression(
                                                SyntaxFactory.MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                    SyntaxFactory.IdentifierName("HttpWebRequest"),
                                                    SyntaxFactory.IdentifierName("Create")))
                                            .WithArgumentList(
                                                SyntaxFactory.ArgumentList(
                                                    SyntaxFactory.SingletonSeparatedList(
                                                        SyntaxFactory.Argument(
                                                            SyntaxFactory.LiteralExpression(
                                                                SyntaxKind.StringLiteralExpression,
                                                                SyntaxFactory.Literal(url)))))))))));
        
        postStatements[2] = SyntaxFactory.ExpressionStatement(
            SyntaxFactory.AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    SyntaxFactory.IdentifierName("httpRequest"),
                    SyntaxFactory.IdentifierName("Method")),
                SyntaxFactory.LiteralExpression(
                    SyntaxKind.StringLiteralExpression,
                    SyntaxFactory.Literal("POST"))));
        
        postStatements[3] = SyntaxFactory.ExpressionStatement(
            SyntaxFactory.AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    SyntaxFactory.IdentifierName("httpRequest"),
                    SyntaxFactory.IdentifierName("ContentType")),
                SyntaxFactory.LiteralExpression(
                    SyntaxKind.StringLiteralExpression,
                    SyntaxFactory.Literal("application/json"))));

        postStatements[4] = SyntaxFactory.UsingStatement(
                SyntaxFactory.UsingStatement(
                        SyntaxFactory.Block(
                            SyntaxFactory.SingletonList<StatementSyntax>(
                                SyntaxFactory.ExpressionStatement(
                                    SyntaxFactory.InvocationExpression(
                                            SyntaxFactory.MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                SyntaxFactory.IdentifierName("writer"),
                                                SyntaxFactory.IdentifierName("Write")))
                                        .WithArgumentList(
                                            SyntaxFactory.ArgumentList(
                                                SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
                                                    SyntaxFactory.Argument(
                                                        SyntaxFactory.IdentifierName("json")))))))))
                    .WithDeclaration(
                        SyntaxFactory.VariableDeclaration(
                                SyntaxFactory.IdentifierName("var"))
                            .WithVariables(
                                SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(
                                    SyntaxFactory.VariableDeclarator(
                                            SyntaxFactory.Identifier("writer"))
                                        .WithInitializer(
                                            SyntaxFactory.EqualsValueClause(
                                                SyntaxFactory.ObjectCreationExpression(
                                                        SyntaxFactory.IdentifierName("StreamWriter"))
                                                    .WithArgumentList(
                                                        SyntaxFactory.ArgumentList(
                                                            SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
                                                                SyntaxFactory.Argument(
                                                                    SyntaxFactory.IdentifierName("requestStream")))))))))))
            .WithDeclaration(
                SyntaxFactory.VariableDeclaration(
                        SyntaxFactory.IdentifierName("var"))
                    .WithVariables(
                        SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(
                            SyntaxFactory.VariableDeclarator(
                                    SyntaxFactory.Identifier("requestStream"))
                                .WithInitializer(
                                    SyntaxFactory.EqualsValueClause(
                                        SyntaxFactory.InvocationExpression(
                                            SyntaxFactory.MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                SyntaxFactory.IdentifierName("httpRequest"),
                                                SyntaxFactory.IdentifierName("GetRequestStream"))))))));
        postStatements[5] = SyntaxFactory.LocalDeclarationStatement(
            SyntaxFactory.VariableDeclaration(
                    SyntaxFactory.IdentifierName("var"))
                        .WithVariables(
                            SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(
                                SyntaxFactory.VariableDeclarator(
                                        SyntaxFactory.Identifier("reader"))
                                .WithInitializer(
                                    SyntaxFactory.EqualsValueClause(
                                        SyntaxFactory.ObjectCreationExpression(
                                                SyntaxFactory.IdentifierName("StreamReader"))
                                        .WithArgumentList(
                                            SyntaxFactory.ArgumentList(
                                                SyntaxFactory.SingletonSeparatedList(
                                                    SyntaxFactory.Argument(
                                                        SyntaxFactory.InvocationExpression(
                                                            SyntaxFactory.MemberAccessExpression(
                                                                SyntaxKind.SimpleMemberAccessExpression,
                                                                SyntaxFactory.InvocationExpression(
                                                                    SyntaxFactory.MemberAccessExpression(
                                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                                        SyntaxFactory.IdentifierName("httpRequest"),
                                                                        SyntaxFactory.IdentifierName("GetResponse"))),
                                                                SyntaxFactory.IdentifierName("GetResponseStream"))))))))))));
        postStatements[6] = SyntaxFactory.ExpressionStatement(
            SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    SyntaxFactory.IdentifierName("reader"),
                    SyntaxFactory.IdentifierName("ReadToEnd"))));
        return postStatements;
    }

    private List<MethodDeclaration> ParseAllMethods()
    {
        var parser = new Parser();
        List<MethodDeclaration> allMethods = new List<MethodDeclaration>();
        allMethods.AddRange(parser.ParseController("/Users/annakomova/Desktop/TechProg/lab-2/cats/src/main/java/com/lab2/cats/controllers/CatController.java"));
        allMethods.AddRange(parser.ParseController("/Users/annakomova/Desktop/TechProg/lab-2/cats/src/main/java/com/lab2/cats/controllers/OwnerController.java"));
        return allMethods;
    }
    
    public void Execute(GeneratorExecutionContext context)
    {
        var allMethods = ParseAllMethods();
        
        var system1 = SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("System.Net"));
        var system2 = SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("System.Text.Json"));
        var myUsing = SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("MyEntities"));
        var usingList = SyntaxFactory.List(new[] {system1, system2, myUsing});
        
        var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName("JavaWeb"));
        
        var webClass = SyntaxFactory.ClassDeclaration("GeneratedClient")
            .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)));

        MemberDeclarationSyntax[] methods = new MemberDeclarationSyntax[allMethods.Count];
        var i = 0;
        foreach (var method in allMethods)
        {

            var parameters = new ParameterSyntax[method.ArgList.Count];
            var url = method.Url;
            var argNum = 1;
            var j = 0;
            foreach (var arg in method.ArgList)
            {
                parameters[j]=SyntaxFactory.Parameter(SyntaxFactory.Identifier(arg.Name))
                     .WithType(SyntaxFactory.IdentifierName(arg.Type));
                if (method.HttpMethodName == "Get")
                {
                    if (argNum == 1)
                    {
                        url += $"?{arg.Name}={{{arg.Name}}}";
                    }
                    else
                    {
                        url += $"&{arg.Name}={{{arg.Name}}}";
                    }

                    ++argNum;
                    ++j;
                }
            }
            
            if (method.HttpMethodName == "Get")
            {
                var resultMethod = SyntaxFactory
                    .MethodDeclaration(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.StringKeyword)), method.MethodName)
                    .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                    .AddParameterListParameters(parameters)
                    .WithBody(
                        SyntaxFactory.Block(GenerateGetRequest(url)));
                methods[i]=resultMethod;
                ++i;
            }
            
            if (method.HttpMethodName == "Post")
            {
                var resultMethod = SyntaxFactory
                    .MethodDeclaration(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)), method.MethodName)
                    .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                    .AddParameterListParameters(parameters)
                    .WithBody(
                        SyntaxFactory.Block(GeneratePostRequest(url, method.ArgList[0].Name)));
                methods[i]=resultMethod;
                ++i;
            }
        }

        var compilationUnit = SyntaxFactory.CompilationUnit()
            .WithUsings(usingList)
            .AddMembers(namespaceDeclaration.AddMembers(webClass.AddMembers(methods)))
            .NormalizeWhitespace();    
        
        context.AddSource("GeneratedClient.cs",compilationUnit.ToString());
    }
}