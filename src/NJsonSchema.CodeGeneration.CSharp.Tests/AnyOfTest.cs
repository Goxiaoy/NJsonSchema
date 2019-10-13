using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NJsonSchema.Generation;
using Xunit;

namespace NJsonSchema.CodeGeneration.CSharp.Tests
{
    public class AnyOfTest
    {
        [Fact]
        public async Task When_schema_has_any_should_use_base()
        {
            //// Arrange
            //// Arrange
            var json =
                @"{
    ""$schema"": ""http://json-schema.org/draft-04/schema#"",
    ""title"": ""MySchema"",
    ""type"": ""object"",
    ""properties"": {
        ""B"": {
            ""$ref"": ""#/definitions/B""
        }
    },
    ""additionalProperties"": false,
    ""definitions"": {
        ""A"": {
            ""type"": ""array"",
            ""items"":{
                ""anyOf"":[
                    {
                        ""type"": ""number""
                    },
                    {
                        ""$ref"":""#/definitions/A""
                    }             
                ]
            } ,
            ""additionalItems"": false
        },
        ""B"": {
            ""type"": ""object"",
            ""allOf"":[
                {""$ref"":""#/definitions/A""},
                {
                    ""properties"": {
                        ""list"":{
                            ""type"":""array"",
                            ""items"":{
                                ""anyOf"":[
                                    {
                                        ""$ref"":""#/definitions/A""
                                    },
                                    {
                                        ""$ref"":""#/definitions/B""
                                    }             
                                ]
                            } 
                        }
                    }
                }
            ],
            ""additionalItems"": false
        }
    }
}";
            var schema = await JsonSchema.FromJsonAsync(json);

            //// Act
            var generator = new CSharpGenerator(schema, new CSharpGeneratorSettings
            {
                ClassStyle = CSharpClassStyle.Poco,
                SchemaType = SchemaType.Swagger2,
                DateType = "System.DateTime"
            });
            var code = generator.GenerateFile("MyClass");

            //// Assert
            Assert.Contains(@" System.Collections.Generic.ICollection<A> List", code);
        }
    }
}
