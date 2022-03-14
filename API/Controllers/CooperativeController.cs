using System;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using Core.Entities;
// using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]// makes validation
    [Route("api/[controller]")]
    public class CooperativeController : ControllerBase
    {
        private readonly UserManager<CooperativeUser> _userManager;
        private readonly UserManager<FarmerUser> _fUserManager;
        private readonly SignInManager<CooperativeUser> _signInManager;
        private readonly ITokenService _tokenService;
        public CooperativeController(UserManager<CooperativeUser> userManager, UserManager<FarmerUser> fUserManager, SignInManager<CooperativeUser> signInManager
            , ITokenService tokenService
            )
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _fUserManager = fUserManager;
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
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email); //get user from database
                if (user == null) return BadRequest(new ApiResponse(401, "Invalid credentials, could not log you in"));
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false); //attempt to sign in
                if (!result.Succeeded) return BadRequest(new ApiResponse(401, "Invalid credentials, could not log you in"));

                return Ok(new UserDto
                {
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                    Name = user.Name,
                    AccountType = user.AccountType
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400));
            }
        }

        [HttpPost("signup")]
        public async Task<ActionResult<UserDto>> SignUp(RegisterDto registerDto)
        {
            CooperativeUser user;
            try
            {
                if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
                {
                    return BadRequest(new ApiResponse(422, "Email address is in use"));
                }

                user = new CooperativeUser
                {
                    Name = registerDto.Name,
                    Email = registerDto.Email,
                    UserName = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber,
                    AccountType = "cooperative"
                };


                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (!result.Succeeded)
                    return BadRequest(new ApiResponse(400));

            }
            catch (Exception)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Name = user.Name,
                AccountType = user.AccountType
            });
        }
    }
}