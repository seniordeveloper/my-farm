using System.Diagnostics.CodeAnalysis;

namespace MyFarm.ApiModels.Search
{
    [ExcludeFromCodeCoverage]
    public class SearchResponse<TRecordModel>
    {
        public List<TRecordModel> Records { get; set; }

        public int TotalCount { get; set; }
    }
}
