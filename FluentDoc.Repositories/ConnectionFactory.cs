using System;
using System.Collections.Generic;
using FluentDoc.Mapping;
using FluentDoc.Repositories.Documents;
using FluentDoc.Repositories.Mappers;

namespace FluentDoc.Repositories
{
    public class ConnectionFactory
    {
        public Dictionary<Type, IDocumentMap> DocumentMaps { get; set; }

        public IDocumentStore DocumentStore
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Dictionary<Type, IMapper> Mappers { get; set; }

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

        public IMapper GetMappers<T>() where T:class
        {
            return Mappers.ContainsKey(typeof (T)) ? Mappers[typeof (T)] : null;
        }
    }
}
