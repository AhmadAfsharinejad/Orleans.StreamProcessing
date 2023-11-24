namespace Workflow.Domain;
#pragma warning disable CS8603
#pragma warning disable CS8765
#pragma warning disable CS8604
#pragma warning disable CS8618

[Immutable, GenerateSerializer]
[Newtonsoft.Json.JsonConverter(typeof(PortIdNewtonsoftJsonConverter))]
[System.Text.Json.Serialization.JsonConverter(typeof(PortIdSystemTextJsonConverter))]
[System.ComponentModel.TypeConverter(typeof(PortIdTypeConverter))]
public record struct PortId
{
    [Id(0)] public string Value { get; }

    public PortId()
    {
    }

    public PortId(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public static implicit operator string(PortId id) => id.Value;
    public static explicit operator PortId(string id) => new(id);
    
    class PortIdNewtonsoftJsonConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(PortId);
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var id = (PortId)value;
            serializer.Serialize(writer, id.Value);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            return new PortId(serializer.Deserialize<string>(reader));
        }
    }
    
    class PortIdTypeConverter : System.ComponentModel.TypeConverter
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
                return new PortId(stringValue);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
        }

        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value is PortId idValue)
            {
                if (destinationType == typeof(string))
                {
                    return idValue.Value;
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
    
    class PortIdSystemTextJsonConverter : System.Text.Json.Serialization.JsonConverter<PortId>
    {
        public override PortId Read(ref System.Text.Json.Utf8JsonReader reader, Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
        {
            return new PortId(reader.GetString());
        }

        public override void Write(System.Text.Json.Utf8JsonWriter writer, PortId value, System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}