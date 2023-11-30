using Game1.Web.Models;
using Game1.Web.Models.ViewModels.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Game1.Web.Helpers;

namespace Game1.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AdminController(
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
   
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userExists = await _userManager.FindByNameAsync(model.UserName);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });


                //Check Level
                int lvl = CheckLevel(model.Role);

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
                    Level = lvl,
                    Role = model.Role,
                    Salt = salt,
                    PasswordHash = hashedPassword,
                    IsApprove = true,
                    ReferralLink = model.UserName + "123"
                };

                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

                if (!await _roleManager.RoleExistsAsync(UserRoles.SAD))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.SAD));

                if (!await _roleManager.RoleExistsAsync(UserRoles.ADM))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.ADM));

                if (!await _roleManager.RoleExistsAsync(UserRoles.INC))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.INC));

                if (!await _roleManager.RoleExistsAsync(UserRoles.SUB))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.SUB));

                if (!await _roleManager.RoleExistsAsync(UserRoles.MST))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.MST));

                if (!await _roleManager.RoleExistsAsync(UserRoles.GLD))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.GLD));

                if (!await _roleManager.RoleExistsAsync(UserRoles.PLY))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.PLY));

                return RedirectToAction("SignIn", "User");
            }
            else
                return Unauthorized();
        }
        private int CheckLevel(string role)
        {
            switch (role)
            {
                case "SAD":
                    return 0;
                case "ADM":
                    return 1;
                case "INC":
                    return 2;
                case "SUB":
                    return 3;
                case "MST":
                    return 4;
                case "GLD":
                    return 5;
                case "PLY":
                    return 6;
                default:
                    return 6;
            }
        }
    }
}
