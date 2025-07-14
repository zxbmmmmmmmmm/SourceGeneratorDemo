using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SourceGeneratorDemo.Generator;

[Generator]
public class SyntaxTreeGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => 
            ctx.AddSource("HelloWorldSyntaxTree.g.cs",BuildSyntaxTree().GetText()));
    }
    private SyntaxTree BuildSyntaxTree()
    {
        var complicationUnit = CompilationUnit()
            .AddUsings(
                UsingDirective(
                    IdentifierName("System")))
            .AddMembers(
                FileScopedNamespaceDeclaration(
                    IdentifierName("SourceGeneratorDemo.Generator")))
            .AddMembers(
                ClassDeclaration("HelloWorldSyntaxTree")
                    .AddModifiers(
                        Token(SyntaxKind.PublicKeyword))
                    .AddMembers(
                        MethodDeclaration(
                                PredefinedType(
                                    Token(SyntaxKind.VoidKeyword)),
                                Identifier("SayHello"))
                            .WithModifiers(
                                TokenList(
                                    Token(SyntaxKind.PublicKeyword),
                                    Token(SyntaxKind.StaticKeyword)))
                            .WithBody(
                                Block(
                                    ExpressionStatement(
                                        InvocationExpression(
                                                MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                    IdentifierName("Console"),
                                                    IdentifierName("WriteLine")))
                                            .WithArgumentList(
                                                ArgumentList(
                                                    SingletonSeparatedList(
                                                        Argument(
                                                            LiteralExpression(
                                                                SyntaxKind.StringLiteralExpression,
                                                                Literal("Hello World from SyntaxTree!")))))))))));
        return SyntaxTree(complicationUnit.NormalizeWhitespace(),encoding:Encoding.UTF8);
    }
}