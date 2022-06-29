using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace L09.Models
{
   public class TravelUser
   {
      [Required(ErrorMessage = "Please enter User ID")]
      public string UserId { get; set; }

      [Required(ErrorMessage = "Please enter Password")]
      [StringLength(20, MinimumLength = 5, ErrorMessage = "Password must be 5-20 char")]
      [DataType(DataType.Password)]
      public string UserPw { get; set; }

      [Compare("UserPw", ErrorMessage = "Passwords do not match")]
      [DataType(DataType.Password)]
      public string UserPw2 { get; set; }

      [Required(ErrorMessage = "Please enter Full Name")]
      public string FullName { get; set; }

      [Required(ErrorMessage = "Please enter Birthdate")]
      public DateTime Dob { get; set; }

      [Required(ErrorMessage = "Please enter Email")]
      [EmailAddress(ErrorMessage = "Invalid Email")]
      public string Email { get; set; }

      public string UserRole { get; set; }

      public DateTime LastLogin { get; set; }

   }
}
