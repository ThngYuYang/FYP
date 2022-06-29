using System;

namespace L06.Models
{
   public class Trip
   {
      public int      ID { get; set; }
      public string   Title { get; set; }
      public string   City { get; set; }
      public DateTime TripDate { get; set; }
      public int      Duration { get; set; }
      public double   Spending { get; set; }
      public string   Story { get; set; }
      public string   PhotoFile { get; set; }
   }
}

