using MyFarm.ApiModels.Animal;
using MyFarm.ApiModels.Search;

namespace MyFarm.Contracts.Services
{
    /// <summary>
    /// Provides an asbtraction to manage animals.
    /// </summary>
    public interface IAnimalManager
    {
        /// <summary>
        /// Search within animals table and returns a collection of <see cref="AnimalModel"/> as an asynchronous operation.
        /// </summary>
        /// <param name="searchRequest">A payload that contain search terms.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that contains the result of the asynchronous operation containing an instance of <see cref="SearchResponse{TRecordModel}"/>.</returns>
        Task<SearchResponse<AnimalModel>> SearchAnimalsAsync(SearchRequest searchRequest, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously deletes given animal.
        /// </summary>
        /// <param name="id">Animal's unique identifier.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that contains the result of the asynchronous operation containing a boolean flag.</returns>
        Task<bool> DeleteAnimalAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously creates a new or updates existing animal record.
        /// </summary>
        /// <param name="animal">Payload.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that contains the result of the asynchronous operation containing a boolean flag.</returns>
        Task<AnimalModel> CreateOrUpdateAsync(AnimalModel animal, CancellationToken cancellationToken);
    }
}
