using System.ComponentModel.DataAnnotations;

namespace L12.Models
{
   public class Poll
   {
      public string PollGUID { get; set; }

      [Required(ErrorMessage = "Mandatory")]
      [StringLength(200, ErrorMessage = "Max 200 chars")]
      public string Question { get; set; }

      [Required(ErrorMessage = "Mandatory")]
      [StringLength(50, ErrorMessage = "Max 50 chars")]
      public string ChoiceA { get; set; }

      [Required(ErrorMessage = "Mandatory")]
      [StringLength(50, ErrorMessage = "Max 50 chars")]
      public string ChoiceB { get; set; }

      public int CountA { get; set; }
      public int CountB { get; set; }

   }
}
