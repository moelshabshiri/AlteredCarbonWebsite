using System;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
// using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]// makes validation
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<CooperativeUser> _userManager;
        private readonly UserManager<FarmerUser> _fUserManager;
        private readonly SignInManager<CooperativeUser> _signInManager;
        private readonly SignInManager<FarmerUser> _fSignInManager;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<CooperativeUser> userManager, UserManager<FarmerUser> fUserManager, SignInManager<CooperativeUser> signInManager, SignInManager<FarmerUser> fSignInManager
            , ITokenService tokenService
            )
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _fSignInManager = fSignInManager;
            _userManager = userManager;
            _fUserManager = fUserManager;
        }




        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                var userf = await _fUserManager.Users.SingleOrDefaultAsync(x => x.Email == email);

                return new UserDto
                {
                    Email = userf.Email,
                    Token = _tokenService.CreateToken(userf),
                    Name = userf.Name,
                    AccountType = userf.AccountType
                };
            }

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Name = user.Name,
                AccountType  = user.AccountType
            };
        }


        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            if (await _userManager.FindByEmailAsync(email) != null)
            {
                return true;
            }
            else
            {
                return await _fUserManager.FindByEmailAsync(email) != null;
            }
        }



        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            try{
            var user = await _userManager.FindByEmailAsync(loginDto.Email); //get user from database
            if (user == null)
            {
                var userf = await _fUserManager.FindByEmailAsync(loginDto.Email); //get user from database
                if (userf == null)
                {
                    return  BadRequest(new ApiResponse(401, "Invalid credentials, could not log you in" ));
                }

                var result = await _fSignInManager.CheckPasswordSignInAsync(userf, loginDto.Password, false); //attempt to sign in
                if (!result.Succeeded) return  BadRequest(new ApiResponse(401, "Invalid credentials, could not log you in" ));

                return Ok(new UserDto
                {
                    Email = userf.Email,
                    Token = _tokenService.CreateToken(userf),
                    Name = userf.Name,
                    AccountType = userf.AccountType
                });
            }

            else
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false); //attempt to sign in
                if (!result.Succeeded) return  BadRequest(new ApiResponse(401, "Invalid credentials, could not log you in" ));

                return Ok(new UserDto
                {
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                    Name = user.Name,
                    AccountType = user.AccountType
                });
            }

            }
             catch (Exception)
            {
                return BadRequest(new ApiResponse(400));
            }

        }

    }
}