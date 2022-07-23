using System;
using System.Collections.Generic;
using All_Spice.Models;
using All_Spice.Repositories;

namespace All_Spice.Services
{
    public class StepsService
    {
        private readonly StepsRepository _repo;

        private readonly RecipesService _rs;


        public StepsService(StepsRepository repo, RecipesService rs)
        {
            _repo = repo;
            _rs = rs;
        }

        internal Step Create(Step stepsData, string userId)
        {
            Recipe recipe = _rs.GetById(stepsData.RecipeId);
            if (recipe.CreatorId != userId)
            {
                throw new Exception("Forbidden");
            }
            return _repo.Create(stepsData);
        }

        internal Step GetStepById(int id)
        {
            Step found = _repo.GetStepById(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;
        }

        internal List<Step> GetStepsByRecipe(int recipeId, string userId)
        {
            return _repo.GetStepsByRecipe(recipeId, userId);
        }

        internal Step Edit(int id, Step stepData, string userId)
        {
            Step original = GetStepById(id);
            Recipe recipe = _rs.GetById(original.RecipeId);
            if (recipe.CreatorId != userId)
            {
                throw new Exception("Forbidden");
            }
            if (original == null)
            {
                throw new Exception("Invalid Id");
            }
            original.StepPosition = stepData.StepPosition ?? original.StepPosition;
            original.Body = stepData.Body ?? original.Body;
            _repo.Edit(original);
            return original;
        }

        internal string Delete(int id, string userId)
        {
            Step step = GetStepById(id);
            Recipe recipe = _rs.GetById(step.RecipeId);
            if (recipe.CreatorId != userId)
            {
                throw new Exception("Forbidden");
            }
            if (step == null)
            {
                throw new Exception("Invalid Id");
            }
            return _repo.Delete(id);

        }
    }
}