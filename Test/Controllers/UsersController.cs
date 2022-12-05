using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Model;
using Test.Repositories.Interface;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _userRepo;
        private readonly ILoginRepository _loginRepo;

        public UsersController(IUsersRepository _userRepo, ILoginRepository _loginRepo)
        {
            this._userRepo = _userRepo;
            this._loginRepo = _loginRepo;
        }
        //[HttpGet]
        //[Route("GetString")]
        //public string GetString()
        //{
        //    return "Hello";
        //}

        [HttpGet]
        [Route("UserDetails")]
        public IActionResult UserDetails()
        {
            var u = _userRepo.UserDetails();
            return Ok(u);

        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser(Users users)
        {
            try
            {
                var result = _userRepo.AddUser(users);

                if (await result == 0)
                {
                    return StatusCode(409, "The request could not be processed because of conflict in the request");
                }
                else
                {
                    return StatusCode(200, string.Format("Record Inserted Successfuly with compnay Id {0}", result));
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] Users user)
        {
            try
            {

                Users u = _loginRepo.Authenticate(user.UserEmail, user.Userpassword);
                if (u != null)
                {
                    return new ObjectResult(u);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("EditProfile")]
        public IActionResult ModifyProduct([FromBody] Users user)
        {
            try
            {
                _userRepo.UpdateUser(user);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }



    }
}

