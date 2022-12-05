using Dapper;
using System.Data;
using Test.Context;
using Test.Model;
using Test.Repositories.Interface;

namespace Test.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DapperContext _context;
        public LoginRepository(DapperContext context)
        {
            _context = context;
        }

        public Users Authenticate(string emailId, string password)
        {
            Users user = new Users();
            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserEmail", emailId);
                dynamicParameters.Add("@Userpassword", password);
                user = connection.Query<Users>("SP_SelectByemailId_Users", dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            return user;
        }
    }
}
