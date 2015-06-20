using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using App360SDK.Payment;
using System.Diagnostics;

namespace App360Sample
{
    public partial class PurchasePage : PhoneApplicationPage
    {
        const string IMAGE_LINK = "https://lh6.ggpht.com/4LIsmteJ3JoIjvlgLxdvLEVnYV5yWq4gNauyuWgZDBMVUGs6w326r9S_f_LlyX2P-iA=w300";
        MOGPaymentSDK paymentSDK;
        public PurchasePage()
        {
            InitializeComponent();
            paymentSDK = new MOGPaymentSDK();
        }

        private async void PhongCardButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("\n\n" + "*** Payment with Phone Card");
            var transaction = await paymentSDK.MakePhoneCardTransaction(MOGCardVendor.VINAPHONE, "<CardCode>", "<Card Serial>", "payload");
            TransactionIdTextBox.Text = transaction.TransactionId;
            WriteToLog(transaction);
        }

        private async void SMButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("\n\n" + "*** Payment with SMS");
            var transaction = await paymentSDK.GetSMSSyntax("yourpayload", "1000, 15000, 500", Telecom.VIETTEL);
            WriteToLog(transaction);
        }

        private async void BankingButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("\n\n" + "*** Payment by Banking");
            MOGBankingTransaction transaction = await paymentSDK.MakeBankingTransaction("10000");
            WriteToLog(transaction);
        }


        private void WriteToLog(MOGTransaction transaction)
        {

            if (transaction.IsSuccess)
            {
                Debug.WriteLine("\nDone: \n` TransactionId: {0}\n` Status: {1}", transaction.TransactionId, transaction.Status);
                if (transaction is MOGBankingTransaction)
                {

                    var mbt = (MOGBankingTransaction)transaction;
                    Debug.WriteLine("\n` PayUrl: {0}", mbt.PaymentUrl);
                }
                else if (transaction is MOGPhoneCardTransaction)
                {

                    var mpt = (MOGPhoneCardTransaction)transaction;
                    Debug.WriteLine("\n` Thẻ: {0}\n` PIN: {1}\n` SERIAL: {2}", mpt.CardType, mpt.Pin, mpt.Serial);

                }
                else if (transaction is MOGSMSTransaction)
                {

                    var mst = (MOGSMSTransaction)transaction;
                    Debug.WriteLine("\n` SMS: {0}\n` To: {1}\n` Amount: {2}", mst.Syntax, mst.SMSItems.First().To, mst.SMSItems.First().Amount);
                }
                else if (transaction is MOGStatusTransaction)
                {
                    var mstt = (MOGStatusTransaction)transaction;
                    foreach (var key in mstt.Details)
                    {
                        Debug.WriteLine("\n` {0}: {1}", key.Key.ToString(), key.Value.ToString());
                    }

                }
            }
            else
            {
                Debug.WriteLine("\nError\n:" + transaction.ErrorInfo);
            }


        }

        async private void Button_Click(object sender, RoutedEventArgs e)
        {
            MOGStatusTransaction checkStatus = await paymentSDK.CheckStatusOfTransaction(TransactionIdTextBox.Text);
            WriteToLog(checkStatus);
        }







    }
}