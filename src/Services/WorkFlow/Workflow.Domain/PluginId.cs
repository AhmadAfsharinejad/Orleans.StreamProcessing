namespace Workflow.Domain;
#pragma warning disable CS8603
#pragma warning disable CS8765
#pragma warning disable CS8604
#pragma warning disable CS8618

[Immutable, GenerateSerializer]
[Newtonsoft.Json.JsonConverter(typeof(PluginIdNewtonsoftJsonConverter))]
[System.Text.Json.Serialization.JsonConverter(typeof(PluginIdSystemTextJsonConverter))]
[System.ComponentModel.TypeConverter(typeof(PluginIdTypeConverter))]
public record struct PluginId
{
    [Id(0)] public Guid Value { get; }

    public PluginId()
    {
    }

    public PluginId(Guid value)
    {
        Value = value;
    }

    public static implicit operator Guid(PluginId id) => id.Value;
    public static explicit operator PluginId(Guid id) => new(id);
    public static implicit operator string(PluginId id) => id.Value.ToString();
    public static explicit operator PluginId(string id) => new(Guid.Parse(id));

    class PluginIdTypeConverter : System.ComponentModel.TypeConverter
    {
        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(Guid) || sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return value switch
            {
                Guid guidValue => new PluginId(guidValue),
                string stringValue when !string.IsNullOrEmpty(stringValue) && Guid.TryParse(stringValue, out var result) => new PluginId(result),
                _ => base.ConvertFrom(context, culture, value),
            };
        }

        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(Guid) || sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
        }

        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value is PluginId idValue)
            {
                if (destinationType == typeof(Guid))
                {
                    return idValue.Value;
                }

                if (destinationType == typeof(string))
                {
                    return idValue.Value.ToString();
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    class PluginIdNewtonsoftJsonConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(PluginId);
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var id = (PluginId)value;
            serializer.Serialize(writer, id.Value);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var guid = serializer.Deserialize<Guid?>(reader);
            return guid.HasValue ? new PluginId(guid.Value) : null;
        }
    }

    class PluginIdSystemTextJsonConverter : System.Text.Json.Serialization.JsonConverter<PluginId>
    {
        public override PluginId Read(ref System.Text.Json.Utf8JsonReader reader, Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
        {
            return new PluginId(Guid.Parse(reader.GetString()));
        }

        public override void Write(System.Text.Json.Utf8JsonWriter writer, PluginId value, System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}