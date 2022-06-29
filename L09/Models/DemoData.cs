using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace L09.Models
{
   public class DemoData
   {
      [Required(ErrorMessage = "Please Enter Date Field A")]
      [DataType(DataType.Date)]
      public DateTime DateFieldA { get; set; }

      [Required(ErrorMessage = "Please Enter Date Field B")]
      [DataType(DataType.DateTime)]
      public DateTime DateFieldB { get; set; }

      [Required(ErrorMessage = "Please Enter Email Field")]
      [EmailAddress(ErrorMessage = "Invalid Email")]
      public string EmailField { get; set; }

      [Range(13, 19, ErrorMessage = "Thirteen to Nineteen")]
      public int Teenager { get; set; }

      [Required(ErrorMessage = "Please Enter Product Code")]
      [StringLength(12, MinimumLength=6, ErrorMessage = "6-12 chars")]
      public string ProductCode { get; set; }
   }
}

