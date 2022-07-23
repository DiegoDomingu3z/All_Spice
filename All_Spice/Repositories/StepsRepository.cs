using System.Collections.Generic;
using System.Data;
using System.Linq;
using All_Spice.Models;
using Dapper;

namespace All_Spice.Repositories
{
    public class StepsRepository
    {
        private readonly IDbConnection _db;

        public StepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Step Create(Step stepsData)
        {
            string sql = @"
            INSERT INTO
            steps
            (stepPosition, body, recipeId)
            VALUES
            (@stepPosition, @body, @recipeId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, stepsData);
            stepsData.Id = id;
            return stepsData;

        }

        internal Step GetStepById(int id)
        {
            string sql = @"
            SELECT 
            s.*
            FROM steps s
            WHERE id = @id";
            return _db.QueryFirstOrDefault<Step>(sql, new { id });
        }

        internal List<Step> GetStepsByRecipe(int recipeId, string userId)
        {
            string sql = @"
            SELECT
            s.*
            FROM steps s
           WHERE recipeId = @recipeId";
            return _db.Query<Step>(sql, new { recipeId }).ToList();
        }

        internal Step Edit(Step original)
        {
            string sql = @"
            UPDATE steps
            SET
            stepPosition = @StepPosition,
            body = @body
            WHERE id = @id";
            _db.Execute(sql, original);
            return original;
        }

        internal string Delete(int id)
        {
            string sql = @"
            DELETE FROM steps 
            WHERE id = @id";
            _db.Execute(sql, new { id });
            return ("Delorted");
        }
    }
}