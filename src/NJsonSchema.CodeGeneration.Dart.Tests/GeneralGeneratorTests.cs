﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NJsonSchema.CodeGeneration.Dart.Tests
{
    public class GeneralGeneratorTests
    {
        public class A
        {
            public string Foo { get; set; }
        }

        [Fact]
        public async Task When_setting_is_private()
        {
            // Arrange
            var schema = JsonSchema.FromType<A>();

            // Act
            var generator = new DartGenerator(schema, new DartGeneratorSettings() { IsPrivate = true});
            var code = generator.GenerateFile("A");
            // Assert
            Assert.Contains("class _A", code);
        }

        [Fact]
        public async Task When_classStyle_is_Inpc()
        {
            // Arrange
            var schema = JsonSchema.FromType<A>();

            // Act
            var generator = new DartGenerator(schema, new DartGeneratorSettings() {ClassStyle =DartClassStyle.Inpc });
            var code = generator.GenerateFile("A");
            // Assert
            Assert.Contains("class A with ChangeNotifier", code);
        }

    }
}
