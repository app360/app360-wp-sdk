using App360SDK.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App360Sample
{
    class MyAmountsConverter : IAmountConverter
    {
        public string CardAmountToString(int amount)
        {
            switch (amount)
            {
                case 50000:
                    return "50000 coins";
                case 100000:
                    return "1000 coins";
                case 200000:
                    return "200000 coins";
                default:
                    return "50000000 coins";
            }
        }

        public string BankAmountToString(int amount)
        {
            switch (amount)
            {
                case 50000:
                    return "Teemo";
                case 100000:
                    return "Darven";
                case 200000:
                    return "MAOKAI";
                default:
                    return "Lux";
            }
        }

        public string SmsAmountToString(MOGPaymentSDK.SMS_AMOUNT amount)
        {
            switch (amount)
            {
                case MOGPaymentSDK.SMS_AMOUNT.AMOUNT_500:
                    return "500 xu";
                case MOGPaymentSDK.SMS_AMOUNT.AMOUNT_1000:
                    return "1000 xu";
                case MOGPaymentSDK.SMS_AMOUNT.AMOUNT_2000:
                    return "200000 xu";
                case MOGPaymentSDK.SMS_AMOUNT.AMOUNT_3000:
                    return "3000 xu";
                case MOGPaymentSDK.SMS_AMOUNT.AMOUNT_4000:
                    return "400000 xu";
                case MOGPaymentSDK.SMS_AMOUNT.AMOUNT_5000:
                    return "5000 xu";
                case MOGPaymentSDK.SMS_AMOUNT.AMOUNT_10000:
                    return "10000 xu";
                case MOGPaymentSDK.SMS_AMOUNT.AMOUNT_15000:
                    return "15000 xu";
                default:
                    return "500 xu";
            }
        }
    }
}
