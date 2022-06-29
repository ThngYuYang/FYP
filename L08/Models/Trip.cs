using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace L08.Models
{
   public class Trip
   {
      // TODO L08 Task 3 - Specify [Required] for some properties

      public int Id { get; set; }
      [Required(ErrorMessage = "Please enter Title")]
      public string Title { get; set; }
      [Required(ErrorMessage = "Please enter City Name")]
      public string City { get; set; }

      public DateTime TripDate { get; set; }

      public int Duration { get; set; }

      public double Spending { get; set; }
      [Required(ErrorMessage = "Please enter Story")]
      public string Story { get; set; }
      [Required(ErrorMessage = "Please submit Photo")]
      public IFormFile Photo { get; set; }

      public string Picture { get; set; }
      public string SubmittedBy { get; set; }    
   }
}
// 20031509 Thng Yu Yang

