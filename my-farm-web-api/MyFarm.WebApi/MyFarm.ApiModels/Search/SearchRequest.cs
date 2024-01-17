using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MyFarm.ApiModels.Search
{
    [ExcludeFromCodeCoverage]
    public class SearchRequest
    {
        public string SearchTerm { get; set; }

        public int? PageIndex { get; set; }

        public int? PageSize { get; set; }
    }
}
