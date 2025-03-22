using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PE_PRN231_SP25_TrialTest_SE170255_BE.Repository.Models;
using PE_PRN231_SP25_TrialTest_SE170255_BE.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PE_PRN231_SP25_TrialTest_SE170255_BE.APIService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SystemAccountController
    (IConfiguration config, ISystemAccountService systemAccountService)
    : ControllerBase
{
    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = systemAccountService.Authenticate(request.Email, request.Password);

        if (user == null || user.Result == null)
            return Unauthorized();

        var token = GenerateJSONWebToken(user.Result);

        return Ok(token);
    }

    // GET: api/<TestAnswerController>
    [HttpGet]
    public async Task<IEnumerable<SystemAccount>> Get()
    {
        return await systemAccountService.GetAll();
    }
    private string GenerateJSONWebToken(SystemAccount userInfo)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(config["Jwt:Issuer"]
                , config["Jwt:Audience"]
                , new Claim[]
                {
                    new(ClaimTypes.Email, userInfo.EmailAddress),
                    new(ClaimTypes.Role, userInfo.Role.ToString())
                },
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }

    public sealed record LoginRequest(string Email, string Password);
}
