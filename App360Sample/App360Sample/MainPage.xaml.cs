﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using App360Sample.Resources;
using App360SDK;
using System.Diagnostics;

namespace App360Sample
{
    public partial class MainPage : PhoneApplicationPage
    {
       
        public MainPage()
        {
            InitializeComponent();

           
            this.Loaded += MainPage_Loaded;
        }

        async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
           

            MOGSession session;
            if (MOGSessionManager.ActiveSession == null)
            {
                String scopedId = "your-user-id";
                session = await MOGSessionManager.OpenActiveSessionWithScopeIdAsync(scopedId, null);

                if (session.IsSuccess)
                {
                    Debug.WriteLine("\n -Đăng nhập thành công \n SessionInfo: \n" + session.ToString());

                    MOGScopedUser currentUser = MOGScopedUser.CurrentUser;
                    Debug.WriteLine("\n -Current user id: \n" + currentUser.ScopeId);
                }
                else
                {
                    Debug.WriteLine("\n -Lỗi \n" + session.Error + "Đăng nhập không thành công");
                }
            }

        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginButton.IsEnabled = false;
            MOGSession sessionInfo;
            sessionInfo = await MOGSessionManager.OpenActiveSessionWithScopeIdAsync(null, null);

            if (sessionInfo.IsSuccess)
            {
                Debug.WriteLine("\n -Đăng nhập thành công \n SessionInfo: \n" + sessionInfo.ToString());
                MessageBox.Show("\n -Đăng nhập thành công \n SessionInfo: \n" + sessionInfo.ToString());
            }
            else
            {

                MessageBox.Show("\n -Lỗi \n" + sessionInfo.Error, "Đăng nhập không thành công", MessageBoxButton.OK);
            }
            LoginButton.IsEnabled = true;
        }


    }
}