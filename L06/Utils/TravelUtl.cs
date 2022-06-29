using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class TravelUtl
{
   public static string Abbreviate(this string story)
   {
      // TODO: L06 Task 1 - Abbreviate the Story
        if(story.Length < 30)
        {
            return story;
        }
        else
        {
            return story.Substring(0, 20) + "...";
        }
   }
}
// 20031509 Thng Yu Yang
