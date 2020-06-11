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
   public partial class PasswordGeneratorWindow : Window
   {
      private TextBox _password;

      public PasswordGeneratorWindow(TextBox password)
      {
         InitializeComponent();

         _password = password;
      }

      public void GeneratePassword( object sender, RoutedEventArgs e )
      {
         int length = int.Parse( PasswordGenLength.Text);
         bool useUpperCase = (bool) PasswordGenUseUpperCase.IsChecked;
         bool useLowerCase = (bool) PasswordGenUseLowerCase.IsChecked;
         bool useNumeric = (bool) PasswordGenUseNumeric.IsChecked;
         bool useSpecial = (bool) PasswordGenUseSpecial.IsChecked;

         StringBuilder builder = new StringBuilder();
         Random random = new Random();

         while (builder.Length <= length)
         {
            int type = random.Next(0, 4);
            switch ( type )
            {
               case 0:
                  if (useUpperCase)
                  {
                     char upper = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                     builder.Append( upper );
                  }
                  break;
               case 1:
                  if ( useLowerCase )
                  {
                     char lower = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 97)));
                     builder.Append( lower );
                  }
                  break;
               case 2:
                  if ( useNumeric )
                  {
                     char num =  Convert.ToChar(48 + random.Next(0, 9));

                     builder.Append( num );
                  }
                  break;
               case 3:
                  if ( useSpecial )
                  {
                     char special = Convert.ToChar(Convert.ToInt32(Math.Floor(14 * random.NextDouble() + 33)));
                     builder.Append( special );
                  }
                  break;
            }

            PasswordGenText.Text = builder.ToString();
         }

      }

      public void SavePassword(object sender, RoutedEventArgs e)
      {
         _password.Text = PasswordGenText.Text;
         this.Close();
      }
   }
}
