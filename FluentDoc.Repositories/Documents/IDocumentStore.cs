using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentDoc.Repositories.Documents
{
    public interface IDocumentStore
    {
        IDocument GetById<T>(object id);
        void Save<T>(IDocument document);
        IList<IDocument> Find<T>();
        IList<IDocument> Find<T>(Expression<Func<bool>> predict );
        void Delete<T>(object id);
        void Update<T>(IDocument document);

    }
}
