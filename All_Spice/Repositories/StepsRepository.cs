using System.Data;
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
    }
}