using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Text.Json;
using TwitterAnalysis.App.Service.Model.Settings;
using TwitterAnalysis.App.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using TwitterAnalysis.App.Service.Model;

namespace TwitterAnalysis.App.Services.FileProcessor
{
    public class GoogleSheetsProcessor : IGoogleSheetsApiProcessor
    {
        readonly IConfiguration _configuration;
        static SheetsService SheetsService { get; set; }
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        const string SpreadsheetsId = "1Ee883eONUerxdkR2RVFta40mmbRfVIsaglqiGYCRii8";
        const string Sheets = "AppRacial";

        public GoogleSheetsProcessor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<RacistModelData>> ExtractSheetsContent()
        {
            var spreadsheetServices = GetCredentialsConfig();

            return await GetSheetFileWithRacistsTexts(spreadsheetServices);
        }

        #region private methods 

        private static async Task<IEnumerable<RacistModelData>> GetSheetFileWithRacistsTexts(SheetsService sheets)
        {
            var range = $"{Sheets}!A:B";
            var racistPhrases = new List<RacistModelData>();

            var request = sheets.Spreadsheets.Values.Get(SpreadsheetsId, range);
            var response = await request.ExecuteAsync();

            await Task.Delay(100);

            foreach (var item in response.Values)
            {
                racistPhrases.Add(new RacistModelData()
                {
                    ActiveRacist = Convert.ToBoolean(item[1]),
                    Text = Convert.ToString(item[0])
                });
            }

            return racistPhrases;
        }

        private SheetsService GetCredentialsConfig()
        {
            var credentials = new Credentials();
            _configuration.GetSection(nameof(Credentials)).Bind(credentials);

            var jsonCredencials = JsonSerializer.Serialize(credentials);

            var googleCredential = GoogleCredential.FromJson(jsonCredencials).CreateScoped(Scopes);

            SheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = googleCredential
            });

            return SheetsService;
        }

        #endregion
    }
}
