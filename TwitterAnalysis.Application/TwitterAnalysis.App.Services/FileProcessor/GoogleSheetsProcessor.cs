using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Services;
using System;
using System.Linq;
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
            string SpreadsheetsId = "1Ee883eONUerxdkR2RVFta40mmbRfVIsaglqiGYCRii8";
            string Sheets = "AppRacial";

            var range = $"{Sheets}!A:B";
            List<RacistModelData> racistPhrases = new();

            var request = sheets.Spreadsheets.Values.Get(SpreadsheetsId, range);
            var responseFromSheets = await request.ExecuteAsync();

            racistPhrases.AddRange(from dataOnSheets in responseFromSheets.Values
                                   select new RacistModelData()
                                   {
                                       ActiveRacist = Convert.ToBoolean(dataOnSheets[1]),
                                       Text = Convert.ToString(dataOnSheets[0])
                                   });
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
