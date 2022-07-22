using System;
using System.Collections.Generic;
using All_Spice.Models;
using All_Spice.Repositories;

namespace All_Spice.Services
{
    public class IngredientsService
    {
        private readonly RecipesService _rs;
        private readonly IngredientsRepository _repo;

        public IngredientsService(RecipesService rs, IngredientsRepository repo)
        {
            _rs = rs;
            _repo = repo;
        }

        internal Ingredient Create(Ingredient ingredientData, string userId)
        {
            Recipe recipe = _rs.GetIngredientById(ingredientData.RecipeId);
            if (recipe.CreatorId != userId)
            {
                throw new Exception("you cannot create an ingredient for this Recipe");
            }
            return _repo.Create(ingredientData);
        }



        internal List<Ingredient> GetIngredientsByRecipeId(int recipeId, string userId)
        {
            // _rs.GetById(recipeId, userId);
            return _repo.GetIngredientsByRecipeId(recipeId);

        }

        //NOTE GetByIngredientById
        internal Ingredient Get(int id)
        {
            Ingredient found = _repo.GetById(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;
        }

        internal string Delete(int id, string userId)
        {
            Ingredient original = Get(id);
            Recipe recipe = _rs.GetById(original.RecipeId);

            if (recipe.CreatorId != userId)
            {
                throw new Exception("Forbidden");
            }
            if (original == null)
            {
                throw new Exception("Forbidden");
            }
            return _repo.Delete(id);
        }

        internal Ingredient Edit(int id, Ingredient ingredientData, string userId)
        {
            Ingredient original = Get(ingredientData.Id);
            Recipe recipe = _rs.GetById(original.RecipeId);
            if (recipe.CreatorId != userId)
            {
                throw new Exception("Forbidden");
            }
            if (original == null)
            {
                throw new Exception("Forbidden");
            }
            original.Name = ingredientData.Name ?? original.Name;
            original.Quantity = ingredientData.Quantity ?? original.Quantity;

            _repo.Edit(original);
            return original;

        }
    }
}