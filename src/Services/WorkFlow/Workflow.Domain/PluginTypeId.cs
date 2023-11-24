using Workflow.Domain.Plugins;
#pragma warning disable CS8603
#pragma warning disable CS8604
#pragma warning disable CS8765

#pragma warning disable CS8618

namespace Workflow.Domain;

[Immutable, GenerateSerializer]
[Newtonsoft.Json.JsonConverter(typeof(PluginTypeIdNewtonsoftJsonConverter))]
[System.Text.Json.Serialization.JsonConverter(typeof(PluginTypeIdSystemTextJsonConverter))]
[System.ComponentModel.TypeConverter(typeof(PluginTypeIdTypeConverter))]
public record struct PluginTypeId
{
    [Id(0)] public string Value { get; set; }

    public PluginTypeId()
    {
    }

    public PluginTypeId(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public PluginTypeId(PluginTypeNames pluginTypeName)
    {
        Value = pluginTypeName.ToString();
    }

    public static implicit operator string(PluginTypeId id) => id.Value;
    public static explicit operator PluginTypeId(string id) => new(id);

    class PluginTypeIdNewtonsoftJsonConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(PluginTypeId);
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var id = (PluginTypeId)value;
            serializer.Serialize(writer, id.Value);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            return new PluginTypeId(serializer.Deserialize<string>(reader));
        }
    }
    
    class PluginTypeIdTypeConverter : System.ComponentModel.TypeConverter
    {
        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            var stringValue = value as string;
            if (stringValue is not null)
            {
                return new PluginTypeId(stringValue);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
        }

        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value is PluginTypeId idValue)
            {
                if (destinationType == typeof(string))
                {
                    return idValue.Value;
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
    
    class PluginTypeIdSystemTextJsonConverter : System.Text.Json.Serialization.JsonConverter<PluginTypeId>
    {
        public override PluginTypeId Read(ref System.Text.Json.Utf8JsonReader reader, Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
        {
            return new PluginTypeId(reader.GetString());
        }

        public override void Write(System.Text.Json.Utf8JsonWriter writer, PluginTypeId value, System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}