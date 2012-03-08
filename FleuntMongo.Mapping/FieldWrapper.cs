using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;

namespace FleuntMongo.Mapping
{
    public class FieldWrapper
    {
        public MemberInfo Member { get; protected set; }
        public FieldWrapper SetMember<T>(Expression<Func<T, object>> expression) where T : class 
        {
            Member = expression.GetMemberInfo();
            return this;
        }

        public string Name { get { return Member.Name; }}

        public Type Type
        {
            get { return Member.GetUnderlyingType(); }
        }
    }
}
