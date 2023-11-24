namespace Workflow.Domain;
#pragma warning disable CS8603
#pragma warning disable CS8765
#pragma warning disable CS8604
#pragma warning disable CS8618

[Immutable, GenerateSerializer]
[Newtonsoft.Json.JsonConverter(typeof(LinkIdNewtonsoftJsonConverter))]
[System.Text.Json.Serialization.JsonConverter(typeof(LinkIdSystemTextJsonConverter))]
[System.ComponentModel.TypeConverter(typeof(LinkIdTypeConverter))]
public record struct LinkId
{
    [Id(0)] public Guid Value { get; }

    public LinkId()
    {
    }

    public LinkId(Guid value)
    {
        Value = value;
    }

    public static implicit operator Guid(LinkId id) => id.Value;
    public static explicit operator LinkId(Guid id) => new(id);
    public static implicit operator string(LinkId id) => id.Value.ToString();
    public static explicit operator LinkId(string id) => new(Guid.Parse(id));


    class LinkIdTypeConverter : System.ComponentModel.TypeConverter
    {
        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(Guid) || sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return value switch
            {
                Guid guidValue => new LinkId(guidValue),
                string stringValue when !string.IsNullOrEmpty(stringValue) && Guid.TryParse(stringValue, out var result) => new LinkId(result),
                _ => base.ConvertFrom(context, culture, value),
            };
        }

        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(Guid) || sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
        }

        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value is LinkId idValue)
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

    class LinkIdNewtonsoftJsonConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LinkId);
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var id = (LinkId)value;
            serializer.Serialize(writer, id.Value);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var guid = serializer.Deserialize<Guid?>(reader);
            return guid.HasValue ? new LinkId(guid.Value) : null;
        }
    }

    class LinkIdSystemTextJsonConverter : System.Text.Json.Serialization.JsonConverter<LinkId>
    {
        public override LinkId Read(ref System.Text.Json.Utf8JsonReader reader, Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
        {
            return new LinkId(Guid.Parse(reader.GetString()));
        }

        public override void Write(System.Text.Json.Utf8JsonWriter writer, LinkId value, System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}