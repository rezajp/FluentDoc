using System;
using FluentDoc.Repositories.Documents;
using FluentDoc.Repositories.Mappers;

namespace FluentDoc.Repositories
{
    public class Connection
    {
        public Type AssociatedType { get; set; }
        public ConnectionFactory ConnectionFactory { get; set; }
        public IDocumentStore DocumentStore
        {
            get { return ConnectionFactory.DocumentStore; }
        }

        public Connection(Type associatedType,ConnectionFactory connectionFactory)
        {
            AssociatedType = associatedType;
            ConnectionFactory = connectionFactory;
            CheckReferencesAreMapped(associatedType);
        }

        private void CheckReferencesAreMapped(Type associatedType)
        {
            var documentMap = ConnectionFactory.DocumentMaps.ContainsKey(associatedType)
                                  ? ConnectionFactory.DocumentMaps[associatedType]
                                  : null;
            if (documentMap == null)
                throw new ArgumentNullException("Could not find mapping for " + associatedType.FullName);
            foreach (var referencedField in documentMap.GetReferencedFields())
            {
                CheckReferencesAreMapped(referencedField.Type);
            }
        }

        public void Save<T>(T entity) where T:class
        {
            var document = GetMapper<T>().GetDocument(entity);
            DocumentStore.Save<T>(document);
        }


        public T Get<T>(object id) where T:class 
        {
            var document = DocumentStore.GetById<T>(id);
            return (T) GetMapper<T>().Map(document);
        }

        private IMapper GetMapper<T>() where T:class
        {
            return ConnectionFactory.GetMappers<T>();
        }
    }
}