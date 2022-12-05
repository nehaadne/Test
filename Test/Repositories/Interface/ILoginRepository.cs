using Test.Model;

namespace Test.Repositories.Interface
{
    public interface ILoginRepository
    {
        Users Authenticate(string UserEmail, string Userpassword);

    }
}
