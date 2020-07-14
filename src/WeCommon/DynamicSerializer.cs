using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System;
using System.Linq;

namespace WeCommon
{
    public sealed class DynamicSerializer<T, TSub> : IBsonSerializer<T>, IBsonIdProvider
        where T : class
    {
        #region IBsonSerializer
        public Type ValueType => typeof(T);


        public bool GetDocumentId(object document, out object id, out Type idNominalType, out IIdGenerator idGenerator)
        {
            throw new NotImplementedException();
        }

        public T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            throw new NotImplementedException();
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            this.Serialize(context, args, (T)value);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
        {
            context.Writer.WriteStartDocument();

            Type propertyType = typeof(Property<>);

            var _value = (Property<TSub>)Convert.ChangeType(value, propertyType.MakeGenericType(typeof(TSub)));
            _value.GetDynamicMemberNames().ToList().ForEach(v =>
            {
                context.Writer.WriteName(v);
                var __value = _value[v];
                switch (Type.GetTypeCode(typeof(TSub)))
                {
                    case TypeCode.String: context.Writer.WriteString(_value[v].ToString()); break;
                    case TypeCode.Int32:context.Writer.WriteInt32(Int32.Parse( _value[v].ToString())); break;
                    case TypeCode.Int64: context.Writer.WriteInt64(Int64.Parse(_value[v].ToString())); break;
                    case TypeCode.Double: context.Writer.WriteDouble(double.Parse(_value[v].ToString())); break;
                    case TypeCode.Boolean: context.Writer.WriteBoolean(Boolean.Parse(_value[v].ToString())); break;
                    case TypeCode.DateTime: context.Writer.WriteDateTime(DateTime.Parse(_value[v].ToString()).Ticks); break;
                    default: break;
                };

            });
            context.Writer.WriteEndDocument();

            //context.Writer.WriteString("1234");
            //throw new NotImplementedException();
        }


        #endregion


        #region IBsonIdProvider
        public void SetDocumentId(object document, object id)
        {
            throw new NotImplementedException();
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            dynamic result = new Property<TSub>();
            BsonType type=BsonType.Null;
            string name=string.Empty;
            bool docreaded = false;
            while (true)
            {

                if (!docreaded && context.Reader.CurrentBsonType == MongoDB.Bson.BsonType.Document)
                {
                    context.Reader.ReadStartDocument();
                    docreaded = true;
                }
                if (context.Reader.State == BsonReaderState.Type)
                {
                    type = context.Reader.ReadBsonType();
                    if (type == BsonType.EndOfDocument)
                    {
                        context.Reader.ReadEndDocument();
                        break;
                    }
                }
                if (context.Reader.State == BsonReaderState.Name)
                    name = context.Reader.ReadName();
                if (context.Reader.State == BsonReaderState.Value)
                {
                    if (type == BsonType.String)
                        result[name] = context.Reader.ReadString();
                    else if(type==BsonType.Int32)
                        result[name] = context.Reader.ReadInt32();
                    else if (type == BsonType.Int64)
                        result[name] = context.Reader.ReadInt64();
                    else if (type == BsonType.Boolean)
                        result[name] = context.Reader.ReadBoolean();
                    else if (type == BsonType.DateTime)
                        result[name] = context.Reader.ReadDateTime();
                    else if (type == BsonType.Double)
                        result[name] = context.Reader.ReadDouble();
                    //var type = context.Reader.GetCurrentBsonType();
                    //name = context.Reader.ReadName();
                }
                else if (context.Reader.State == BsonReaderState.EndOfDocument)
                {
                    context.Reader.ReadEndDocument();
                    break;
                }
            }
            //var value=context.Reader.ReadString();
            // result[context.Reader.ReadName()] = value;
            return result;
        }


        #endregion
    }
}
