using System;
using System.Threading.Tasks;
using Xunit;
using TwitterAnalysis.Application.Services.Interfaces;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.Application.Services;
using TwitterAnalysis.Application.Messages.Response;
using Moq;

namespace TwitterAnalysis.Test.ControllerTest
{
    public class TwitterQueryTest
    {
        private readonly ITwitterSearchProcessor twitterSearchProcessor;
        private readonly Mock<ITwitterSearchQuery> _twitterSearchQuery;
        private readonly Mock<ITwitterSearchProcessor> _twitterSearchProcessor;

        public TwitterQueryTest() 
        {
            _twitterSearchQuery = new Mock<ITwitterSearchQuery>();
            _twitterSearchProcessor = new Mock<ITwitterSearchProcessor>();
            twitterSearchProcessor = new TwitterSearchProcessor(_twitterSearchQuery.Object);

        }
        
        [Fact]
        public void TwitterQuery_ProcessSearch_ShouldThrowNullException()
        {
            //Arrange 
            _twitterSearchQuery.Setup(x => x.GetTweetBySearch(It.IsAny<string>())).Throws(new Exception());

            //Act
            //assert
            Assert.ThrowsAnyAsync<Exception>(() => twitterSearchProcessor.ProcessSearchByQuery(It.IsAny<string>()));
        }

        [Fact]
        public void TwitterQuery_ProcessSearch_ShouldReturnTweetEntityType()
        {
            //Arrange
            TweetResponse tweets = new();
            _twitterSearchProcessor.SetupSequence(x => x.ProcessSearchByQuery(It.IsAny<string>())).ReturnsAsync(tweets);

            //Act
            var result = twitterSearchProcessor.ProcessSearchByQuery(It.IsAny<string>());

            //Assert
            Assert.IsType<Task<TweetResponse>>(result);
        }
    }
}
