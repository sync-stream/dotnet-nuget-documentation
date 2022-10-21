// Define our namespace
namespace SyncStream.Documentation.Extensions;

/// <summary>
///     This class maintains the DocumentationLicense enum extensions
/// </summary>
public static class SyncStreamDocumentationLicenseExtensions
{
    /// <summary>
    ///     This method converts a documentation license to a URL
    /// </summary>
    /// <param name="value">The documentation license enum</param>
    /// <returns>The URL to the license</returns>
    public static Uri ToUrl(this Enum.DocumentationLicenseEnum value)
    {
        // Define our url response
        string baseUrl = "https://opensource.org/licences";

        // We're done, return our URL
        return value switch
        {

            // Return the Apache License 2.0 url-key
            Enum.DocumentationLicenseEnum.Apache20 => new($"{baseUrl}/Apache-2.0"),

            // Return the BSD 2-Clause "Simplified" or "FreeBSD" license url-key
            Enum.DocumentationLicenseEnum.Bsd2Clause => new($"{baseUrl}/BSD-2-Clause"),

            // Return the BSD 3-Clause "New" or "Revised" license url-key
            Enum.DocumentationLicenseEnum.Bsd3Clause => new($"{baseUrl}/BSD-3-Clause"),

            // Return the Common Development and Distribution License url-key
            Enum.DocumentationLicenseEnum.CDDL10 => new($"{baseUrl}/CDDL-1.0"),

            // Return the Eclipse Public License version 2.0 url-key
            Enum.DocumentationLicenseEnum.EPL20 => new($"{baseUrl}/EPL-2.0"),

            // Return the GNU General Public License (GPL) url-key
            Enum.DocumentationLicenseEnum.GPL => new($"{baseUrl}/gpl-license"),

            // Return the GNU Library or "Lesser" General Public License (LGPL) url-key
            Enum.DocumentationLicenseEnum.LGPL => new($"{baseUrl}/lgpl-license"),

            // Return the MIT license url-key
            Enum.DocumentationLicenseEnum.MIT => new($"{baseUrl}/MIT"),

            // Return the Mozilla Public License 2.0 url-key
            Enum.DocumentationLicenseEnum.MPL20 => new($"{baseUrl}/MPL-2.0"),

            // Default to the base license path
            _ => null
        };
    }
}
