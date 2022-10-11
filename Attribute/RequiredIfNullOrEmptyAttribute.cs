using System.ComponentModel.DataAnnotations;
using System.Reflection;

// Define our namespace
namespace SyncStream.Documentation.Attribute;

/// <summary>
///     This class maintains our conditional requirement attribute
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
public class RequiredIfNullOrEmptyAttribute : ValidationAttribute
{
    /// <summary>
    ///     This property contains the description of the requirement for documentation
    /// </summary>
    public readonly string Description;

    /// <summary>
    ///     This property contains a custom error message for the rule
    /// </summary>
    public new readonly string ErrorMessage;

    /// <summary>
    ///     This property contains the name of the property whose value must be analyzed for requirement
    /// </summary>
    public readonly string PropertyName;

    /// <summary>
    ///     This method instantiates the attribute with a property name
    /// </summary>
    /// <param name="propertyName">The property whose value must be analyzed for requirement</param>
    /// <param name="description">The description of the requirement for documentation</param>
    /// <param name="errorMessage">A custom error message to display</param>
    public RequiredIfNullOrEmptyAttribute(string propertyName, string description = null, string errorMessage = null)
    {
        // Set the description into the instance
        Description = description;

        // Set the error message into the instance
        ErrorMessage = errorMessage;

        // Set the property name into the instance
        PropertyName = propertyName;
    }

    /// <summary>
    ///     This method validates the member according to the rules of this attribute
    /// </summary>
    /// <param name="value">The value to to validate</param>
    /// <param name="validationContext">The current validation context</param>
    /// <returns>The ValidationResult of the validation</returns>
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        // Localize the object instance
        object instance = validationContext.ObjectInstance;

        // Localize the property
        PropertyInfo property = instance.GetType().GetProperty(PropertyName);

        // Make sure we have a property
        if (property == null) return new($"{PropertyName} is not defined on the type");

        // Localize the value
        string localValue = property.GetValue(instance)?.ToString();

        // Check the values and fail the validation if they're no good
        if ((value is null || string.IsNullOrEmpty(value.ToString()) ||
             string.IsNullOrWhiteSpace(value.ToString())) &&
            (localValue == null || string.IsNullOrEmpty(localValue) || string.IsNullOrWhiteSpace(localValue)))
            return new ValidationResult(GenerateErrorMessage(property.Name));

        // We're done, we're valid
        return ValidationResult.Success;
    }

    /// <summary>
    ///     This method generates the description of the conditional requirement for the documentation
    /// </summary>
    /// <param name="propertyName">The property name to attach the description to</param>
    /// <param name="targetName">The documentation property name to attach the description to</param>
    /// <returns>The generated description for the rule</returns>
    public string GenerateDescription(string propertyName, string targetName = null) =>
        Description ??
        $"\n * `{propertyName}` is **required** when `{targetName ?? PropertyName}` is `null` or `String.Empty`";

    /// <summary>
    /// This method generates the error message for the conditional requirement
    /// </summary>
    /// <param name="propertyName">The property name to attach the error message to</param>
    /// <returns>The generated error message</returns>
    public string GenerateErrorMessage(string propertyName) =>
        ErrorMessage ?? $"{propertyName} is required when {PropertyName} is null or empty";
}
