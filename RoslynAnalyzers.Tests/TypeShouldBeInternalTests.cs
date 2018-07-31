namespace RoslynAnalyzers.Tests
{
    using Gu.Roslyn.Asserts;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.Diagnostics;
    using NUnit.Framework;

    internal class TypeShouldBeInternalTests
    {
        private static readonly DiagnosticAnalyzer Analyzer = new TypeShouldBeInternalAnalyzer();
        private static readonly CodeFixProvider Fix = new MakeTypeInternalFix();

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

        [Test]
        public void WhenPublicWithFix()
        {
            var code = @"
namespace RoslynSandbox
{
    ↓public class Foo
    {
    }
}";

            var fixedCode = @"
namespace RoslynSandbox
{
    internal class Foo
    {
    }
}";
            AnalyzerAssert.CodeFix(Analyzer, Fix, code, fixedCode);
        }
    }
}
