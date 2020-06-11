using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechVault
{
   public class Database
   {
      public string Password { get; set; }

      public List<Entry> Entries { get; set; } = new List<Entry>();

      public List<Group> Groups { get; set; } = new List<Group>();

   }
}
