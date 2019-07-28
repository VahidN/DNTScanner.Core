using System;
using System.Linq;
using System.Collections.Generic;
using WIA;
using System.Text;

namespace DNTScanner.Core
{
    /// <summary>
    /// WIA Settings Utils.
    /// </summary>
    public class WiaSettings
    {
        private readonly IDictionary<string, IProperty> _deviceNamedProperties = new Dictionary<string, IProperty>();
        private readonly IDictionary<int, IProperty> _deviceNumericProperties = new Dictionary<int, IProperty>();

        /// <summary>
        /// Returns a list of supported properties and their default values
        /// </summary>
        public IDictionary<string, object> ExtendedProperties =>
               _deviceNamedProperties.ToDictionary(t => t.Key, t => t.Value.get_Value());

        /// <summary>
        /// Returns a list of supported properties and their default values
        /// </summary>
        public string ScannersProperties { get; }

        /// <summary>
        /// WIA Settings Utils.
        /// </summary>
        public WiaSettings(IProperties properties)
        {
            var scannersProperties = new StringBuilder();
            for (var i = 1; i <= properties.Count; i++) // Using a regular for loop to avoid `System.IO.FileNotFoundException: Could not load file or assembly CustomMarshalers` in .NET Core 2x apps.
            {
                var property = properties[i];
                _deviceNamedProperties.Add(property.Name, property);
                _deviceNumericProperties.Add(property.PropertyID, property);
                scannersProperties.Append(property.Name).Append('(').Append(property.PropertyID).Append("), ");
            }
            ScannersProperties = scannersProperties.ToString().Trim().TrimEnd(',');
        }

        /// <summary>
        /// Sets a given property value.
        /// </summary>
        public void SetPropertyValue(string propertyName, object value)
        {
            _deviceNamedProperties.TryGetValue(propertyName, out IProperty property);
            if (property == null)
            {
                throw new NotSupportedException(
                    $"This scanner doesn't have `{propertyName}` property. List of all supported properties: {ScannersProperties}.");
            }
            SetPropertyValue(property, value);
        }

        /// <summary>
        /// Sets a given property value.
        /// </summary>
        public void SetPropertyValue(int propertyId, object value)
        {
            _deviceNumericProperties.TryGetValue(propertyId, out IProperty property);
            if (property == null)
            {
                throw new NotSupportedException(
                    $"This scanner doesn't have property ID = `{propertyId}`. List of all supported properties: {ScannersProperties}.");
            }
            SetPropertyValue(property, value);
        }

        /// <summary>
        /// Sets a given property value.
        /// </summary>
        public void SetRangeSubTypePropertyValue(int propertyId, int value)
        {
            _deviceNumericProperties.TryGetValue(propertyId, out IProperty property);
            if (property == null)
            {
                throw new NotSupportedException(
                    $"This scanner doesn't have property ID = `{propertyId}`. List of all supported properties: {ScannersProperties}.");
            }
            SetRangeSubTypePropertyValue(property, value);
        }

        /// <summary>
        /// Sets a given property value.
        /// </summary>
        public void SetRangeSubTypePropertyValue(string propertyName, int value)
        {
            _deviceNamedProperties.TryGetValue(propertyName, out IProperty property);
            if (property == null)
            {
                throw new NotSupportedException(
                    $"This scanner doesn't have `{propertyName}` property. List of all supported properties: {ScannersProperties}.");
            }
            SetRangeSubTypePropertyValue(property, value);
        }

        /// <summary>
        /// Sets a given property value.
        /// </summary>
        public void SetRangeSubTypePropertyValue(IProperty property, int value)
        {
            if (property.SubType != WiaSubType.RangeSubType)
            {
                SetPropertyValue(property, value);
                return;
            }

            var newValue = property.GetAdjustedRangeSubTypeValue(value);
            SetPropertyValue(property, newValue);
        }

        /// <summary>
        /// Sets a given property value.
        /// </summary>
        public void SetPropertyValue(IProperty property, object value)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            try
            {
                property.set_Value(ref value);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Can't set the provided `{value}` value for property `{property.Name}`. Property info: {property.IPropertyToString()}.", ex);
            }
        }

        /// <summary>
        /// Gets a given property value.
        /// </summary>
        public T GetPropertyValue<T>(string propertyName)
        {
            var property = GetProperty(propertyName);
            return (T)property.get_Value();
        }

        /// <summary>
        /// Gets a given property value.
        /// </summary>
        public T GetPropertyValue<T>(int propertyId)
        {
            var property = GetProperty(propertyId);
            return (T)property.get_Value();
        }

        /// <summary>
        /// Gets a given property.
        /// </summary>
        public IProperty GetProperty(string propertyName)
        {
            _deviceNamedProperties.TryGetValue(propertyName, out IProperty property);
            if (property != null)
            {
                return property;
            }
            throw new NotSupportedException(
                $"This scanner doesn't have `{propertyName}` property. List of all supported properties: {ScannersProperties}.");
        }

        /// <summary>
        /// Gets a given property.
        /// </summary>
        public IProperty GetProperty(int propertyId)
        {
            _deviceNumericProperties.TryGetValue(propertyId, out IProperty property);
            if (property != null)
            {
                return property;
            }
            throw new NotSupportedException(
                $"This scanner doesn't have property ID = `{propertyId}`. List of all supported properties: {ScannersProperties}.");
        }
    }
}