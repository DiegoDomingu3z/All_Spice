using System;
using All_Spice.Models;
using All_Spice.Repositories;

namespace All_Spice.Services
{
    public class StepsService
    {
        private readonly StepsRepository _sp;

        private readonly RecipesService _rs;

        public StepsService(StepsRepository sp, RecipesService rs)
        {
            _sp = sp;
            _rs = rs;
        }

        internal Step Create(Step stepsData, string userId)
        {
            Recipe recipe = _rs.GetById(stepsData.recipeId);
            if (recipe.CreatorId != userId)
            {
                throw new Exception("Forbidden");
            }
            return _sp.Create(stepsData);
        }

        internal Step GetStepById(int id)
        {
            Step found = _sp.GetStepById(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;
        }
    }
}