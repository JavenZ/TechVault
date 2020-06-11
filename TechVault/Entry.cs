using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechVault
{
   public class Entry
   {
      public Entry()
      {

      }

      public Entry(string title, string username, string password, string comment, string groupName)
      {
         Title = title;
         Username = username;
         Password = password;
         Comment = comment;
         GroupName = groupName;
      }

      public string Title { get; set; }

      public string Username { get; set; }

      public string Password { get; set; }

      public string Comment { get; set; }

      public string GroupName { get; set; }

   }
}
