﻿#pragma checksum "E:\1_M360 SDK for Windows phone\1_Source Code\Sample Project\App360Sample\App360Sample\PurchasePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7C52B461D84601D9E7D35BAB34E44C95"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace App360Sample {
    
    
    public partial class PurchasePage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.StackPanel LayoutRoot;
        
        internal System.Windows.Controls.Button PhongCardButton;
        
        internal System.Windows.Controls.Button SMButton;
        
        internal System.Windows.Controls.Button BankingButton;
        
        internal System.Windows.Controls.TextBox TransactionIdTextBox;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/App360Sample;component/PurchasePage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.StackPanel)(this.FindName("LayoutRoot")));
            this.PhongCardButton = ((System.Windows.Controls.Button)(this.FindName("PhongCardButton")));
            this.SMButton = ((System.Windows.Controls.Button)(this.FindName("SMButton")));
            this.BankingButton = ((System.Windows.Controls.Button)(this.FindName("BankingButton")));
            this.TransactionIdTextBox = ((System.Windows.Controls.TextBox)(this.FindName("TransactionIdTextBox")));
        }
    }
}

