using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class UserService 
    {
        //public async bool  AuthenticateUser(User user)
        //{
        //    if (user.UserName == "tuanle@gmail.com" && user.Password == "123456")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        //{
        //    ReturnUrl = returnUrl;

        //    if (ModelState.IsValid)
        //    {
        //        // Use Input.Email and Input.Password to authenticate the user
        //        // with your custom authentication logic.
        //        //
        //        // For demonstration purposes, the sample validates the user
        //        // on the email address maria.rodriguez@contoso.com with 
        //        // any password that passes model validation.

        //        var user = await AuthenticateUser(Input.Email, Input.Password);

        //        if (user == null)
        //        {
        //            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //            return Page();
        //        }

        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, user.Email),
        //            new Claim("FullName", user.FullName),
        //            new Claim(ClaimTypes.Role, "Administrator"),
        //        };

        //        var claimsIdentity = new ClaimsIdentity(
        //            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //        await HttpContext.SignInAsync(
        //            CookieAuthenticationDefaults.AuthenticationScheme,
        //            new ClaimsPrincipal(claimsIdentity),
        //            authProperties);

        //        _logger.LogInformation("User {Email} logged in at {Time}.",
        //            user.Email, DateTime.UtcNow);

        //        return LocalRedirect(Url.GetLocalUrl(returnUrl));
        //    }

        //    // Something failed. Redisplay the form.
        //    return Page();
        //}
    }
}