using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace L13.Models
{
   public class Movie
   {
      // TODO: L13 Task 06 - Provide suitable validation attributes

      public int MovieId { get; set; }
      [Required(ErrorMessage = "Enter a Title")]
      [StringLength(50, ErrorMessage = "Maximum is 50 characters")]
      public string Title { get; set; }

      [Required(ErrorMessage = "Enter the release date")]
      [DataType(DataType.Date)]
      public DateTime ReleaseDate { get; set; }
      [Range(0,50, ErrorMessage ="0-50 dollars")]
      public double Price { get; set; }
      [Range(20, 400, ErrorMessage = "20-400 minutes")]
      public double Duration { get; set; }
      [Required(ErrorMessage = "Enter the release date")]
      [RegularExpression("G|PG(13)?|R", ErrorMessage ="Must be G, PG, PG13 or R")]
      public string Rating { get; set; }

      public int GenreId { get; set; }

      public string GenreName { get; set; }
   }
}
//20031509 Thng Yu Yang