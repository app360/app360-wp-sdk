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
using App360SDK.Payment.Form;

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

        private void PurchaseButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PaymentForm paymentForm = new PaymentForm(App.RootFrame);
            // initialize SMS Amount
            List<MOGPaymentSDK.SMS_AMOUNT> smsAmounts = new List<MOGPaymentSDK.SMS_AMOUNT>();
            smsAmounts.Add(MOGPaymentSDK.SMS_AMOUNT.AMOUNT_1000);
            smsAmounts.Add(MOGPaymentSDK.SMS_AMOUNT.AMOUNT_10000);
            smsAmounts.Add(MOGPaymentSDK.SMS_AMOUNT.AMOUNT_15000);
            // initialize banking
            List<int> bankingAmounts = new List<int>();
            bankingAmounts.Add(50000);
            bankingAmounts.Add(100000);
            bankingAmounts.Add(150000);
            // initialize cards
            List<int> cardAmounts = new List<int>();
            cardAmounts.Add(50000);
            cardAmounts.Add(100000);
            cardAmounts.Add(150000);
            // create an instance
            paymentForm = new PaymentForm(App.RootFrame)
            {
                BankingAmounts = bankingAmounts,
                CardAmounts = cardAmounts,
                SmsAmounts = smsAmounts,
                AppTitle = "Swing copters",
                AppDescription = ".Gear Studio",
                Payload = "payload",
                AppImagePath = "/Assets/ApplicationIcon.png",
                AmountConverter = new MyAmountsConverter()
            };
            // register events
            paymentForm.OnCardTransaction += paymentForm_OnCardCharged;
            paymentForm.OnBankingTransaction += paymentForm_OnBankingTransaction;
            paymentForm.OnSMSTransaction += paymentForm_OnSMSTransaction;
            // Show
            paymentForm.Show();
        }

        void paymentForm_OnSMSTransaction(object sender, SMSTransEventArgs args)
        {
            string result = string.Format("\nTo: {0}, Amount: {1}, Syntax:{2}", args.ToNumber, args.Amount, args.Syntax);
            Debug.WriteLine(result);
        }

        void paymentForm_OnBankingTransaction(object sender, BankingTransEventArgs args)
        {
            string result = string.Format("\ntransaction id: {0},\nurl:{1}", args.TransactionId, args.PayUrl);
            Debug.WriteLine(result);
        }

        void paymentForm_OnCardCharged(object sender, CardTransEventArgs args)
        {
            if (args.TransactionDetail.IsSuccess)
            {
                MessageBox.Show(args.TransactionDetail.Status);
            }
            else
            {
                MessageBox.Show(args.TransactionDetail.ErrorInfo);
            }
        }

    }
}