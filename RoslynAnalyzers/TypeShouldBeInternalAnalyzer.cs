namespace RoslynAnalyzers
{
    using System.Collections.Immutable;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Diagnostics;

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class TypeShouldBeInternalAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "Scratch01";

        public static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: DiagnosticId,
            title: "Type should be internal.",
            messageFormat: "Type should be internal.",
            category: "OCD",
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: "Type should be internal.");

        /// <inheritdoc/>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
            ImmutableArray.Create(Descriptor);

        /// <inheritdoc/>
        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(Handle, SyntaxKind.ClassDeclaration, SyntaxKind.StructDeclaration);
        }

        private static void Handle(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is TypeDeclarationSyntax typeDeclaration &&
                context.ContainingSymbol is ITypeSymbol)
            {
                foreach (var modifier in typeDeclaration.Modifiers)
                {
                    if (modifier.IsKind(SyntaxKind.PublicKeyword))
                    {
                        context.ReportDiagnostic(Diagnostic.Create(Descriptor, modifier.GetLocation()));

                    }
                }
            }
        }
    }
}
