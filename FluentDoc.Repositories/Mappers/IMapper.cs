using FluentDoc.Repositories.Documents;

namespace FluentDoc.Repositories.Mappers
{
    public interface IMapper
    {
        object Map(IDocument document);
        IDocument GetDocument(object entity);
    }
}
