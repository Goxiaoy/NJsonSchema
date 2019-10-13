using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
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
                DateType = "System.DateTime"
            });
            var code = generator.GenerateFile("SurveyJs");
            output.WriteLine(code);
            System.IO.File.WriteAllText("Survey.cs",code);
            //Test Compile
            AssertCompile(code);
        }

        private static void AssertCompile(string code)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            var errors = syntaxTree
                .GetDiagnostics()
                .Where(_ => _.Severity == DiagnosticSeverity.Error);

            var sb = new StringBuilder();
            foreach (var e in errors)
            {
                sb.AppendLine($"{e.Id} at {e.Location}: {e.GetMessage()}");
            }

            Assert.Empty(sb.ToString());
        }
    }
}
