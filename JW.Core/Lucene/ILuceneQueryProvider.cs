using Lucene.Net.Search;
using Newtonsoft.Json.Linq;

namespace JW.Core.Lucene
{
    public interface ILuceneQueryProvider
    {
        Query CreateQuery(ILuceneQueryService builder, LuceneQueryContext context, string type, JObject query);
    }
}
