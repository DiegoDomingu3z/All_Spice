using System;
using System.Collections.Generic;
using All_Spice.Models;
using All_Spice.Repositories;

namespace All_Spice.Services
{
    public class RecipesService
    {

        private readonly RecipesRepository _repo;

        public RecipesService(RecipesRepository repo)
        {
            _repo = repo;
        }

        internal List<Recipe> GetAll()
        {
            return _repo.GetAll();
        }

        internal Recipe GetById(int id) //Might need to add Business logic
        {
            Recipe found = _repo.GetById(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;
        }


        // Get Ingredient by Id
        internal Recipe GetIngredientById(int recipeId)
        {
            Recipe found = _repo.GetIngredientById(recipeId);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;
        }

        internal Recipe Create(Recipe recipeData)
        {
            return _repo.Create(recipeData);
        }

        internal Recipe Edit(Recipe recipeData)
        {
            Recipe original = GetById(recipeData.Id);
            if (recipeData.CreatorId != original.CreatorId)
            {
                throw new Exception("You cannot edit this recipe");
            }
            original.Picture = recipeData.Picture ?? original.Picture;
            original.Title = recipeData.Title ?? original.Title;
            original.Subtitle = recipeData.Subtitle ?? original.Subtitle;
            original.Category = recipeData.Category ?? original.Category;

            _repo.Edit(original);
            return original;

        }

        internal Recipe Delete(int id, string userId)
        {
            Recipe deletedRecipe = GetById(id);
            if (userId != deletedRecipe.CreatorId)
            {
                throw new Exception("Forbidden");
            }
            _repo.Delete(id);
            return deletedRecipe;
        }
    }
}