namespace RoslynAnalyzers.Tests
{
    using Gu.Roslyn.Asserts;
    using Microsoft.CodeAnalysis.Diagnostics;
    using NUnit.Framework;

    internal class TypeShouldBeInternalTests
    {
        private static readonly DiagnosticAnalyzer Analyzer = new TypeSouldBeInternalAnalyzer();

        [Test]
        public void WhenInternal()
        {
            var code = @"
namespace RoslynSandbox
{
    internal class Foo
    {
    }
}";
            AnalyzerAssert.Valid(Analyzer, code);
        }

        [Test]
        public void WhenImplicitInternal()
        {
            var code = @"
namespace RoslynSandbox
{
    class Foo
    {
    }
}";
            AnalyzerAssert.Valid(Analyzer, code);
        }

        [Test]
        public void WhenPublic()
        {
            var code = @"
namespace RoslynSandbox
{
    ↓public class Foo
    {
    }
}";
            AnalyzerAssert.Diagnostics(Analyzer, code);
        }
    }
}
