﻿using System.Text.Json.Serialization;
using System.Text.Json;

namespace OS_and_BD_lab_2.Helpers
{
    public class TupleConverter : JsonConverter<(int, int)>
    {
        public override (int, int) Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            reader.Read();
            int item1 = reader.GetInt32();

            reader.Read();
            int item2 = reader.GetInt32();

            reader.Read();
            if (reader.TokenType != JsonTokenType.EndArray)
                throw new JsonException();

            return (item1, item2);
        }

        public override void Write(Utf8JsonWriter writer, (int, int) value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            writer.WriteNumberValue(value.Item1);
            writer.WriteNumberValue(value.Item2);
            writer.WriteEndArray();
        }
    }
}
