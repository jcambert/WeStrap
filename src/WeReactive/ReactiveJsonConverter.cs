using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeReactive
{
    public class ReactiveJsonConverter<T> : JsonConverter<ReactiveProperty<T>>
    {
        public override ReactiveProperty<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, ReactiveProperty<T> value, JsonSerializerOptions options)
        {
            if (value.Value is int)
                writer.WriteNumberValue(Convert.ToInt32(value.Value));
            else if (value.Value is uint)
                writer.WriteNumberValue(Convert.ToUInt32(value.Value));
            else if (value.Value is double)
                writer.WriteNumberValue(Convert.ToDouble(value.Value));
            else if (value.Value is ulong)
                writer.WriteNumberValue(Convert.ToUInt64(value.Value));
            else if (value.Value is float)
                writer.WriteNumberValue(Convert.ToSingle(value.Value));
            else if (value.Value is long)
                writer.WriteNumberValue(Convert.ToInt64(value.Value));
            else if (value.Value is decimal)
                writer.WriteNumberValue(Convert.ToDecimal(value.Value));

            else if (value.Value is string)
                writer.WriteStringValue(value.Value as string);
            else if (value.Value is DateTime)
                writer.WriteStringValue(DateTime.Parse(value.Value.ToString()));
            else if (value.Value is DateTimeOffset)
                writer.WriteStringValue(DateTimeOffset.Parse(value.Value.ToString()));
            else if (value.Value is Guid)
                writer.WriteStringValue(Guid.Parse(value.Value.ToString()));
            else if (value.Value is DateTime)
                writer.WriteStringValue(value.Value.ToString());

        }
    }
}
