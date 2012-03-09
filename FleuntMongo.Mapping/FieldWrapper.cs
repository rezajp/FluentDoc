using System;
using System.Linq.Expressions;
using System.Reflection;
using FleuntDoc.Extensions;

namespace FleuntDoc.Mapping
{
    public class FieldWrapper
    {
        public MemberInfo Member { get; protected set; }
        public FieldWrapper SetMember<T>(Expression<Func<T, object>> expression) where T : class 
        {
            Member = expression.GetMemberInfo<T>();
            return this;
        }

        public string Name { get { return Member.Name; }}

        public Type Type
        {
            get { return Member.GetUnderlyingType(); }
        }
    }
}
