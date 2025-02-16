﻿using MediatR;
using MyRecipes.Recipes.Application.FoodType.Query.GetAllFoodType;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.FoodType.Query.GetFoodTypeById
{
    public class GetFoodTypeByIdQueryHandler : IRequestHandler<GetFoodTypeByIdQuery, GetFoodTypeByIdQueryResult>
    {
        private readonly IFoodTypeRepository _foodTypeRepository;
        public GetFoodTypeByIdQueryHandler(IFoodTypeRepository foodTypeRepository) => _foodTypeRepository = foodTypeRepository;

        public async Task<GetFoodTypeByIdQueryResult> Handle(GetFoodTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.IsEmpty())
                {
                    throw new WrongParameterException("Invalide parameter", "Id is invalide");
                }
                var result = await _foodTypeRepository.GetAsync(request.Id);
                return new GetFoodTypeByIdQueryResult(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
