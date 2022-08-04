using System.Collections.Generic;
using System.Data;
using System.Linq;
using All_Spice.Models;
using Dapper;

namespace All_Spice.Repositories
{
    public class RecipesRepository
    {
        private readonly IDbConnection _db;

        public RecipesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Recipe> GetAll(string query = "")
        {
            string stringQuery = "%" + query + "%";
            string sql = @"
            SELECT 
            r.*,
            a.*
            FROM recipes r
            JOIN accounts a ON r.creatorId = a.id 
            WHERE category LIKE @stringQuery";
            return _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
            {
                recipe.Creator = profile;
                return recipe;
            }, new { stringQuery }).ToList();
        }

        internal List<Recipe>GetRecipesByAccount(string id)
        {
            string sql = @"
            SELECT
            r.*,
            a.*
            FROM recipes r
            JOIN accounts a ON r.creatorId = a.id 
            WHERE r.creatorId = @id 
            ";
            return _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
            {
                recipe.Creator = profile;
                return recipe;
            }, new { id }).ToList();
        }

        internal Recipe GetById(int id)
        {
            string sql = @"
            SELECT
            r.*,
            a.*
            FROM recipes r
            JOIN accounts a ON r.creatorId = a.id
            Where r.id = @id
            ";
            return _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
            {
                recipe.Creator = profile;
                return recipe;
            }, new { id }).FirstOrDefault();

        }



        internal Recipe Create(Recipe recipeData)
        {
            string sql = @"
            INSERT INTO recipes
            (title, picture, subtitle, category, creatorId)
            VALUES
            (@title, @picture, @subtitle, @category, @creatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, recipeData);

            recipeData.Id = id;
            return recipeData;
        }

        internal void Edit(Recipe original)
        {
            string sql = @"
            UPDATE recipes
            SET
            title = @Title,
            picture = @Picture,
            subtitle = @Subtitle,
            category = @Category
            ";
            _db.Execute(sql, original);
        }

        internal void Delete(int id)
        {
            string sql = @"
            DELETE FROM recipes WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }


        // Getting ingredients by recipe Id
        internal Recipe GetIngredientById(int recipeId)
        {
            string sql = @"
           SELECT
           r.*,
           a.*
           FROM recipes r
           JOIN accounts a ON r.creatorId = a.id
           Where r.id = @recipeId";
            return _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
            {
                recipe.Creator = profile;
                return recipe;
            }, new { recipeId }).FirstOrDefault();

        }
    }
}