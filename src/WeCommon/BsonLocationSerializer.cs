using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeCommon
{
    public sealed class BsonLocationSerializer : IBsonSerializer<Location>
    {
        #region IBsonSerializer
        public Type ValueType => typeof(Location);

        public Location Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            throw new NotImplementedException();
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Location value)
        {
            context.Writer.WriteStartDocument();
            context.Writer.WriteName("type");
            context.Writer.WriteString("Point");

            context.Writer.WriteName("coordinates");
            context.Writer.WriteStartArray();
            context.Writer.WriteDouble(value.Longitude);
            context.Writer.WriteDouble(value.Latitude);
            context.Writer.WriteEndArray();
            context.Writer.WriteEndDocument();
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            this.Serialize(context, args, (Location)value);
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            Nullable<double> lon = null, lat = null;
            BsonType type = BsonType.Null;
            string name = string.Empty;
            bool error = false;
            context.Reader.ReadStartDocument();
            while (!(name!="coordinates" && lon!=null && lat!=null))
            {
                //try
                //{
                    if (context.Reader.State == BsonReaderState.Type)
                    {
                        type = context.Reader.ReadBsonType();
                        if (type == BsonType.EndOfDocument) break;
                    }
                    if (context.Reader.State == BsonReaderState.Name)
                    {
                        name = context.Reader.ReadName();
                        if (name == "coordinates" && type==BsonType.Array)
                        {
                            context.Reader.ReadStartArray();
                            lon = context.Reader.ReadDouble();
                            lat = context.Reader.ReadDouble();
                            context.Reader.ReadEndArray();
                        }
                    }
                    else if (context.Reader.State == BsonReaderState.Value)
                    {
                        if (type == BsonType.String)
                            context.Reader.ReadString();//read type==Point

                    }
                    if (lon != null && lat != null) break;
                /*}
                catch {
                    error = true;
                }*/
            }
            // context.Reader.ReadEndDocument();
            //if (error) return null;
            return new Location(lon.Value, lat.Value);
        }
        #endregion
    }
}
