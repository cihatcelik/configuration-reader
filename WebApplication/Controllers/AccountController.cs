using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private RoleManager<ApplicationRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        #region Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Required][EmailAddress] string email, [Required] string password, string returnurl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = await userManager.FindByEmailAsync(email);

                if (appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnurl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(email), "Login Failed: Invalid Email or Password");
            }

            return View();
        }
        #endregion

        #region Logout
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserModel user)
        {

            if (ModelState.IsValid)
            {
                if (user.Password != user.RePassword)
                {
                    ModelState.AddModelError("", "Girdiğiniz Şifreler Eşleşmiyor");
                    return View(user);
                }
                if (!user.Terms)
                {
                    ModelState.AddModelError("", "Kullanım Koşullarını Kabul Ediniz");
                    return View(user);
                }
                ApplicationUser appUser = new ApplicationUser
                {
                    UserName = user.Email,
                    Email = user.Email,
                    FullName = user.FullName
                };

                //Daha önceden User ve Admin rolleri yoksa oluştur.

                var roleUser = await roleManager.FindByNameAsync("User");
                if (roleUser == null)
                {
                    //User rolü yoksa oluştur
                    await roleManager.CreateAsync(new ApplicationRole() { Name = "User" });
                }
                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);

                await userManager.AddToRoleAsync(appUser, "User");

                if (result.Succeeded)
                    ViewBag.Message = "Kullanıcı Başarıyla Oluşturuldu";
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }
        #endregion
    }
}
