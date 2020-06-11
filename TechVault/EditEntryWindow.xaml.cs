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
   public partial class EditEntryWindow : Window
   {
      private Entry _entry;

      public EditEntryWindow(Entry entry, List<Group> groups)
      {
         InitializeComponent();

         _entry = entry;

         EditEntryTitle.Text = entry.Title;
         EditEntryUsername.Text = entry.Username;
         EditEntryPassword.Text = entry.Password;
         EditEntryComment.Text = entry.Comment;

         EditEntryGroupCombo.ItemsSource = groups;
         foreach (Group g in groups)
         {
            if (entry.GroupName.Equals( g.Name )) 
            {
               EditEntryGroupCombo.SelectedItem = g;
            }
         }
      }

      public void OpenPasswordGenerator( object sender, RoutedEventArgs e)
      {
         PasswordGeneratorWindow window = new PasswordGeneratorWindow(EditEntryPassword);
         window.Show();
      }

      public void SaveEntry(object sender, RoutedEventArgs e )
      {
         Group group = (Group) EditEntryGroupCombo.SelectedItem;

         _entry.Title = EditEntryTitle.Text;
         _entry.Username = EditEntryUsername.Text;
         _entry.Password = EditEntryPassword.Text;
         _entry.Comment = EditEntryComment.Text;
         _entry.GroupName = group.Name;

         this.Close();
      }
   }
}
