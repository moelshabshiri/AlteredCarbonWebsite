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
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]// makes validation
    [Route("api/[controller]")]
    public class FarmersController : ControllerBase
    {
        private readonly UserManager<FarmerUser> _userManager;
        private readonly UserManager<CooperativeUser> _cUserManager;
        private readonly SignInManager<FarmerUser> _signInManager;
        private readonly ITokenService _tokenService;
        // private readonly IMapper _mapper;
        public FarmersController(UserManager<FarmerUser> userManager, UserManager<CooperativeUser> cUserManager, SignInManager<FarmerUser> signInManager
            , ITokenService tokenService
            )
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _cUserManager = cUserManager;
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
                return await _cUserManager.FindByEmailAsync(email) != null;
            }
        }



        [Authorize]
        [HttpGet("points")]
        public async Task<ActionResult<decimal>> GetPoints()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == email);

            return user.Points;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email); //get user from database

                if (user == null) return BadRequest(new ApiResponse(401, "Invalid credentials, could not log you in" ));

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false); //attempt to sign in

                if (!result.Succeeded) return BadRequest(new ApiResponse(401, "Invalid credentials, could not log you in" ));

                return Ok(new UserDto
                {
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                    Name = user.Name
                });
            }
            catch (Exception)
            {
                return BadRequest(new ApiResponse(400));
            }

            // return Ok(new { user = user });

        }

        [HttpPost("signup")]
        public async Task<ActionResult<FarmerUser>> SignUp(RegisterDto registerDto)
        {
            FarmerUser user;
            try
            {
                if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
                {
                    return BadRequest(new ApiResponse(422, "Email address is in use"));
                }
                user = new FarmerUser
                {
                    Name = registerDto.Name,
                    Email = registerDto.Email,
                    UserName = registerDto.Email,
                    AccountType = "farmer",
                    Points = 500
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (!result.Succeeded)
                    throw new Exception();

            }
            catch (Exception)
            {
                return BadRequest(new ApiResponse(400));
            }

            return Ok(new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Name = user.Name
            });
        }








        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);

            AddressDto addressDto = new AddressDto();


            addressDto.FirstName = user.Address.FirstName;
            addressDto.LastName = user.Address.LastName;
            addressDto.Street = user.Address.Street;
            addressDto.City = user.Address.City;
            addressDto.ZipCode = user.Address.ZipCode;

            return addressDto;
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);


            user.Address.FirstName = address.FirstName;
            user.Address.LastName = address.LastName;
            user.Address.Street = address.Street;
            user.Address.City = address.City;
            user.Address.ZipCode = address.ZipCode;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok((address));

            return BadRequest("Problem updating the user");
        }
    }
}