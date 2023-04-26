using Auth.DTOs;

namespace MSUsers.Interfaces
{
    public interface IUsersRepository
    {
        public Task<UserToken> CreateUser(UserCredentials userCredentials);
        public Task<UserToken> LoginUser(UserCredentials userCredentials);
    }
}