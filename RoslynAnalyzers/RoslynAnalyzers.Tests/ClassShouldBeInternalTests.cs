using Gu.Roslyn.Asserts;
using Microsoft.CodeAnalysis.Diagnostics;
using NUnit.Framework;

namespace RoslynAnalyzers.Tests
{
    internal class ClassShouldBeInternalTests
    {
        private static readonly DiagnosticAnalyzer Analyzer = new ClassShouldBeInternalAnalyzer();

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
