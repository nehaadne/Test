using Dapper;
using System.Data;
using System.Data.SqlClient;
using Test.Context;
using Test.Model;
using Test.Repositories.Interface;

namespace Test.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DapperContext _context;
        public UsersRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> AddUser(Users user)
        {

            int result = 0;
            //using (IDbConnection con = new SqlConnection(Connection.ConnectionString))
            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserName", user.UserName);
                dynamicParameters.Add("@UserMobile", user.UserMobile);
                dynamicParameters.Add("@UserEmail", user.UserEmail);
                dynamicParameters.Add("@Userpassword", user.Userpassword);

                result = connection.Execute("AddUsers", dynamicParameters, commandType: CommandType.StoredProcedure);

            }
            return result;

            //int result = 0;
            //var query = @"insert into Users(UserName,UserMobile,UserEmail,
            //            FaceBookUrl,LinkedInUrl,TwitterUrl,PersonalWebUrl) 
            //            values(@UserName,@UserMobile,@UserEmail,@FaceBookUrl,
            //             @LinkedInUrl,@TwitterUrl,@PersonalWebUrl)
            //            SELECT CAST(SCOPE_IDENTITY() as int)";
            //using (var connection = _context.CreateConnection())
            //{
            //    result = await connection.QuerySingleAsync<int>(query, user);
            //    return result;
            //}
        }





        public int UpdateUser(Users user)
        {
            int result = 0;
            //using (IDbConnection con = new SqlConnection(Connection.ConnectionString))
            using (var connection = _context.CreateConnection())

            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserId", user.UserId);
                dynamicParameters.Add("@UserName", user.UserName);
                dynamicParameters.Add("@UserEmail", user.UserEmail);
                dynamicParameters.Add("@UserMobile", user.UserMobile);
                result = connection.Execute("SP_Update_User_Table", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            return result;
        }


        public List<Users> UserDetails()
        {
            List<Users> u;
            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                //dynamicParameters.Add("@UserId", .UserId);
                u = (List<Users>)connection.Query<Users>("GetAllUsers", commandType: CommandType.StoredProcedure);


                return u;
            }
        }
    }
}

