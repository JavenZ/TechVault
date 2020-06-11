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
using System.Windows.Navigation;
using Microsoft.Win32;
using System.IO;
using System.Collections;
using Newtonsoft.Json;

namespace TechVault
{

   public partial class LaunchDatabaseWindow : Window
   {

      public LaunchDatabaseWindow()
      {
         InitializeComponent();
         InitializeConfiguration();
      }

      private void InitializeConfiguration()
      {
         if ( File.Exists( "config.txt" ) )
         {
            Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Config.CONFIG_PATH));
            ExistingDatabasePath.Text = config.DefaultDatabasePath;
         }
         else
         {
            Config config = new Config();
            File.WriteAllText( Config.CONFIG_PATH, JsonConvert.SerializeObject( config ) );
         }
      }

      private void ExistingDatabasePathChanged( object sender, RoutedEventArgs e )
      {
         Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Config.CONFIG_PATH));
         config.DefaultDatabasePath = ExistingDatabasePath.Text;
         File.WriteAllText( Config.CONFIG_PATH, JsonConvert.SerializeObject( config ) );
      }

      private void SearchDatabaseFileExisting( object sender, RoutedEventArgs e )
      {
         OpenFileDialog openFileDialog = new OpenFileDialog();
         openFileDialog.Filter = "Database files (*.tspd)|*.tspd";
         if ( openFileDialog.ShowDialog() == true )
         {
            ExistingDatabasePath.Text = openFileDialog.FileName;
         }
      }

      private void SearchDatabaseFileCreate( object sender, RoutedEventArgs e )
      {
         SaveFileDialog saveFileDialog = new SaveFileDialog();
         saveFileDialog.Filter = "Database files (*.tspd)|*.tspd";
         if ( saveFileDialog.ShowDialog() == true )
         {
            NewDatabasePath.Text = saveFileDialog.FileName;
         }
      }

      private void StartDatabase( object sender, RoutedEventArgs e )
      {
         if (ExistingDatabaseRadio.IsChecked == true)
         {
            //Selecting existing Database file
            if ( File.Exists( ExistingDatabasePath.Text ) )
            {
               using ( StreamReader sr = File.OpenText( ExistingDatabasePath.Text ) )
               {
                  Database database = JsonConvert.DeserializeObject<Database>(sr.ReadToEnd());
                  if (database.Password.Equals( ExistingDatabasePassword.Password ) )
                  {
                     OpenedDatabaseWindow win = new OpenedDatabaseWindow(ExistingDatabasePath.Text, ExistingDatabasePassword.Password);
                     win.Show();
                     this.Close();
                  } else
                  {
                    MessageBox.Show("Incorrect password", "Error");
                  }
               }
            } else
            {
               MessageBox.Show( "Select valid database file", "Error" );
            }
         } else
         {
            //Creating new Database file
            if (!File.Exists( NewDatabasePath.Text ) )
            {
               using (StreamWriter sw = File.CreateText(NewDatabasePath.Text) )
               {
                  Database database = new Database();
                  database.Password = NewDatabasePassword.Password;

                  database.Groups.Add( new Group( "All" ) );
                  database.Groups.Add( new Group( "Social" ) );
                  database.Groups.Add( new Group( "Work" ) );
                  database.Groups.Add( new Group( "Productivity" ) );

                  database.Entries.Add( new Entry( "Personal Email", "john.smith@gmail.com", "password", "This is the entry for my personal email!", "Productivity") );
                  sw.Write(JsonConvert.SerializeObject( database ));
               }

               OpenedDatabaseWindow win = new OpenedDatabaseWindow(NewDatabasePath.Text, NewDatabasePassword.Password);
               win.Show();
               this.Close();
            }
            else
            {
               MessageBox.Show( "Database file already exists", "Error" );
            }
         }
      }
   }
}
