using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FYP.Models
{
   public class Score
   {

      public int Id { get; set; }
      [Required(ErrorMessage = "Enter a student name")]
      [StringLength(50, ErrorMessage = "Maximum is 50 characters")]
      public string StudentName{ get; set; }

      [Required(ErrorMessage = "Enter the test score")]
      [Range(0,100, ErrorMessage ="the score range is 0-100")]
      public int TestScore { get; set; }
   }
}