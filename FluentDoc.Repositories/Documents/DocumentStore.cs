using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentDoc.Repositories.Documents
{
    public class DocumentStore:IDocumentStore
    {
        public IDocument GetById<T>(object id)
        {
            throw new NotImplementedException();
        }

        public void Save<T>(IDocument document)
        {
            throw new NotImplementedException();
        }

        public IList<IDocument> Find<T>()
        {
            throw new NotImplementedException();
        }

        public IList<IDocument> Find<T>(Expression<Func<bool>> predict)
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(object id)
        {
            throw new NotImplementedException();
        }

        public void Update<T>(IDocument document)
        {
            throw new NotImplementedException();
        }
    }
}
