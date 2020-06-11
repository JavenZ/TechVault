using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TechVault
{
   public partial class AddGroupWindow : Window
   {
      private Database _database;

      public AddGroupWindow(Database database)
      {
         InitializeComponent();

         _database = database;

      }

      public void AddGroup( object sender, RoutedEventArgs e )
      {
         Group newGroup = new Group(NewGroupName.Text);

         _database.Groups.Add( newGroup );

         this.Close();
      }

   }
}
