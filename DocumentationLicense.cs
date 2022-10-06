using System.Text.Json.Serialization;

// Define our namespace
namespace  SyncStream.Documentation;

/// <summary>
///     This enum defines our license enum values
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DocumentationLicense
{
    /// <summary>
    ///     This enum value defines the Apache 2.0 License
    /// </summary>
    /// <returns></returns>
    Apache20,

    /// <summary>
    ///     This enum value defines the BSD 2-Clause "Simplified" or "FreeBSD" license
    /// </summary>
    Bsd2Clause,

    /// <summary>
    ///     This enum value defines the BSD 3-Clause "New" or "Revised" license
    /// </summary>
    Bsd3Clause,

    /// <summary>
    ///     This enum value defines the Common Development and Distribution License
    /// </summary>
    CDDL10,

    /// <summary>
    ///     This enum value defines the Eclipse Public License version 2.0 license
    /// </summary>
    EPL20,

    /// <summary>
    ///     This enum value defines the GNU General Public License (GPL) license
    /// </summary>
    GPL,

    /// <summary>
    ///     This enum value defines the GNU Library or "Lesser" General Public License (LGPL) license
    /// </summary>
    LGPL,

    /// <summary>
    ///     This enum value defines the MIT license
    /// </summary>
    MIT,

    /// <summary>
    ///     This enum value defines the Mozilla Public License 2.0 license
    /// </summary>
    MPL20,

    /// <summary>
    ///     This enum value defines a proprietary license
    /// </summary>
    Proprietary
}
