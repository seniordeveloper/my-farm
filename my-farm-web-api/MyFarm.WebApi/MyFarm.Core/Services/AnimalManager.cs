using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyFarm.ApiModels.Animal;
using MyFarm.ApiModels.Search;
using MyFarm.Common.Constants;
using MyFarm.Common.Exceptions;
using MyFarm.Common.Extensions;
using MyFarm.Contracts.Services;
using MyFarm.Data;
using MyFarm.Data.Entities;

namespace MyFarm.Core.Services
{
    /// <summary>
    /// A default implementation of <see cref="IAnimalManager"/>
    /// </summary>
    public class AnimalManager : IAnimalManager
    {
        private readonly FarmDbContext _farmDbContext;
        private readonly IMapper _mapper;

        public AnimalManager(
            FarmDbContext farmDbContext,
            IMapper mapper)
        {
            _farmDbContext = farmDbContext;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAnimalAsync(int id, CancellationToken cancellationToken)
        {
            _farmDbContext.Animals.RemoveRange(_farmDbContext.Animals.Where(x => x.Id == id));
            
            return (await _farmDbContext.SaveChangesAsync()) > 0;
        }

        public async Task<SearchResponse<AnimalModel>> SearchAnimalsAsync(SearchRequest searchRequest, CancellationToken cancellationToken)
        {
            var query = BuildQuery(searchRequest);

            var count = await query.CountAsync(cancellationToken);

            var pageSize = searchRequest.PageSize ?? SqlConstants.DefaultPageSize;
            var pageIndex = searchRequest.PageIndex ?? 0;

            query = query
                .Paginate(pageSize, pageIndex)
                .OrderBy(x => x.Name);

            var entities = await query.ToListAsync(cancellationToken);

            return new SearchResponse<AnimalModel> 
            {
                TotalCount = count,
                Records = _mapper.Map<List<AnimalModel>>(entities)
            };
        }

        public async Task<AnimalModel> CreateOrUpdateAsync(AnimalModel animal, CancellationToken cancellationToken) 
        {
            if (string.IsNullOrWhiteSpace(animal?.Name)) 
            {
                throw new MyFarmApplicationException(Common.Enums.AppErrorCode.UnhandledError);
            }

            var creatingNew = animal.Id == 0;
            
            var animalEntity = !creatingNew
                ? await _farmDbContext.Animals.SingleAsync(x => x.Id == animal.Id)
                : new AnimalEntity();

            _mapper.Map(animal, animalEntity);

            if (creatingNew) 
            {
               // hope we have the only user
               animalEntity.Id = await _farmDbContext.Animals.MaxAsync(x => x.Id) + 1;
                animal.Id = animalEntity.Id;

                _farmDbContext.Animals.Add(animalEntity);
            }

            await _farmDbContext.SaveChangesAsync();

            return animal;
        }

        private IQueryable<AnimalEntity> BuildQuery(SearchRequest searchRequest) 
        {
            var animalsQuery = _farmDbContext.Animals.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchRequest.SearchTerm)) 
            {
                var searchTerm = searchRequest.SearchTerm.Trim();

                animalsQuery = animalsQuery.Where(x => x.Name.Contains(searchTerm));
            }
            return animalsQuery;
        }
    }
}
