//-----------------------------------------------------------------------
// <copyright file="CSharpGeneratorSettings.cs" company="NJsonSchema">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/RicoSuter/NJsonSchema/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System.Reflection;

namespace NJsonSchema.CodeGeneration.Dart
{
    /// <summary>The generator settings.</summary>
    public class DartGeneratorSettings : CodeGeneratorSettingsBase
    {
        /// <summary>Initializes a new instance of the <see cref="DartGeneratorSettings"/> class.</summary>
        public DartGeneratorSettings()
        {
            AnyType = "Object";
            Library = "MyLibrary";

            DateType = "DateTime";
            DateTimeType = "DateTime";
            TimeType = "Duration";
            TimeSpanType = "Duration";

            ArrayType = "List";
            ArrayInstanceType = "List";
            ArrayBaseType = "List";

            DictionaryType = "Map";
            DictionaryInstanceType = "Map";
            DictionaryBaseType = "Map";

            ClassStyle = DartClassStyle.Poco;

            RequiredPropertiesMustBeDefined = true;
            GenerateDataAnnotations = false;
            TypeAccessModifier = "";
            PropertySetterAccessModifier = string.Empty;
            GenerateJsonMethods = false;
            EnforceFlagEnums = false;

            ValueGenerator = new DartValueGenerator(this);
            PropertyNameGenerator = new DartPropertyNameGenerator();
            TemplateFactory = new DefaultTemplateFactory(this, new Assembly[]
            {
                typeof(DartGeneratorSettings).GetTypeInfo().Assembly
            });

            InlineNamedArrays = false;
            InlineNamedTuples = false;
            InlineNamedDictionaries = false;
        }

        /// <summary>Gets or sets the Dart library of the generated types (default: MyLibrary).</summary>
        public string Library { get; set; }

        /// <summary>Gets or sets a value indicating whether a required property must be defined in JSON 
        /// (sets Required.Always when the property is required) (default: true).</summary>
        public bool RequiredPropertiesMustBeDefined { get; set; }

        /// <summary>Gets or sets a value indicating whether to generated data json_annotation attributes (default: false).</summary>
        public bool GenerateDataAnnotations { get; set; }

        /// <summary>Gets or sets the any type (default: "object").</summary>
        public string AnyType { get; set; }

        /// <summary>Gets or sets the date .NET type (default: 'DateTimeOffset').</summary>
        public string DateType { get; set; }

        /// <summary>Gets or sets the date time .NET type (default: 'DateTimeOffset').</summary>
        public string DateTimeType { get; set; }

        /// <summary>Gets or sets the time .NET type (default: 'TimeSpan').</summary>
        public string TimeType { get; set; }

        /// <summary>Gets or sets the time span .NET type (default: 'TimeSpan').</summary>
        public string TimeSpanType { get; set; }

        /// <summary>Gets or sets the generic array .NET type (default: 'ICollection').</summary>
        public string ArrayType { get; set; }

        /// <summary>Gets or sets the generic dictionary .NET type (default: 'IDictionary').</summary>
        public string DictionaryType { get; set; }

        /// <summary>Gets or sets the generic array .NET type which is used for ArrayType instances (default: 'Collection').</summary>
        public string ArrayInstanceType { get; set; }

        /// <summary>Gets or sets the generic dictionary .NET type which is used for DictionaryType instances (default: 'Dictionary').</summary>
        public string DictionaryInstanceType { get; set; }

        /// <summary>Gets or sets the generic array .NET type which is used as base class (default: 'Collection').</summary>
        public string ArrayBaseType { get; set; }

        /// <summary>Gets or sets the generic dictionary .NET type which is used as base class (default: 'Dictionary').</summary>
        public string DictionaryBaseType { get; set; }

        /// <summary>Gets or sets the Dart class style (default: 'Poco').</summary>
        public DartClassStyle ClassStyle { get; set; }

        /// <summary>Gets or sets the access modifier of generated classes and interfaces (default: 'public').</summary>
        public string TypeAccessModifier { get; set; }

        /// <summary>Gets the access modifier of property setters (default: '').</summary>
        public string PropertySetterAccessModifier { get; set; }

        /// <summary>Gets or sets the custom Json.NET converters (class names) which are registered for serialization and deserialization.</summary>
        public string[] JsonConverters { get; set; }

        /// <summary>Gets or sets a value indicating whether to remove the setter for non-nullable array properties (default: false).</summary>
        public bool GenerateImmutableArrayProperties { get; set; }

        /// <summary>Gets or sets a value indicating whether to remove the setter for non-nullable dictionary properties (default: false).</summary>
        public bool GenerateImmutableDictionaryProperties { get; set; }

        /// <summary>Gets or sets a value indicating whether to use preserve references handling (All) in the JSON serializer (default: false).</summary>
        public bool HandleReferences { get; set; }

        /// <summary>Gets or sets the name of a static method which is called to transform the JsonSerializerSettings used in the generated ToJson()/FromJson() methods (default: null).</summary>
        public string JsonSerializerSettingsTransformationMethod { get; set; }

        /// <summary>Gets or sets a value indicating whether to render ToJson() and FromJson() methods (default: true).</summary>
        public bool GenerateJsonMethods { get; set; }

        /// <summary>Gets or sets a value indicating whether enums should be always generated as bit flags (default: false).</summary>
        public bool EnforceFlagEnums { get; set; }

        /// <summary>Gets or sets a value indicating whether named/referenced dictionaries should be inlined or generated as class with dictionary inheritance.</summary>
        public bool InlineNamedDictionaries { get; set; }

        /// <summary>Gets or sets a value indicating whether named/referenced tuples should be inlined or generated as class with tuple inheritance.</summary>
        public bool InlineNamedTuples { get; set; }

        /// <summary>Gets or sets a value indicating whether named/referenced arrays should be inlined or generated as class with array inheritance.</summary>
        public bool InlineNamedArrays { get; set; }

        /// <summary>Gets or sets a value indicating whether optional schema properties (not required) are generated as nullable properties (default: false).</summary>
        public bool GenerateOptionalPropertiesAsNullable { get; set; }
    }
}
