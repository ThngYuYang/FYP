using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace L10.Models
{
   public class LemonUser
   {
      // TODO: L10 Task 1 - Fill in the blanks with the appropriate validation attributes
      //                    Remember to uncomment those lines with validation attributes
      
      [Required(ErrorMessage = "Please enter User ID")]
      [Remote(action: "VerifyUserID", controller: "Account")]
      public string UserId { get; set; }

      [Required(ErrorMessage = "Please enter Password")]
      [StringLength(20, MinimumLength = 5, ErrorMessage = "Password must be 5 characters or more")]
      public string UserPw { get; set; }

      [Compare("UserPw", ErrorMessage = "Passwords do not match")]
      public string UserPw2 { get; set; }

      [Required(ErrorMessage = "Please enter Full Name")]
      public string FullName { get; set; }

      [Required(ErrorMessage = "Please enter Email")]
      [EmailAddress(ErrorMessage = "Invalid Email")]
      public string Email { get; set; }

      public string UserRole { get; set; }
      public DateTime LastLogin { get; set; }
   }
}
// 20031509 Thng Yu Yang