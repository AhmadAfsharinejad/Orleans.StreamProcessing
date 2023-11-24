#pragma warning disable CS8618

#pragma warning disable CS8603
#pragma warning disable CS8765
#pragma warning disable CS8604
#pragma warning disable CS8618

namespace Workflow.Domain;

[Immutable, GenerateSerializer]
[Newtonsoft.Json.JsonConverter(typeof(WorkflowIdNewtonsoftJsonConverter))]
[System.Text.Json.Serialization.JsonConverter(typeof(WorkflowIdSystemTextJsonConverter))]
[System.ComponentModel.TypeConverter(typeof(WorkflowIdTypeConverter))]
public record struct WorkflowId
{
    //TODO [Id(0)] --> Write Source Generator To Create All Domain for Stream, so we dont need refrence directly to Workflow.Domain Or add Id Attribute in Workflow.Domain 
    //TOOD Figure out how to add Id attribute when using StronglyId 
    [Id(0)] public string Value { get; set; }

    public WorkflowId()
    {
    }

    public WorkflowId(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public WorkflowId(Guid id)
    {
        Value = id.ToString();
    }

    public static implicit operator string(WorkflowId id) => id.Value;
    public static explicit operator WorkflowId(string id) => new(id);
    public static implicit operator Guid(WorkflowId id) => Guid.Parse(id.Value);
    public static explicit operator WorkflowId(Guid id) => new(id.ToString());

    class WorkflowIdNewtonsoftJsonConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(WorkflowId);
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var id = (WorkflowId)value;
            serializer.Serialize(writer, id.Value);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            return new WorkflowId(serializer.Deserialize<string>(reader));
        }
    }

    class WorkflowIdTypeConverter : System.ComponentModel.TypeConverter
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
                return new WorkflowId(stringValue);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
        }

        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value is WorkflowId idValue)
            {
                if (destinationType == typeof(string))
                {
                    return idValue.Value;
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    class WorkflowIdSystemTextJsonConverter : System.Text.Json.Serialization.JsonConverter<WorkflowId>
    {
        public override WorkflowId Read(ref System.Text.Json.Utf8JsonReader reader, Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
        {
            return new WorkflowId(reader.GetString());
        }

        public override void Write(System.Text.Json.Utf8JsonWriter writer, WorkflowId value, System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}