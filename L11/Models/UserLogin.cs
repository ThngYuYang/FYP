using System.ComponentModel.DataAnnotations;

namespace L11.Models
{
   public class UserLogin
   {
      [Required(ErrorMessage = "Please enter User ID")]
      public string UserID { get; set; }

      [Required(ErrorMessage = "Please enter Password")]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      // TODO: L11 Task 1b - Create a boolean property named RememberMe
      public bool RememberMe { get; set; }
   }
}
// 20031509 Thng Yu Yang