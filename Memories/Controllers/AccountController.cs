using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Memories.DTOs;
using Memories.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Memories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager; //checken of de gebruiker bestaat en om nieuwe gebruiker aan te maken
        private readonly SignInManager<IdentityUser> _signInManager; //gebruiker aan te melden
        private readonly IUserRepository _userRepository; //nieuwe gebruiker maken
        private readonly IConfiguration _config; //secret ophalen en token retourneren

        public AccountController(SignInManager<IdentityUser> sim, UserManager<IdentityUser> um, IUserRepository userRepo, IConfiguration config)
        {
            _signInManager = sim;
            _userManager = um;
            _userRepository = userRepo;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<String>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Email); //checken of de user bestaat
            if(user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false); //password checken

                if(result.Succeeded) //als de gebruiker is aangemeld, dat kunnen we een token aanmaken
                {
                    string token = GetToken(user);
                    return Created("", token);
                }
            }
            return BadRequest();
        }

        private string GetToken(IdentityUser user) //token = header.payload.signature header=info over het algoritme payload=info over gebruiker in vorm van claims signature=handtekening
        {
            var claims = new List<Claim> { //hier geef je info door die de frontend nodig heeft
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };
            //aanmaken secret
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            //combinatie algoritme en secret voor de handtekening
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //aanmaken token
            var token = new JwtSecurityToken(null, null, claims, expires:DateTime.Now.AddMinutes(30), signingCredentials:cred);
            //aanmaken tokenstring
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}