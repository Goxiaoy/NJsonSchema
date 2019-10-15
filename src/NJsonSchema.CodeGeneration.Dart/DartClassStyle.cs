//-----------------------------------------------------------------------
// <copyright file="CSharpClassStyle.cs" company="NJsonSchema">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/RicoSuter/NJsonSchema/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System.ComponentModel;

namespace NJsonSchema.CodeGeneration.Dart
{
    /// <summary>The CSharp styles.</summary>
    public enum DartClassStyle
    {
        /// <summary>Generates like POCOs (Plain Old C# Objects).</summary>
        Poco,

        /// <summary>Generates classes With ChangeNotifier</summary>
        Inpc,

        /// <summary>Generates classes implementing the Prism base class.</summary>
        Prism,

        /// <summary>Generates Records - like read only POCOs.</summary>
        Record
    }
}