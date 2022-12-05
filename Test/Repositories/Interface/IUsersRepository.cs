using Test.Model;

namespace Test.Repositories.Interface
{
    public interface IUsersRepository
    {
        Task<int> AddUser(Users user);
        int UpdateUser(Users user);
        List<Users> UserDetails();

    }
}
