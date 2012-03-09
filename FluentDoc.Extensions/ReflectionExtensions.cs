using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentDoc.Extensions
{
    public static class ReflectionExtensions
    {
        public static bool IsPrimitiveOrString(this Type type)
        {
            return type.IsPrimitive || type == typeof (string);
        }
        public static string GetMemberName<T>(this Expression<Func<T, object>> expression)
        {
            Expression body = expression.Body;
            return GetMemberName(body);
        }

        public static string GetMemberName(this Expression<Func<object>> expression)
        {
            Expression body = expression.Body;
            return GetMemberName(body);
        }
        public static MemberInfo GetMemberInfo(this Expression<Func<object>> expression)
        {
            Expression body = expression.Body;
            return GetMemberInfo(body);
        }
        public static MemberInfo GetMemberInfo<T>(this Expression<Func<T, object>> expression)
        {
            Expression body = expression.Body;
            return GetMemberInfo(body);
        }
        public static MemberInfo GetMemberInfo(this Expression expression)
        {
            if (expression is MemberExpression)
            {
                var memberExpression = (MemberExpression)expression;

                //if (memberExpression.Expression.NodeType ==
                //    ExpressionType.MemberAccess)
                //{
                //    return GetMemberName(memberExpression.Expression)
                //        + "."
                //        + memberExpression.Member.Name;
                //}
                return memberExpression.Member;
            }

            if (expression is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)expression;

                if (unaryExpression.NodeType != ExpressionType.Convert)
                    throw new Exception(string.Format(
                        "Cannot interpret member from {0}",
                        expression));

                return GetMemberInfo(unaryExpression.Operand);
            }

            throw new Exception(string.Format(
                "Could not determine member from {0}",
                expression));
        }
        public static string GetMemberName(this Expression expression)
        {
            if (expression is MemberExpression)
            {
                var memberExpression = (MemberExpression)expression;

                if (memberExpression.Expression.NodeType ==
                    ExpressionType.MemberAccess)
                {
                    return GetMemberName(memberExpression.Expression)
                        + "."
                        + memberExpression.Member.Name;
                }
                return memberExpression.Member.Name;
            }

            if (expression is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)expression;

                if (unaryExpression.NodeType != ExpressionType.Convert)
                    throw new Exception(string.Format(
                        "Cannot interpret member from {0}",
                        expression));

                return GetMemberName(unaryExpression.Operand);
            }

            throw new Exception(string.Format(
                "Could not determine member from {0}",
                expression));
        }
        public static bool IsSubclassOfRawGeneric(this  Type toCheck, Type generic)
        {
            if (toCheck == null)
                return false;
            while (toCheck != typeof(object))
            {
                if (toCheck == null)
                    return false;
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }
        public static Type GetUnderlyingType(this MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;
                default:
                    throw new ArgumentException("MemberInfo must be if type FieldInfo, PropertyInfo or EventInfo", "member");
            }
        }

    }
}
