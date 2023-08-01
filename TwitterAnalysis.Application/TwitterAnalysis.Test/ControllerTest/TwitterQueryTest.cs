using AutoFixture;
using System;
using System.Threading.Tasks;
using Xunit;
using TwitterAnalysis.Application.Services.Interfaces;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.Application.Services;
using TwitterAnalysis.Application.Messages.Response;
using Moq;
using TwitterAnalysis.Application.Messages.Request;
using TwitterAnalysis.Application.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace TwitterAnalysis.Test.ControllerTest
{
    public class TwitterQueryTest
    {
        private readonly ITwitterSearchProcessor twitterSearchProcessor;
        private readonly Mock<ITwitterSearchQuery> _twitterSearchQuery;
        private readonly Mock<ITwitterSearchProcessor> _twitterSearchProcessor;
        private readonly Fixture fixture = new();
        TwitterQueryController twitterQueryObject;

        public TwitterQueryTest() 
        {
            _twitterSearchQuery = new Mock<ITwitterSearchQuery>();
            _twitterSearchProcessor = new Mock<ITwitterSearchProcessor>();
            twitterSearchProcessor = new TwitterSearchProcessor(_twitterSearchQuery.Object);
            twitterQueryObject = new TwitterQueryController(_twitterSearchProcessor.Object);
        }
        
        [Fact]
        public async Task TwitterQuery_ProcessSearch_ShouldThrowNullException()
        {
            //Arrange 
            var queryRequest = fixture.Create<QueryRequest>();
            var pagination = fixture.Create<PaginationQuery>();
            _twitterSearchProcessor.Setup(x => x.ProcessSearchByQuery(It.IsAny<string>(), It.IsAny<PaginationQuery>())).Throws(new Exception());

            //Act
            //assert
            await Assert.ThrowsAnyAsync<Exception>(() => twitterQueryObject.PostQuery(queryRequest, pagination));
        }

        [Fact(DisplayName = " Should return OK status code ")]
        public async Task TwitterQuery_ProcessSearch_ShouldReturnOkTypeResult()
        {
            //Arrange
            var queryRequest = fixture.Create<QueryRequest>();
            var pagination = fixture.Create<PaginationQuery>();
            TweetResponse tweets = new();

            _twitterSearchProcessor.Setup(x => x.ProcessSearchByQuery(It.IsAny<string>(), It.IsAny<PaginationQuery>())).ReturnsAsync(tweets);

            //Act
            var result = await twitterQueryObject.PostQuery(queryRequest, pagination);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
