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
    public static Uri ToUrl(this DocumentationLicense value)
    {
        // Define our url response
        string baseUrl = "https://opensource.org/licences";

        // We're done, return our URL
        return value switch
        {

            // Return the Apache License 2.0 url-key
            DocumentationLicense.Apache20 => new($"{baseUrl}/Apache-2.0"),

            // Return the BSD 2-Clause "Simplified" or "FreeBSD" license url-key
            DocumentationLicense.Bsd2Clause => new($"{baseUrl}/BSD-2-Clause"),

            // Return the BSD 3-Clause "New" or "Revised" license url-key
            DocumentationLicense.Bsd3Clause => new($"{baseUrl}/BSD-3-Clause"),

            // Return the Common Development and Distribution License url-key
            DocumentationLicense.CDDL10 => new($"{baseUrl}/CDDL-1.0"),

            // Return the Eclipse Public License version 2.0 url-key
            DocumentationLicense.EPL20 => new($"{baseUrl}/EPL-2.0"),

            // Return the GNU General Public License (GPL) url-key
            DocumentationLicense.GPL => new($"{baseUrl}/gpl-license"),

            // Return the GNU Library or "Lesser" General Public License (LGPL) url-key
            DocumentationLicense.LGPL => new($"{baseUrl}/lgpl-license"),

            // Return the MIT license url-key
            DocumentationLicense.MIT => new($"{baseUrl}/MIT"),

            // Return the Mozilla Public License 2.0 url-key
            DocumentationLicense.MPL20 => new($"{baseUrl}/MPL-2.0"),

            // Default to the base license path
            _ => null
        };
    }
}
