using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FleuntMongo.Mapping;

namespace FluentMongo.Repositories
{
    public class ConnectionFactory
    {
        public Dictionary<Type, IDocumentMap> DocumentMaps { get; set; }

        public IDatabase Database
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public ConnectionFactory AddMap<T>(IDocumentMap documentMap) where T : class
        {
            if (DocumentMaps == null)
                DocumentMaps = new Dictionary<Type, IDocumentMap>();
            DocumentMaps.Add(typeof(T), documentMap);
            return this;
        }


        public Connection GetConnection<T>()
        {
            var connection = new Connection(typeof (T),this);
            return connection;
        }
    }

    public class Connection
    {
        public Type AssociatedType { get; set; }
        public ConnectionFactory ConnectionFactory { get; set; }
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

        public void Save(object entity)
        {
            throw new NotImplementedException();
        }


        public T Get<T>(object id)
        {
            var document = ConnectionFactory.Database.GetById(id);
            return GetMapper<T>().Map(document);
        }

        private IMapper GetMapper<T>()
        {
            throw new NotImplementedException();
        }
    }
}
