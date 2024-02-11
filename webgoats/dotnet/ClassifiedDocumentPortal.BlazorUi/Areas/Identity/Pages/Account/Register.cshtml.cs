using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using ClassifiedDocumentPortal.Domain.Entities;
using ClassifiedDocumentPortal.Domain.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace ClassifiedDocumentPortal.BlazorUi.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<PortalUser> _signInManager;
        private readonly UserManager<PortalUser> _userManager;
        private readonly IUserStore<PortalUser> _userStore;
        private readonly IUserEmailStore<PortalUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<PortalUser> userManager,
            IUserStore<PortalUser> userStore,
            SignInManager<PortalUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Email Address")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Full Name")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Address")]
            public string Address { get; set; }

            [Required]
            [Display(Name = "Background Check Status Completed")]
            public bool BackgroundCheckStatusCompleted { get; set; }

            [Required]
            [Display(Name = "Security Clearance")]
            public ClassificationType SecurityClearance { get; set; }

            [Required]
            [Display(Name = "Department Of Defense Contractor Number")]
            public string DepartmentOfDefenseContractorNumber { get; set; }

            [Required]
            [Display(Name = "US Federal Contractor Registration Number")]
            public string USFederalContractorRegistrationNumber { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = CreateUser(Input.Email, Input.Name, Input.Address, Input.BackgroundCheckStatusCompleted, Input.SecurityClearance, Input.DepartmentOfDefenseContractorNumber, Input.USFederalContractorRegistrationNumber); ;
                await _userStore.SetUserNameAsync(user, Input.Username, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }

        private PortalUser CreateUser(
            string email,
            string name, 
            string address, 
            bool backgroundCheckStatusCompleted, 
            ClassificationType securityClearance, 
            string departmentOfDefenseContractorNumber, 
            string usFederalContractorRegistrationNumber)
        {
            try
            {
                var user = Activator.CreateInstance<PortalUser>();
                user.Email = email;
                user.Name = name;
                user.Address = address;
                user.BackgroundCheckStatusCompleted = backgroundCheckStatusCompleted;
                user.SecurityClearance = securityClearance;
                user.DepartmentOfDefenseContractorNumber = departmentOfDefenseContractorNumber;
                user.USFederalContractorRegistrationNumber = usFederalContractorRegistrationNumber;

                return user;
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(PortalUser)}'. " +
                    $"Ensure that '{nameof(PortalUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<PortalUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<PortalUser>)_userStore;
        }
    }
}
