using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Services;
using System;
using System.Text.Json;
using TwitterAnalysis.App.Service.Model.Settings;
using TwitterAnalysis.App.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using TwitterAnalysis.App.Service.Model;
using Microsoft.Extensions.Options;

namespace TwitterAnalysis.App.Services.FileProcessor
{
    public class GoogleSheetsProcessor : IGoogleSheetsApiProcessor
    {
        private readonly GoogleCredentialsSettings _googleCredentialsSettings;
        static SheetsService SheetsService { get; set; }
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        const string SpreadsheetsId = "1Ee883eONUerxdkR2RVFta40mmbRfVIsaglqiGYCRii8";
        const string Sheets = "AppRacial";

        public GoogleSheetsProcessor(IOptions<GoogleCredentialsSettings> options)
        {
            _googleCredentialsSettings = options.Value;
        }

        public async Task<IEnumerable<RacistModelData>> ExtractSheetsContent()
        {
            var spreadsheet = GetCredentialsConfig();

            return await GetSheetFileWithRacistsTexts(spreadsheet);
        }

        #region private methods 
        private static async Task<IEnumerable<RacistModelData>> GetSheetFileWithRacistsTexts(SheetsService sheets)
        {
            var range = $"{Sheets}!A:B";
            var racistPhrases = new List<RacistModelData>();

            var request = sheets.Spreadsheets.Values.Get(SpreadsheetsId, range);
            var response = await request.ExecuteAsync();

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
            var jsonCredencials = JsonSerializer.Serialize(_googleCredentialsSettings);

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
