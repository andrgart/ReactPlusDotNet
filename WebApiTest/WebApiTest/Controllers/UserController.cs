using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.AppIdentity;
using WebApiTest.Dto;

namespace WebApiTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserStore<AppUser> _userStore;
        private readonly IUserEmailStore<AppUser> _emailStore;
        private readonly IEmailSender _emailSender;

        public UserController(
            UserManager<AppUser> userManager,
            IUserStore<AppUser> userStore,
            SignInManager<AppUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Post([FromBody] ClientFromForm model)
        {
            Console.WriteLine($"{model.FirstName} T-T");
            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);

            user.Firstname = model.FirstName;
            user.Lastname = model.LastName;
            user.PostAddress = model.PostAddress;

            var result = await _userManager.CreateAsync(user, model.Password);

            if (true)
            {
                await Console.Out.WriteLineAsync("0-0");
                // выдаём РОЛЬ - и авто подтверждение email

                var token = _userManager
                .GenerateEmailConfirmationTokenAsync(user).Result;

                result = _userManager.ConfirmEmailAsync(user, token)
                .Result;

                result = _userManager.AddToRoleAsync(user, "user")
                .Result;

                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Ok();
        }

        private AppUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AppUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUser)}'. " +
                    $"Ensure that '{nameof(AppUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<AppUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<AppUser>)_userStore;
        }

        [HttpGet]
        [Route("cl_test")]
        public string Get()
        {
            return "hello sir, ur a programmer?";
        }
    }
}
