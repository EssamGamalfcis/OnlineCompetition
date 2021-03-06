using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using AdminPanel.Models;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace OnlineCompetition.MVC.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _hostEnvironment;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "الاسم الاول")]
            public string FirstName { get; set; }
            [Required]
            [Display(Name = "الاسم الاخير")]
            public string LastName { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "البريد الالكترونى")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "يجب ان يتكون الرقم السرى من حروف وارقام واشكال مميزة.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "الرقم السرى")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "تأكيد الرقم السرى")]
            [Compare("Password", ErrorMessage = "غير متطابق.")]
            public string ConfirmPassword { get; set; }
            [Display(Name = "الصورة الشخصية")]
            public string ImagePath { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                MailAddress address = new MailAddress(Input.Email);
                string userName = address.User;
                if (file != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    Input.ImagePath = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/img/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                }
                var user = new ApplicationUser 
                { 
                    UserName = userName, 
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    ImagePath = Input.ImagePath
                };
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
             _logger.LogInformation("User created a new account with password.");
             //await _userManager.AddToRoleAsync(user, Enums.Roles.Teacher.ToString());
             var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
             code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
             var callbackUrl = Url.Page(
                 "/Account/ConfirmEmail",
                 pageHandler: null,
                 values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                 protocol: Request.Scheme);

             await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                 $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

             if (_userManager.Options.SignIn.RequireConfirmedAccount)
             {
                 return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
             }
             else
             {
                 //await _signInManager.SignInAsync(user, isPersistent: false);
                 return LocalRedirect(returnUrl);
             }
            }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
