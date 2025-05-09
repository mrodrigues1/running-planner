using System.ComponentModel;
using System.Reflection;

namespace RunningPlanner.Core.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Gets the description from the DescriptionAttribute of an enum value.
    /// If the attribute is not found or the description is empty, returns the enum value's name.
    /// </summary>
    /// <param name="value">The enum value.</param>
    /// <returns>The description string or the enum name.</returns>
    public static string GetDescription(this Enum value)
    {
        ArgumentNullException.ThrowIfNull(value);

        FieldInfo? fieldInfo = value.GetType().GetField(value.ToString());

        if (fieldInfo == null)
        {
            return value.ToString(); // Should not happen for valid enums
        }

        var attribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>(false);

        // Return the description if the attribute exists and has a non-empty value,
        // otherwise return the enum member's name.
        return attribute != null && !string.IsNullOrWhiteSpace(attribute.Description)
            ? attribute.Description
            : value.ToString();
    }
}
