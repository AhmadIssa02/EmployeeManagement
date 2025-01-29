using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<APIUser> _userManager;
        private readonly SignInManager<APIUser> _signInManager;

        public AccountController(SignInManager<APIUser> signInManager, UserManager<APIUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {

            try
            {


                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }

                var user = new APIUser
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Email = registerDto.Email,
                    UserName = registerDto.Email
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (!result.Succeeded)
                {

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return BadRequest(ModelState);
                }
                await _userManager.AddToRolesAsync(user, registerDto.Roles);
                return Accepted();
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }

        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(RegisterDto registerDto)
        {
           throw new NotImplementedException();
        }
    }
}