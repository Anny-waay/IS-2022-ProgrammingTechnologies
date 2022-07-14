using JavaParser;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SourceGenerator;

[Generator]
public class EntityGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
    }

    private MemberDeclarationSyntax[] GenerateFields(List<ArgDeclaration> fields)
    {
        var resultFields = new MemberDeclarationSyntax[fields.Count];
        var i = 0;
        foreach (var field in fields)
        {
            resultFields[i] = 
                SyntaxFactory.PropertyDeclaration(
                        SyntaxFactory.IdentifierName(field.Type),
                        SyntaxFactory.Identifier(field.Name))
                    .WithModifiers(
                        SyntaxFactory.TokenList(
                            SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                    .AddAccessorListAccessors(
                        SyntaxFactory.AccessorDeclaration(
                                SyntaxKind.GetAccessorDeclaration)
                            .WithSemicolonToken(
                                SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                        SyntaxFactory.AccessorDeclaration(
                                SyntaxKind.SetAccessorDeclaration)
                            .WithSemicolonToken(
                                SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
            ++i;
        }

        return resultFields;
    }
    private List<EntityDeclaration> ParseEntities()
    {
        var parser = new Parser();
        var entities = new List<EntityDeclaration>();
        entities.Add(parser.ParseEntity(
            "/Users/annakomova/Desktop/TechProg/lab-2/cats/src/main/java/com/lab2/cats/entities/Cat.java"));
        entities.Add(parser.ParseEntity(
            "/Users/annakomova/Desktop/TechProg/lab-2/cats/src/main/java/com/lab2/cats/entities/Owner.java"));
        return entities;
    }
    public void Execute(GeneratorExecutionContext context)
    {
        var entities = ParseEntities();
        
        var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName("MyEntities"));

        foreach (var entity  in entities)
        {
            var entityClass = SyntaxFactory.ClassDeclaration(entity.EntityName)
                .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)));
            
            var fields = GenerateFields(entity.ArgList);
            
            var compilationUnit = SyntaxFactory.CompilationUnit()
                .AddMembers(namespaceDeclaration.AddMembers(entityClass.AddMembers(fields)))
                .NormalizeWhitespace();
            
            context.AddSource("Generated"+entity.EntityName + ".cs",compilationUnit.ToString());
        }
    }
}