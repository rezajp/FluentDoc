using System.Collections.Generic;
namespace FluentDoc.Repositories.Documents
{
    public interface IDocument
    {
        object Id { get; set; }
        IDictionary<string,object> Values { get; set; }
    }
}
