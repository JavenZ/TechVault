using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using Newtonsoft.Json;

namespace TechVault
{
   public partial class OpenedDatabaseWindow : Window
   {
      private readonly string _path;

      private readonly string _password;

      private Database _database;

      public OpenedDatabaseWindow(string path, string password)
      {
         InitializeComponent();

         _path = path;
         _password = password;
         _database = initializeDatabase();

         EntryGroupsList.ItemsSource = _database.Groups;
         EntryGroupsList.SelectedItem = _database.Groups[0];
         EntryList.ItemsSource = _database.Entries;
      }

      private void SelectedGroupChanged( object sender, RoutedEventArgs e )
      {
         List<Entry> FilteredEntires = new List<Entry>();
         Group selected = (Group) EntryGroupsList.SelectedItem;

         foreach (Entry entry in _database.Entries)
         {
            if (entry.GroupName.Equals(selected.Name) || selected.Name.Equals("All") )
            {
               FilteredEntires.Add( entry );
            }
         }
         EntryList.ItemsSource = FilteredEntires;
      }

      private Database initializeDatabase()
      {
         using ( StreamReader sr = File.OpenText( _path ) )
         {
            Database database = JsonConvert.DeserializeObject<Database>(sr.ReadToEnd());

            return database;
         }
      }

      private void AddEntry( object sender, RoutedEventArgs e )
      {
         AddEntryWindow window = new AddEntryWindow(_database);
         window.Show();
         window.Closed += new EventHandler( UpdateDatabase );
      }

      private void AddGroup( object sender, RoutedEventArgs e)
      {
         AddGroupWindow window = new AddGroupWindow(_database);
         window.Show();
         window.Closed += new EventHandler( UpdateDatabase );
      }

      private void EditEntry( object sneder, RoutedEventArgs e )
      {
         Entry selectedEntry = (Entry) EntryList.SelectedItem;
         if (selectedEntry != null)
         {
            EditEntryWindow window = new EditEntryWindow(selectedEntry, _database.Groups);
            window.Show();
            window.Closed += new EventHandler( UpdateDatabase );
         }
      }

      private void UpdateDatabase( object sender, EventArgs e )
      {
         EntryList.Items.Refresh();
         EntryGroupsList.Items.Refresh();
         File.WriteAllText(_path, JsonConvert.SerializeObject( _database ) );
      }

   }
}
