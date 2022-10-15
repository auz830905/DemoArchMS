using Auth.DTOs;

namespace MSUsers.Interfaces
{
    public interface IUsersRepository
    {
        //Task<List<User>> GetUsers();
        //Task<User> GetUser(int Id);
        //Task<User> AddUser(User user);
        //Task<User> DeleteUser(int Id);
        //Task<User> UpdateUser(User user);
        public Task<UserToken> CreateUser(UserCredentials userCredentials);
        public Task<UserToken> LoginUser(UserCredentials userCredentials);
    }
}