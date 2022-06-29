using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace L12.Models
{
   public class Cadet
   {
      [Required(ErrorMessage = "Please enter Cadet No")]
      public string CNo { get; set; }

      [Required(ErrorMessage = "Please enter Class Group")]
      [RegularExpression("C[12][1-3][A-C]", ErrorMessage ="Invalid format")]
      public string ClassGrp { get; set; }

      [Required(ErrorMessage = "Please enter Cadet Name")]
      [StringLength(40, ErrorMessage = "Max 40 chars")]
      public string CName { get; set; }

      [Required(ErrorMessage = "Please enter Shooting Score")]
      [Range(0, 100, ErrorMessage = "0-100 marks")]
      public int Shooting { get; set; }

      [Required(ErrorMessage = "Please enter Fitness Score")]
      [Range(0, 100, ErrorMessage = "0-100 marks")]
      public int Fitness { get; set; }

      [Required(ErrorMessage = "Please enter Exam Score")]
      [Range(0, 100, ErrorMessage = "0-100 marks")]
      public int Exam { get; set; }

   }
}
