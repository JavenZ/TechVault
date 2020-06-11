using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechVault
{
   public class Config
   {
      public const string CONFIG_PATH = "config.txt";

      public string DefaultDatabasePath { get; set; } = "";
   }
}
