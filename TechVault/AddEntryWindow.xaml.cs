using System.Windows;

namespace TechVault
{

   public partial class AddEntryWindow : Window
   {
      private readonly Database _database;

      public AddEntryWindow( Database database )
      {
         InitializeComponent();

         _database = database;

         NewEntryGroupCombo.ItemsSource = database.Groups;
      }

      public void AddEntry( object sender, RoutedEventArgs e )
      {
         Entry entry = new Entry();
         Group selectedGroup = (Group) NewEntryGroupCombo.SelectedItem;

         entry.Title = NewEntryTitle.Text;
         entry.Username = NewEntryUsername.Text;
         entry.Password = NewEntryPassword.Text;
         entry.GroupName = selectedGroup.Name;
         entry.Comment = NewEntryComment.Text;

         _database.Entries.Add( entry );

         this.Close();
      }

      public void OpenPasswordGenerator( object sender, RoutedEventArgs e )
      {
         PasswordGeneratorWindow window = new PasswordGeneratorWindow(NewEntryPassword);
         window.Show();
      }

   }
}
