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
        MOGPaymentSDK paymentSDK;
        public PurchasePage()
        {
            InitializeComponent();
            paymentSDK = new MOGPaymentSDK();
        }

        private async void PhongCardButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("\n\n" + "*** Payment with Phone Card");
            var transaction = await paymentSDK.MakePhoneCardTransaction(MOGCardVendor.VINAPHONE, "xxxxxxxxxx", "xxxxxxxxx", "abcpayload");
            WriteToLog(transaction);
        }

        private async void SMButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("\n\n" + "*** Payment with SMS");
            var transaction = await paymentSDK.GetSMSSyntax("yourpayload", MOGPaymentSDK.SMS_AMOUNT.AMOUNT_10000, MOGPaymentSDK.SMS_AMOUNT.AMOUNT_15000);
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
                        Debug.WriteLine("\n` {0}: {1}", key.Key, key.Value);
                    }

                }
            }
            else
            {
                Debug.WriteLine("\nError\n:" + transaction.ErrorInfo);
            }


        }
    }
}