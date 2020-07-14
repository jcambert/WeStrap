using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace WeCommon
{

    public sealed class Property<T> : DynamicObject
    {
        // The inner dictionary.
        Dictionary<string, T> dictionary = new Dictionary<string, T>();


        public T this[string name]
        {
            get =>(T) GetValue(name);
            set => SetValue(name, value);
        }
        // If you try to get a value of a property
        // not defined in the class, this method is called.
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result=GetValue(binder.Name);
            return true;
        }

        // If you try to set a value of a property that is
        // not defined in the class, this method is called.
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            SetValue(binder.Name, value);
            return true;

        }
        public override DynamicMetaObject GetMetaObject(Expression expression)
        {
            return new SerializableDynamicMetaObject<T>(expression,
            BindingRestrictions.GetInstanceRestriction(expression, this), this);
        }
        #region Helper methods for dynamic meta object support
        internal object SetValue(string name, object value)
        {
            
            //Console.WriteLine(value is int);
            try
            {
                var v=(T)Convert.ChangeType(value, typeof(T));
                if (v==null && !object.Equals(v, default(T)) ) throw new ArgumentException($"{nameof(value)}-{value.GetType().ToString()} must be of type {typeof(T)}");
                dictionary[name.ToLower()] = v;
            }
            catch
            {
                throw new ArgumentException($"{nameof(value)}-{value.GetType().ToString()} must be of type {typeof(T)}");
            }
            return value;
        }

        internal object GetValue(string name)
        {
            T r;
            dictionary.TryGetValue(name, out r);
            return r;


        }

        public  override IEnumerable<string> GetDynamicMemberNames()
        {
            return dictionary.Keys;
        }
        #endregion
    }

    public sealed class SerializableDynamicMetaObject<T> : DynamicMetaObject
    {
        Type objType;

        internal SerializableDynamicMetaObject(Expression expression, BindingRestrictions restrictions, object value)
            : base(expression, restrictions, value)
        {
            objType = value.GetType();
        }

        public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
        {
            var self = this.Expression;
            var dynObj = this.Value;
            var keyExpr = Expression.Constant(binder.Name);
            var getMethod = objType.GetMethod("GetValue", BindingFlags.NonPublic | BindingFlags.Instance);
            var target = Expression.Call(Expression.Convert(self, objType),
                                         getMethod,
                                         keyExpr);
            return new DynamicMetaObject(target,
                BindingRestrictions.GetTypeRestriction(self, objType));
        }

        public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
        {
            var self = this.Expression;
            var keyExpr = Expression.Constant(binder.Name);
            var valueExpr = Expression.Convert(value.Expression, typeof(object));
            var setMethod = objType.GetMethod("SetValue", BindingFlags.NonPublic | BindingFlags.Instance);
            var target = Expression.Call(Expression.Convert(self, objType),
            setMethod,
            keyExpr,
            valueExpr);
            return new DynamicMetaObject(target,
                BindingRestrictions.GetTypeRestriction(self, objType));
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            var dynObj = (Property<T>)this.Value;
            return dynObj.GetDynamicMemberNames();
        }
    }
}
