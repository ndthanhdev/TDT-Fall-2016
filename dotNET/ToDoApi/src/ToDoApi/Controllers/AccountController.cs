using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Data;
using Microsoft.AspNetCore.Authorization;
using ToDoApi.Options;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoApi.Controllers
{
    [Authorize("User")]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtIssuerOptions _jwtOptions;

        public AccountController(IOptions<JwtIssuerOptions> jwtOptions, ApplicationDbContext context)
        {
            _jwtOptions = jwtOptions.Value;
            _context = context;
        }

        /// <summary>
        /// Get a Json Web Token(JWT)
        /// </summary>
        /// <param name="accountInput"> only accountid and password is required </param>
        /// <returns></returns>
        /// <response code="200">jwt encoded</response>
        /// <response code="401">login fail</response>        
        /// <response code="404">account is not exist</response>        
        [AllowAnonymous]
        [HttpPost("Login")]
        //[ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Login([FromBody, Required] Account accountInput)
        {
            try
            {
                var accountInDb = await _context.Accounts.SingleOrDefaultAsync(u => u.AccountId == accountInput.AccountId);
                if (accountInDb == null)
                    return NotFound();
                if (accountInDb.Password == accountInput.Password)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,accountInput.AccountId),
                        new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                        new Claim("full_name",accountInDb.FullName),
                        new Claim("phone",accountInDb.Phone),
                        new Claim(JwtRegisteredClaimNames.Email,accountInDb.Email),
                        new Claim(ClaimTypes.Role,"User")
                    };
                    // Create the JWT security token and encode it.
                    var jwt = new JwtSecurityToken(
                        issuer: _jwtOptions.Issuer,
                        audience: _jwtOptions.Audience,
                        claims: claims,
                        notBefore: _jwtOptions.NotBefore,
                        expires: _jwtOptions.Expiration,
                        signingCredentials: _jwtOptions.SigningCredentials);

                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                    var json = JsonConvert.SerializeObject(encodedJwt, Formatting.Indented);
                    return Ok(json);
                }
            }
            catch (Exception ex)
            {
            }
            return Unauthorized();
        }

        /// <summary>
        /// Register an account
        /// </summary>
        /// <param name="account">required all except events</param>
        /// <returns></returns>
        /// <response code="201">account created</response>
        /// <response code="400">can't register account with provided information</response>  
        [HttpPost("Register")]
        [AllowAnonymous]
        //[ProducesResponseType(typeof(ObjectResult), 201)]
        [ProducesResponseType(typeof(object), 400)]
        public async Task<IActionResult> Register([FromBody, Required] Account account)
        {
            try
            {
                if (_context.Accounts.Any(a => a.AccountId == account.AccountId))
                {
                    return BadRequest();
                }
                await _context.Accounts.AddAsync(account);
                await _context.SaveChangesAsync();
                return Created(string.Empty, null);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update account infor
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        /// <response code="200">account was updated</response>
        /// <response code="400">can't update account</response>        
        /// <response code="404">account is not exist</response>        
        [HttpPost("Update")]
        //[ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Update([FromBody, Required] Account account)
        {
            try
            {
                if (!_context.Accounts.Any(a => a.AccountId == account.AccountId))
                {
                    return NotFound();
                }
                _context.Update(account);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
