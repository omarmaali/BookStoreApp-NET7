using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<APIUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<APIUser> userManager, IConfiguration configuration)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._userManager = userManager;
            this._configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            var user = _mapper.Map<APIUser>(userDto);
            var result = await _userManager.CreateAsync(user,userDto.Password);
            if(!result.Succeeded)
            {
                result.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(error.Code, error.Description);
                });

                return BadRequest(ModelState);
            }

            await _userManager.AddToRoleAsync(user, userDto.Role);
            return Accepted();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(UserLoginDto userLoginDto)
        {
            var user =  await _userManager.FindByEmailAsync(userLoginDto.Email);

            if(user == null)
            {
                return Unauthorized(userLoginDto);

            }
            var passwordIsValid =await  _userManager.CheckPasswordAsync(user, userLoginDto.Password);

            if (!passwordIsValid)
                return Unauthorized();

            string token = await GenerateToken(user);

                return Accepted( new AuthenticationResponse()
                {
                    Email = user.Email,
                    UserName = user.Email,
                    Token = token
                });


        }

        private async Task<string> GenerateToken(APIUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credintials =new  SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userRoles = await _userManager.GetRolesAsync(user);
            List<Claim> rolesClaims = userRoles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserID", user.Id)
            }.Union(rolesClaims);



            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims:claims,
                expires: DateTime.Now.AddHours(Convert.ToInt32( _configuration["JWT:Duration"])),
                signingCredentials: credintials 
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
