using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace NJsonSchema.CodeGeneration.CSharp.Tests
{
    public class SurveyJsTests
    {
        private readonly ITestOutputHelper output;
        public SurveyJsTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task SurveyJsModel()
        {
            var json = System.IO.File.ReadAllText("References/surveyjs.json");
            var schema = await JsonSchema.FromJsonAsync(json);

            //// Act
            var generator = new CSharpGenerator(schema, new CSharpGeneratorSettings
            {
                ClassStyle = CSharpClassStyle.Poco,
                SchemaType = SchemaType.Swagger2,
                DateType = "System.DateTime",
                Namespace = "SurveyJs"
            });
            var code = generator.GenerateFile("SurveyJs");
            output.WriteLine(code);
            System.IO.File.WriteAllText("Survey.cs",code);
            //Test Compile
            var type=AssertCompile(code);

            var q1 = File.ReadAllText("References/q1.json");

            var q1Survey=JsonConvert.DeserializeObject(q1, type);
        }

        private static Type AssertCompile(string code)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            var errors = syntaxTree
                .GetDiagnostics()
                .Where(_ => _.Severity == DiagnosticSeverity.Error);

            var assembly = GenerateAssembly(syntaxTree);

            return assembly.GetType("Survey.SurveyJs");
        }

        private static Assembly GenerateAssembly(SyntaxTree syntaxTree)
        {
            string assemblyName = Path.GetRandomFileName();
            var references = AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic)
                .Select(x => MetadataReference.CreateFromFile(x.Location));

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    var sb = new StringBuilder();
                    foreach (var e in failures)
                    {
                        sb.AppendLine($"{e.Id} at {e.Location}: {e.GetMessage()}");
                    }
                    Assert.Empty(sb.ToString());
                    return null;
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(ms.ToArray());
                    return assembly;
                }
            }
        }
    }
}
