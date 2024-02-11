using System.ComponentModel.DataAnnotations;
using ClassifiedDocumentPortal.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClassifiedDocumentPortal.BlazorUi.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<PortalUser> _signInManager;
        private readonly UserManager<PortalUser> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<PortalUser> signInManager, UserManager<PortalUser> userManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }
        
        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            public string Username { get; set; }
            
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/documents");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(Input.Username);

                    if (user is not null)
                    {
                        _logger.LogInformation(
                        $"User logged in. " +
                        $"Name: {user.Name}; " +
                        $"Email: {user.Email}; " +
                        $"Security Clearance: {user.SecurityClearance}; " +
                        $"Background Check Status: {user.BackgroundCheckStatusCompleted}; " +
                        $"Department of Defense Contractor Number: {user.DepartmentOfDefenseContractorNumber}; " +
                        $"US Federal Contractor Registration Number: {user.USFederalContractorRegistrationNumber}. ");
                    }

                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email or password are not correct.");

                    return Page();
                }
            }

            return Page();
        }
    }
}
