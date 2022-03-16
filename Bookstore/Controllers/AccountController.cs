using Bookstore.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Controller for the login page
namespace Bookstore.Controllers
{
    public class AccountController : Controller
    {
        //Parts of the Core Identity package
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        //Constructor
        public AccountController(UserManager<IdentityUser> um, SignInManager<IdentityUser> sm)
        {
            //Set the variables in the constructor
            this.userManager = um;
            this.signInManager = sm;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)  //Change to Login
        {
            return View(new LoginModel { ReturnUrl = returnUrl });  //Create a new instance of a login model and set the ReturnUrl variable
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            //Check to see if the model is valid
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(loginModel.Username);

                //If the user is not equal to null
                if (user != null)
                {
                    await signInManager.SignOutAsync();

                    //Try loggin in based on the info passed
                    if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        //Take them to the admin page if the return url is null
                        return Redirect(loginModel?.ReturnUrl ?? "/Admin");
                    }
                }
            }

            //If the user did not log in successfully, return an error
            ModelState.AddModelError("", "Invalid username or password");
            return View(loginModel);
        }

        //Logout action
        public async Task<RedirectResult> Logout (string returnUrl = "/")  //Set return Url = to root if null
        {
            await signInManager.SignOutAsync();

            return Redirect(returnUrl);
        }
    }
}
