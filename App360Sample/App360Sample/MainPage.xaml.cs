using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using App360Sample.Resources;
using App360SDK.AppScopedID;
using System.Diagnostics;

namespace App360Sample
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor

        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginButton.IsEnabled = false;
            MOGSession sessionInfo;
            sessionInfo = await MOGSessionManager.OpenActiveSessionWithScopeIdAsync(null,null);

            if (sessionInfo.IsSuccess)
            {
                Debug.WriteLine("\n -Đăng nhập thành công \n SessionInfo: \n" + sessionInfo.ToString());
                App.RootFrame.Navigate(new Uri("/PurchasePage.xaml", UriKind.RelativeOrAbsolute));
            }
            else
            {

                MessageBox.Show("\n -Lỗi \n" + sessionInfo.Error, "Đăng nhập không thành công", MessageBoxButton.OK);
            }
            LoginButton.IsEnabled = true;
        }


    }
}