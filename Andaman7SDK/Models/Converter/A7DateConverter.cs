using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Andaman7SDK.Models.Converter
{
    public class A7DateConverter : IsoDateTimeConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            string text;

            if (value is DateTime)
            {
                DateTime dateTime = (DateTime) value;
                dateTime = dateTime.ToUniversalTime();
                text = dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fff") + dateTime.ToString("zzz").Replace(":","");
            }
            else if (value is DateTimeOffset)
            {
                DateTimeOffset dateTimeOffset = (DateTimeOffset) value;
                dateTimeOffset = dateTimeOffset.ToUniversalTime();
                text = dateTimeOffset.ToString("yyyy-MM-ddTHH:mm:ss.fff") + dateTimeOffset.ToString("zzz").Replace(":","");
            }
            else
            {
                throw new JsonSerializationException("Unexpected value when converting date. Expected DateTime or DateTimeOffset, got " + value + ".");
            }

            writer.WriteValue(text);
        }
    }
}