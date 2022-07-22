using System.Collections.Generic;
using System.Data;
using System.Linq;
using All_Spice.Models;
using Dapper;

namespace All_Spice.Repositories
{
    public class IngredientsRepository
    {

        private readonly IDbConnection _db;

        public IngredientsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Ingredient Create(Ingredient ingredientData)
        {
            string sql = @"
            INSERT INTO ingredients
            (name, quantity, recipeId)
            VALUES
            (@name, @quantity, @recipeId);
            SELECT LAST_INSERT_ID();";

            int id = _db.ExecuteScalar<int>(sql, ingredientData);
            ingredientData.Id = id;
            return ingredientData;

        }

        internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
        {
            string sql = @"SELECT
             i.* 
             FROM ingredients i
             WHERE recipeId = @recipeId";
            return _db.Query<Ingredient>(sql, new { recipeId }).ToList();
        }

        internal Ingredient GetById(int id)
        {
            string sql = @"
            SELECT
            i.*
            FROM ingredients i
            WHERE i.id = @id";
            return _db.QueryFirstOrDefault<Ingredient>(sql, new { id });

        }

        internal string Delete(int id)
        {
            string sql = @"
            DELETE
            FROM ingredients
            WHERE id = @id";
            _db.Execute(sql, new { id });
            return ("delorted");
        }

        internal Ingredient Edit(Ingredient original)
        {
            string sql = @"
            UPDATE ingredients
            SET
            name = @Name,
            quantity = @Quantity
            WHERE id = @Id";
            _db.Execute(sql, original);
            return (original);
        }
    }
}