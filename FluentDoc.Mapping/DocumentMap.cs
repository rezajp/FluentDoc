using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentDoc.Extensions;

namespace FluentDoc.Mapping
{
    public interface IDocumentMap
    {
        FieldWrapper IdWrapper { get; set; }
        List<FieldWrapper> GetMappedFields();
        List<FieldWrapper> GetReferencedFields();
    }

    public class DocumentMap<T> : IDocumentMap where T:class 
    {
        protected List<FieldWrapper> MappedFields= new List<FieldWrapper>();
        protected List<FieldWrapper> ReferencedFields = new List<FieldWrapper>(); 

        public FieldWrapper IdWrapper { get; set; }
        public List<FieldWrapper> GetMappedFields()
        {
            return MappedFields;
        }

        public List<FieldWrapper> GetReferencedFields()
        {
            return ReferencedFields;
        }

        public FieldWrapper Id(Expression<Func<T, object>> expression)
        {
            var field = new FieldWrapper().SetMember(expression);
            if(!field.Type.IsPrimitive)
                throw new NotSupportedException("The id field has to be a primitive type");
            IdWrapper = field;
            return field;
        }

        public FieldWrapper Map(Expression<Func<T, object>> expression)
        {
            var field = new FieldWrapper().SetMember(expression);
            if (!ReflectionExtensions.IsPrimitiveOrString(field.Type))
                throw new NotSupportedException("The Map method is for primitive types");
            if(MappedFields==null)
                MappedFields= new List<FieldWrapper>();
            MappedFields.Add(field);
            return field;

        }
        public FieldWrapper Reference(Expression<Func<T, object>> expression)
        {
            var field = new FieldWrapper().SetMember(expression);
            if (ReflectionExtensions.IsPrimitiveOrString(field.Type))
                throw new NotSupportedException("The Reference method is for non-primitive types");
            if(ReferencedFields==null)
            {
                ReferencedFields= new List<FieldWrapper>();
            }
            ReferencedFields.Add(field);
            return field;
        }
    }
}