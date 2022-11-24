using Dapper;
using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterAnalysis.App.Service.Model;
using TwitterAnalysis.Infrastructure.Data.Interfaces;
using TwitterAnalysis.Infrastructure.Data.Query;

namespace TwitterAnalysis.Infrastructure.Data.Repository
{
    public class DataTrainingRepository : Repository, IDataTrainingRepository
    {
        public DataTrainingRepository(IDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<RacistModelData>> GetRacistsPhrases()
        {
            try
            {
                return await _npgsqlConnection.QueryAsync<RacistModelData>(TrainingQuery.TrainingRacistPhrasesQuery);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                await CloseConnection();

                if (_npgsqlConnection.State == ConnectionState.Closed)
                    Dispose();
            }
        }

        public async Task InsertRacistPhraseAlgorithmTraining(RacistModelData modelData)
        {
            try
            { 
                var parameters = new {
                    Text = modelData.Text,
                    ActiveRacist = modelData.ActiveRacist
                };

                await _npgsqlConnection.QuerySingleAsync<RacistModelData>(TrainingQuery.TrainingInsertRacistPhraseQuery, parameters);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await CloseConnection();

                if (_npgsqlConnection.State == ConnectionState.Closed)
                    Dispose();
            }
        }
    }
}
