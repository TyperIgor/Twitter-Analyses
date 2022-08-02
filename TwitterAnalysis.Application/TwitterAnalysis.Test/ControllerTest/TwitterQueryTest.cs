using System;
using Xunit;
using TwitterAnalysis.Application.Services.Interfaces;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.Application.Services;
using Moq;
using TwitterAnalysis.Application.Controllers;

namespace TwitterAnalysis.Test.ControllerTest
{
    public class TwitterQueryTest
    {
        private readonly ITwitterSearchProcessor twitterSearchProcessor;
        private readonly Mock<ITwitterSearchQuery> _twitterSearchQuery;
        private readonly TwitterQueryController twitterQueryController;

        public TwitterQueryTest() 
        {
            _twitterSearchQuery = new Mock<ITwitterSearchQuery>();
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

        public void TwitterQuery_ProcessSearch_ShouldReturnTweetEntity()
        {

        }
    }
}
