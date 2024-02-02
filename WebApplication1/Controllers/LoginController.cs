using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        public IConfiguration Configuration;

        public LoginController(ILogger<LoginController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public string UserIdentity
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    return HttpContext.Session.GetString("JWTName");
                }
                else { return ""; }
            }
        }
        // GET: HomeController1
        public ActionResult Index()
        {
            var responses = User.Identity.IsAuthenticated;
            if (responses)
            {
                return Redirect("/");
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> AuthenticatedUser(string username, string password)
        {
            try
            {
                var responsez = User.Identity.IsAuthenticated;
                if (responsez)
                {
                    return Redirect("/");
                }
                ViewBag.pathurl = "/";
                IActionResult responses = Unauthorized();
                var login = new LoginModel()
                {
                    username = username,
                    password = password
                };
                var response = JsonConvert.DeserializeObject<ListLogin>(await Helper.PostDataAsync(login,Configuration["UserConfig:PathAPI"] + "/auth/Login")); ;
                var status = response.status;

                //GET DATA PRIVILEGE
                if (status == "200")
                {
                    //HttpClientHandler clientHandlerUser = new HttpClientHandler();
                    //clientHandlerUser.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                    //{
                    //    return true;
                    //};
                    //HttpClient clientUser = new HttpClient(clientHandlerUser);
                    //HttpRequestMessage requestUser = new HttpRequestMessage(HttpMethod.Get, Configuration["UserConfig:PathAPI"] + "/api/RolesPrivilege");
                    //HttpResponseMessage responseUser = await clientUser.SendAsync(requestUser);
                    //var jsonUser = await responseUser.Content.ReadAsStringAsync();
                    //var objUser = JsonConvert.DeserializeObject<List<RolesAndPrivileges>>(jsonUser);
                    //var privilege = "";
                    //foreach (var i in objUser)
                    //{
                    //    if (i.rolesname.ToUpper() == rolename.ToUpper())
                    //    {
                    //        privilege = i.privilege;
                    //    }
                    //}

                    //if (privilege == "All")
                    //{
                    //    privilege = "1";
                    //}
                    //else if (privilege == "Created Document and View")
                    //{
                    //    privilege = "2";
                    //}
                    //else
                    //{
                    //    privilege = "3";
                    //}

                    var name = response.data.firstname + " " + response.data.lastname;
                    var uname = response.data.username;
                    var IsAdmin = response.data.IsAdmin ? "1" : "0";

                    var token = GenerateJwtToken(login.username);
                    HttpContext.Session.SetString("JWTToken", token);
                    HttpContext.Session.SetString("JWTName", name);
                    HttpContext.Session.SetString("JWTUname", uname);
                    HttpContext.Session.SetString("JWTIsAdmin", IsAdmin);
                    //HttpContext.Session.SetString("JWTPrivilege", privilege);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    var res_ = response;

                    //return RedirectToAction("Index", "Login", res_);
                    return View("Index", res_);
                }
            }
            catch (Exception ex)
            {
                ResultModel result = new ResultModel { status = "500", message = ex.Message.ToString() };
                    return View("Index", result);

                //return RedirectToAction("Error","Login",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

        }   
        private string GenerateJwtToken(string username)
        {
            ViewBag.pathurl = "/";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim (JwtRegisteredClaimNames.Sub, Convert.ToString (username)),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString ())
            };

            var token = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedToken;
        }

        public ActionResult Logout()
        {
            ViewBag.pathurl = Configuration["UserConfig:PathClient"];
            HttpContext.Session.SetString("JWTName", "");
            HttpContext.Session.SetString("JWTUname", "");
            HttpContext.Session.SetString("JWTRolename", "");
            HttpContext.Session.SetString("JWTToken", "");
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
