using System;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters.V2;
using TwitterAnalysis.App.Services.Interfaces;

namespace TwitterAnalysis.App.Services
{
    public class TwitterSearchService : ITwitterSearchQuery
    {
		private string BearerToken = "";


		public async void GetTweetBySearch(string query)
		{
			var appCredentials = new TwitterClient(new ConsumerOnlyCredentials { BearerToken = BearerToken });

			try
			{

				var searchParameters = new SearchTweetsV2Parameters("Racismo")
				{
					PageSize = 100,
				};

				var response = await appCredentials.SearchV2.SearchTweetsAsync(searchParameters);
				foreach (var item in response.Tweets)
				{
					Console.WriteLine($"Twitter hoje: {item.Text} ");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Erro " + e);
				throw e;
			}
		}
	}
}
