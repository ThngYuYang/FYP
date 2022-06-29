using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L06.Models
{
   public class OrgProduct
   {
      public int OrgCode { get; set; }
      public string OrgDesc { get; set; }
      public double Price { get; set; }
      public int Gram  { get; set; }
      public string Country { get; set; }
   }
}
