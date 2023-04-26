using Auth.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Moq;
using MSUsers.Extensions;
using MSUsers.Interfaces;
using FluentAssertions;
using UnitTest.Extensions;


namespace UnitTest;

public class UnitTestMSUsers
{
    [Fact]
    public async void EndPointsExtensions_CreateUser_WhenPasswordIsNull_ReturnsBadReques()
    {
        //Arrange
        var (mockLogger, mockRepository) = GetDependencies();

        var user = new UserCredentials()
        {
            Email = "arley@arley.com",
            Password = null!
        }; 

        //Act
        var response = await EndPointsExtensions.CreateUser(user, mockRepository.Object, mockLogger.Object);

        //Assert
        response.GetBadRequestObjectResultStatusCode().Should().Be(StatusCodes.Status400BadRequest);
    }

    private static (Mock<ILogger<IUsersRepository>>, Mock<IUsersRepository>) GetDependencies()
        => (new(), new());
}