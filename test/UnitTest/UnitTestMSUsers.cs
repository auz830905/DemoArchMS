using Auth.DTOs;
using Microsoft.AspNetCore.Http;

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
    public async void EndPointsExtensions_CreateUser_WhenPasswordIsNull_ReturnsBadRequest()
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
        //Assert.Equal(400, response.StatusCode);
    }

    [Fact]
    public async void EndPointsExtensions_CreateUser_WhenUserNameIsNull_ReturnsBadRequest()
    {
        //Arrange
        var (mockLogger, mockRepository) = GetDependencies();

        var user = new UserCredentials()
        {
            Email = null!,
            Password = "123456"
        };

        //Act
        var response = await EndPointsExtensions.CreateUser(user, mockRepository.Object, mockLogger.Object);

        //Assert
        //Assert.Equal(400, response.StatusCode);
    }

    [Fact]
    public async void EndPointsExtensions_CreateUser_WhenPasswordIsEmpty_ReturnsBadRequest()
    {
        //Arrange
        var (mockLogger, mockRepository) = GetDependencies();

        var user = new UserCredentials()
        {
            Email = "arley@arley.com",
            Password = string.Empty
        };

        //Act
        var response = await EndPointsExtensions.CreateUser(user, mockRepository.Object, mockLogger.Object);

        //Assert
        //Assert.Equal(400, response.StatusCode);
    }

    [Fact]
    public async void EndPointsExtensions_CreateUser_WhenUserNameIsEmpty_ReturnsBadRequest()
    {
        //Arrange
        var (mockLogger, mockRepository) = GetDependencies();

        var user = new UserCredentials()
        {
            Email = string.Empty,
            Password = "123456"
        };

        //Act
        var response = await EndPointsExtensions.CreateUser(user, mockRepository.Object, mockLogger.Object);

        //Assert
       // Assert.Equal(400, response.StatusCode);
    }    

    private static (Mock<ILogger<IUsersRepository>>, Mock<IUsersRepository>) GetDependencies()
        => (new(), new());
}