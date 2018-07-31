namespace RoslynAnalyzers
{
    using System.Collections.Immutable;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Diagnostics;

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class TypeSouldBeInternalAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "GU0072";

        public static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: DiagnosticId,
            title: "All types should be internal.",
            messageFormat: "All types should be internal.",
            category: "OCD",
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: "All types should be internal.");

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
