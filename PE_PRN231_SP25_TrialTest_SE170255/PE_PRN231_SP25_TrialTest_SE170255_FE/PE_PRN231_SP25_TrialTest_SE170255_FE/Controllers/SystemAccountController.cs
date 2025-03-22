using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.Data;
using System.IdentityModel.Tokens.Jwt;

namespace PE_PRN231_SP25_TrialTest_NguyenMaiVietVy_FE.Controllers;

public class SystemAccountController : Controller
{
    private string APIEndPoint = "https://localhost:5050/api/";

    public IActionResult Index()
    {
        return RedirectToAction("Login", "SystemAccount");
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest login)
    {

        try
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "SystemAccount/Login", login))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var tokenString = await response.Content.ReadAsStringAsync();

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var jwtToken = tokenHandler.ReadToken(tokenString) as JwtSecurityToken;

                        if (jwtToken != null)
                        {
                            var email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                            var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, email),
                            new Claim(ClaimTypes.Role, role),
                        };

                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                            Response.Cookies.Append("Email", email);
                            Response.Cookies.Append("Role", role);
                            Response.Cookies.Append("TokenString", tokenString);

                            return RedirectToAction("Index", "Home");

                            //if (role == "4" || role == "3")
                            //{
                            //    return View(new List<CosmeticInformation>());
                            //}
                            //else
                            //{
                            //    response = await httpClient.GetAsync("CosmesticInformation/odata");
                            //}                                
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }

        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        ModelState.AddModelError("", "You are not allowed to access this function!");
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        Response.Cookies.Delete("Email");
        Response.Cookies.Delete("Role");
        Response.Cookies.Delete("TokenString");
        return RedirectToAction("Login", "SystemAccount");

    }

    public async Task<IActionResult> Forbidden()
    {
        return View();
    }
}
