using Game1.Web.Helpers;
using Game1.Web.Models;
using Game1.Web.Models.ViewModels.Admin;
using Game1.Web.Models.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Game1.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.userName);
                if (user == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Not Found!" });

                //PasswordHash
                string salt = user.Salt;
                string hashedPassword = PasswordHasher.HashPassword(model.Password, salt);

                bool isValid = PasswordHasher.VerifyPassword(model.Password, user.PasswordHash, user.Salt);

                if (isValid)
                {
                    if(user.Level == 0)
                        return RedirectToAction("Index", "Approval");
                    else
                    {
                        return RedirectToAction("PlayerView", "Dashboard", new { userName = model.userName });
                    }
                }
            }
            return RedirectToAction("SignIn", "User");
        }

        public IActionResult SignUp(string? referralLink)
        {
            //referral1.LocalLink = "https://localhost:7054/members/signup?username=" + model.UserName;
            //public IActionResult SignUp(string? username)

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegistrationViewModel model)
        {
            //referral1.LocalLink = "https://localhost:7054/members/signup?username=" + model.UserName;
            //public IActionResult SignUp(string? username)

            if (ModelState.IsValid)
            {
                var userExists = await _userManager.FindByNameAsync(model.UserName);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });


                //Check Agent
                var agent = _userManager.Users.Where(u => u.ReferralLink == model.referralLink).Single().Id;

                //PasswordHash
                string salt = PasswordHasher.GenerateSalt();
                string hashedPassword = PasswordHasher.HashPassword(model.Password, salt);

                User user = new()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = "admin@gmail.com",
                    PhoneNumber = model.Contact,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName,
                    Level = 6,
                    Role = "PLY",
                    Salt = salt,
                    PasswordHash = hashedPassword,
                    IsApprove = true,
                    ReferralLink = model.referralLink,
                    AgentId = agent
                };

                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

                return RedirectToAction("SignIn", "User");
            }
            else
                return Unauthorized();
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(8),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
