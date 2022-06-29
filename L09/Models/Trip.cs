using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace L09.Models
{
   public class Trip
   {
      public int Id { get; set; }

      [Required(ErrorMessage = "Please enter Title")]
      [StringLength(100, ErrorMessage = "Max 100 chars")]
      public string   Title { get; set; }

      [Required(ErrorMessage = "Please enter City")]
      [StringLength(70, ErrorMessage = "Max 70 chars")]
      public string City { get; set; }

      [Required(ErrorMessage = "Please enter Date")]
      [DataType(DataType.Date)]
      public DateTime TripDate { get; set; }

      [Range(1,365, ErrorMessage = "1-365 days")]
      public int Duration { get; set; }

      [Range(0, 100000, ErrorMessage = "1-100K dollars")]
      public double Spending { get; set; }

      [Required(ErrorMessage = "Please enter Story")]
      [StringLength(2000, ErrorMessage = "Max 2000 chars")]
      public string Story { get; set; }

      [Required(ErrorMessage = "Please select Photo")]
      public IFormFile Photo { get; set; }

      public string Picture { get; set; }

      public string SubmittedBy { get; set; }

   }
}

