using System.Collections.Generic;
using System.Data;
using System.Linq;
using All_Spice.Models;
using Dapper;

namespace All_Spice.Repositories
{
    public class FavoritesRepository
    {

        private readonly IDbConnection _db;

        public FavoritesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Favorite Create(Favorite favoriteData)
        {
            string sql = @"
            INSERT INTO favorites
            (accountId, recipeId)
            VALUES
            (@accountId, @recipeId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, favoriteData);
            favoriteData.Id = id;
            return favoriteData;
        }

        internal List<FavoriteRecipeViewModel> GetByAccount(string id)
        {
            string sql = @"
           SELECT 
            a.*,
            f.*,
            r.*
            FROM favorites f
            JOIN recipes r ON f.recipeId = r.id
            JOIN accounts a ON r.creatorId = a.id
            WHERE f.accountId = @id;
            ";
            List<FavoriteRecipeViewModel> recipes = _db.Query<Account, Favorite, FavoriteRecipeViewModel, FavoriteRecipeViewModel>(sql, (account, favorite, recipe) =>
            {
                recipe.Creator = account;
                recipe.FavoriteId = favorite.Id;
                return recipe;
            }, new { id }).ToList<FavoriteRecipeViewModel>();
            return recipes;
        }

        internal Favorite GetById(int id)
        {
            string sql = @"SELECT
            f.*
            FROM favorites f
            WHERE id = @id";
            return _db.QueryFirstOrDefault<Favorite>(sql, new { id });
        }

        internal void Delete(int id)
        {
            string sql = @"
            DELETE FROM favorites
            WHERE 
            id = @id";
            _db.Execute(sql, new { id });


        }
    }
}