using System;
using System.Collections.Generic;
using All_Spice.Models;
using All_Spice.Repositories;

namespace All_Spice.Services
{
    public class FavoritesService
    {
        private readonly FavoritesRepository _fp;

        public FavoritesService(FavoritesRepository fp)
        {
            _fp = fp;
        }

        internal List<FavoriteRecipeViewModel> GetByAccount(string userId)
        {
            return _fp.GetByAccount(userId);

        }

        internal Favorite GetById(int id)
        {
            Favorite favorite = _fp.GetById(id);
            if (favorite == null)
            {
                throw new Exception("invalid Id");
            }
            return favorite;
        }

        internal Favorite Create(Favorite favoriteData)
        {
            Favorite exists = _fp.CheckForExists(favoriteData);
            if (exists != null)
            {
                return exists;
            }
            return _fp.Create(favoriteData);
        }

        internal void Delete(int id, string userId)
        {
            Favorite favoriteDeleted = GetById(id);
            if (favoriteDeleted.AccountId != userId)
            {
                throw new Exception("Forbidden");
            }
            if (favoriteDeleted == null)
            {
                throw new Exception("Forbidden");
            }
            _fp.Delete(id);
        }


    }
}