using System;
using WIA;

namespace DNTScanner.Core
{
    /// <summary>
    /// IProperty utils.
    /// </summary>
    public static class IPropertyExtensions
    {
        /// <summary>
        /// Converts the properties and values of IProperty to a friendly string.
        /// </summary>
        public static string IPropertyToString(this IProperty property)
        {
            var result = $"{property.Name}(Id={property.PropertyID}) = ";
            if (property.IsVector)
            {
                result += " [vector of data] ";
            }
            else
            {
                result += property.get_Value();
                if (property.SubType != WiaSubType.UnspecifiedSubType)
                {
                    result += $" (Default = {property.SubTypeDefault}) ";
                }
            }

            if (property.IsReadOnly)
            {
                result += " [READ ONLY] ";
            }

            switch (property.SubType)
            {
                case WiaSubType.FlagSubType:
                    result += " [ valid flags include:";
                    for (var i = 1; i <= property.SubTypeValues.Count; i++)
                    {
                        result += property.SubTypeValues.get_Item(i);
                        if (i != property.SubTypeValues.Count)
                        {
                            result += ", ";
                        }
                    }
                    result += " ]";
                    break;
                case WiaSubType.ListSubType:
                    result += " [ valid values include:";
                    for (var i = 1; i <= property.SubTypeValues.Count; i++)
                    {
                        result += property.SubTypeValues.get_Item(i);
                        if (i != property.SubTypeValues.Count)
                        {
                            result += ", ";
                        }
                    }
                    result += " ]";
                    break;
                case WiaSubType.RangeSubType:
                    result += $" [ valid values in the range from {property.SubTypeMin} to {property.SubTypeMax} in increments of {property.SubTypeStep} ]";
                    break;
            }

            return result;
        }

        /// <summary>
        /// Calculates an scaled WiaSubType.RangeSubType property value.
        /// </summary>
        public static int GetRangeSubTypeValue(this IProperty property)
        {
            if (property.SubType != WiaSubType.RangeSubType)
            {
                throw new ArgumentException($"Type of {nameof(property)} is not RangeSubType.");
            }

            int min = property.SubTypeMin;
            int max = property.SubTypeMax;
            int center = (max + min) / 2;
            int delta = max - center;
            return Convert.ToInt32(Math.Round(((int)property.get_Value() - center) / (double)delta * 100, 0));
        }

        /// <summary>
        /// Calculates an scaled WiaSubType.RangeSubType value.
        /// </summary>
        public static int GetAdjustedRangeSubTypeValue(this IProperty property, int value)
        {
            if (property.SubType != WiaSubType.RangeSubType)
            {
                throw new ArgumentException($"Type of {nameof(property)} is not a RangeSubType.");
            }

            var min = property.SubTypeMin;
            var max = property.SubTypeMax;
            var center = (max + min) / 2;
            var delta = max - center;
            return Convert.ToInt32(Math.Round(value / (double)100 * delta + center, 0));
        }
    }
}