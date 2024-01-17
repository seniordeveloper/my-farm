using Microsoft.AspNetCore.Mvc;
using MyFarm.ApiModels.Animal;
using MyFarm.ApiModels.Search;
using MyFarm.Contracts.Services;
using MyFarm.WebApi.Controllers.Base;
using ILogger = MyFarm.Log.ILogger;

namespace MyFarm.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnimalsController : BaseApiController
    {
        private readonly IAnimalManager _animalManager;

        public AnimalsController(IAnimalManager animalManager, ILogger logger)
           : base(logger)
        {
            _animalManager = animalManager;
        }

        [Route("[action]")]
        [HttpGet]
        public Task<IActionResult> Search(string request = null) 
        {
            return WrapResponseAsync(cancellationToken =>
            {
                var requestModel = FromJson<SearchRequest>(request);

                return _animalManager.SearchAnimalsAsync(requestModel, cancellationToken);
            });
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        public Task<IActionResult> Delete(int id)
        {
            return WrapResponseAsync(cancellationToken => _animalManager.DeleteAnimalAsync(id, cancellationToken));
        }

        [Route("[action]")]
        [HttpPut]
        public Task<IActionResult> Update(AnimalModel animal)
        {
            return WrapResponseAsync(cancellationToken => _animalManager.CreateOrUpdateAsync(animal, cancellationToken));
        }

        [Route("[action]")]
        [HttpPost]
        public Task<IActionResult> Create(AnimalModel animal)
        {
            return WrapResponseAsync(cancellationToken => _animalManager.CreateOrUpdateAsync(animal, cancellationToken));
        }
    }
}
