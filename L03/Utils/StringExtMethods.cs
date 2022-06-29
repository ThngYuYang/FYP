using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Reflection;

public static class StringExtMethods
{
   // TODO: L03 TASK 1 Add the Stretch extension method
        public static string Stretch(this String str)
        {
            string newstr = "";
            for(int i = 0; i < str.Length; i++)
            {
                newstr += str.Substring(i, 1) + " ";
            }
            return newstr.ToUpper();
        }

    // Extension Method to uppercase and lowercase 
    // alternate characters in a String
    public static string UpperLower(this String str)
   {
      string newstr = "";
      for (int i = 0; i < str.Length; i++)
      {
         if (i % 2 == 0)
            newstr += str.Substring(i, 1).ToUpper();
         else
            newstr += str.Substring(i, 1).ToLower();
      }
      return newstr;
   }

}
//20031509 Thng Yu Yang