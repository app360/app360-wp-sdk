Ngôn ngữ khác: [English](https://github.com/app360/app360-wp-sdk/blob/master/README.md)
#Giới thiệu
App360SDK cung cấp cách thức đơn giản nhất để quản lý user và tích hợp thanh toán (bao gồm sms, thẻ điện thoại và e-banking) vào ứng dụng của bạn.

Sơ đồ luồng tích hợp App360 SDK

![App360 SDK 's Integration Flow](https://camo.githubusercontent.com/c1f062a8627f539dbb4d1668f712d1c675b17142/687474703a2f2f692e696d6775722e636f6d2f505864436d62332e706e67)

>Bản App360SDK chỉ hỗ trợ cho Windows Phone 8 and 8.1 silverlight project.

#Bắt đầu với Demo project

Đầu tiên, clone hoặc download ví dụ về máy.

 - `git clone https://github.com/app360/app360-wp-sdk.git`
 - or, download from https://github.com/app360/app360-wp-sdk



#Thêm thư viện App360SDK vào trong project

##Sử dụng [Nuget](https://www.nuget.org/packages/App360SDK) (Khuyến nghị)
Vào Tools -> Nuget Package Manager -> Package Manager Console

`Install-Package App360SDK`

##Thêm bằng tay
![enter image description here](https://lh3.googleusercontent.com/yOX4kN46CQYG59dE4qRCUp8311ySvqMlGnNepRse2Q=s0 "app360project.png")
1. Tải project demo trên https://github.com/app360/app360-wp-sdk.
2. Thêm thư viện vào trong project(RightClick to your project -> Add -> Reference)
	    chọn App360SDK.dll tư thư mục [/release](https://github.com/app360/app360-wp-sdk/tree/master/Release) 
	
3. Tạo một file xm mới với tên app360.xml vào project với nội dung như sau:
	```xml
	<?xml version="1.0" encoding="utf-8" ?>
	<config>
	  <channel>mwork</channel>
	  <subchannel></subchannel>
	</config>
	```


4. Tạo thư mục vi-VN vào project của bạn và copy vào trong file App360SDK.resources.dll từ thư mục /release/vi-VN mà bạn đã download về.
5. Tiếp tục thêm các file trong folder Payment/Form/Images vào project
6. Vào Tools -> Nuget Package Manager -> Package Manager Console
	    Cài đặt các thư viện sau:
	`Install-Package FubarCoder.RestSharp.Portable`
	`Install-Package WPtoolkit`
 


#Tích hợp ứng dụng của bạn với App360 SDK cho windows phone

##Tài liệu
 - Getting Started Guide: this README
 
##Channeling
Thay đổi cấu hình của channel và subchannel trong app360.xml

![enter image description here](https://lh3.googleusercontent.com/0v-0aZOiaDpL0ndmXav9WW9J5d1E9VGKtDph_VqH_Q=s0 "m360Config.png")

## Application Id & Secret Key
Để sử dụng các hàm của App360SDk, bạn cần điền *Application Id* and *Secret key* dùng để xác thực  app(game) của bạn với App360's server.

Go to https://developers.app360.vn/ to create your application.
![enter image description here](https://camo.githubusercontent.com/165a524062d77d5e9205d159f24268cee4c114d5/687474703a2f2f692e696d6775722e636f6d2f34785a386659632e706e67)
bạn có thể lấy **Application ID** and **Secret Key**  trong trang details application và Information tab.


#App-scoped ID

    


##Khởi tạo SDK
Trong Application constructor, gọi hàm App360SDK.Initialize() với các tham số application id và secret key.
```csharp
public App()
{
    // App.xaml initialization
    
	App360SDK.Initialize(<application_id>, <secret>);	
}
```

Khi chạy ứng dụng, SDK sẽ mở lại app-scoped ID session nếu đã được lưu lại từ lần đăng nhập trước.
Sau khi khởi tại, nếu ActiveSession có giá trị null thì bạn cần phải mở một session mới(xem mục Khởi tạo Session)

##Khởi tạo Session
App-scoped ID được đăng nhập thông qua việc tạo một session mới cho ID đấy.Có hai cách để tạo ra một session (sử dụng MOGSessionManager class):

###Anonymously

Khi session được tạo bằng các nặc danh và scopedId đã tồn tại, một session mới sẽ được tạo cho app-scoped ID đó. Nếu scopedId không tồn tại, một app-scoped ID mới trùng với scopedId sẽ được tạo. Nếu scopedId null, nó sẽ được sinh một cách ngẫu nhiên.

```csharp
MOGSession sessionInfo = await MOGSessionManager.OpenActiveSessionWithScopeIdAsync(null, null);
```

>Cần chú ý rằng sẽ không có bất kì cơ thế xác thực nào (password, token,...) cho việc đăng nhập nặc danh. Hình thức đăng nhập này nên được sử dụng sau khi ứng dụng đã có sẵn cơ chế xác thực riêng (trong trường hợp này, scopedId có thể được đặt theo username trong ứng dụng); hoặc khi ứng dụng không cần người dùng đăng nhập ngay. Anonymously logged-in user có thể liên kết với tài khoản Facebook và Google sau đó nhằm mục đích bảo mật và linh động (ví dụ như đăng nhập vào cùng 1 app-scoped ID trên những thiết bị khác nhau).



###Đăng nhập với Facebook/Google access token
```csharp 
// MOGSession class contains informations about Scoped Session such as id, channel, client id, etc.
MOGSession sessionInfo = await 
// Replace your access token with <AccessToken>
MOGSessionManager.OpenActiveSessionWithSocialIDAsync(SocialDServiceName.FACEBOOK,"<AccessToken>");

```
Khi session được tạo qua Facebook/Google access token, SDK sẽ kiểm tra token với server của Facebook/Google sau đó sẽ tìm một app-scoped ID đã tồn tài và đã liên kết với tài khoản mạng xã hội tương ứng. Nếu không có ID nào, thì một ID mới sẽ được sinh ngẫu nhiên và được liên kết với tài khoản mạng xã hội đó, session tương ứng sẽ được trả về. Nếu không, một session mới sẽ được sinh ra cho app-scoped ID tương ứng. Nếu sử dụng cách này thì cơ chế xác thực sẽ sử dụng qua Facebook và Google.


Đăng nhập có thành công?
“sessionInfo.IsSuccess” sẽ trả về true,nếu user đó đăng nhập thành công và ngược lại. Nếu có lỗi xảy ra thì chi tiết lỗi sẽ được trẻ về  “sessionInfo.Error”
```csharp
if (sessionInfo.IsSuccess)
{
	var messageBoxResult = MessageBox.Show("Đăng nhập thành công");	
}
else
{                
	MessageBox.Show("Đăng nhập không thành công", "\n -Lỗi \n" + sessionInfo.Error, MessageBoxButton.OK);
}
```
Sau khi khởi tạo 1 session, một user mới cũng được tạo cho session đó Bạn có thể lấy các thông tin của user đó trong `MOGScopedUser.CurrentUser` . Bạn cũng có thể bổ sung hay sửa các thông tin của user nếu có:

example:
```csharp
{
	MOGScopedUser.CurrentUser["age"] = 22;
    MOGScopedUser.CurrentUser["gender"] = "female";
    // save all by synchronization with server
    await MOGScopedUser.CurrentUser.SaveAsync();
}
```
## Kết nối app-scoped ID với Facebook/Google


Để kết nối current app-scoped ID với Facebook/Google:
```csharp
// replace <facebook_access_token> with Facebook access token of user
MOGScopedUser.CurrentUser.LinkWithFacebookToken("<facebook_access_token>");
```
Khi kết nối này thành công,  giá trị của `MOGScopedUser.CurrentUser.SocialID` sẽ được cập nhật.

#Thanh toán
##Luồng thanh toán
Để đảm bảo độ tin cậy cho luồng thanh toán, ứng dụng của bạn có thể chọn tích hợp với SDK thông qua cả client-side và server-side, luồng thanh toán được mô tả trong sơ đồ dưới đây:

![Payment flow](http://www.websequencediagrams.com/cgi-bin/cdraw?lz=dGl0bGUgUGF5bWVudCBzZXF1ZW5jZQoKR2FtZS0-U0RLOgAUCXJlcXVlc3QgKDEpClNESy0-R2FtZTogVHJhbnNhY3Rpb24gaWQsIHN0YXR1cywgYW1vdW50ICgyKQBGB0dhbWUgc2VydmVyOiBzZW5kIHQAMAtkYXRhIGZvciBpbnNwZQBJBigzAF8LADIJACoMaWQAYQgsIHVzZXJfaWQgKDQAbAYAZQcAgT8HYWNrIChIVFRQAIEaByAyMDApICg1AB0PAIEYDWNvbmZpcm0gKDYAExMAFAs3KQ&s=rose)

1. Ứng dụng (client-side) gọi API thanh toán từ SDK, tùy chọn có thể gửi kèm payload (mô tả bên dưới).
2. SDK trả về transaction id và thông tin bổ sung (nếu có)
3. Ứng dụng client gửi thông tin về transaction lên server của ứng dụng để đợi xác nhận.
4. SDK server gọi đến server của ứng dụng đã được đăng ký để thông báo về trạng thái của giao dịch khi nó đã hoàn thành.
- Lưu ý rằng hai bước (3) và (4), thứ tự có thể không giống như sơ đồ. Bước (4) hoàn toàn có thể xảy ra trước bước (3).
5. Server của ứng dụng thông báo đã nhận được kết quả cho SDK server bằng việc trả về HTTP status code 200.
6. Server của ứng dụng duyệt giao dịch dựa vào các thông tin của nó (transaction ID, payload,....)
7. Server của ứng dụng xác nhận/thông báo cho ứng dụng về trạng thái của giao dịch.

**Note**:
- Để đăng kí địa chỉ server nhận callback, truy cập https://developers.app360.vn/; điền _Payment callback endpoint_ trong trang thông tin của ứng dụng, tab _Information_.
- Trước khi sử dụng bất kì phương thức thanh toán náo, ứng dụng phải khởi tạo SDK và khởi tạo session trước. Xem mục _Khởi tạo SDK_ ở trên.

Có hai cách để tích hợp thanh toán của App360SDK vào ứng dụng của bạn:
- Sử dụng UI mặc định mà SDK cung cấp
- Tự xây dựng UI của bạn và sử dụng các API thanh toán mà SDK cung cấp.

## Using payment form UI
Khởi tạo một object của PaymentForm class
Dưới đây là ví dụ về sử dụng form Thanh toán của sdk:


```csharp
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

paymentForm.Show();            
```
- Nếu bạn không set giá trị các Amounts của một phương thức thanh toán nào đó thì phương thức đó sẽ được ẩn đi trong danh sách.

![enter image description here](https://lh4.googleusercontent.com/--2Fws7ta3u8/VQlTMDgC88I/AAAAAAAAABQ/9AJLR6qRjig/s0/PaymentMenu_Screenshot.png "Payment Menu")


- Bạn cũng có thể gọi các hàm khác như`paymentForm.ShowCardForm()`, `paymentForm.ShowBankingForm()`, `paymentForm.ShowSMSForm()` để hiển thị luôn một phương thức thanh toán.

![enter image description here](https://lh3.googleusercontent.com/-AhUwyJC6abQ/VQlT4n6XtHI/AAAAAAAAABk/WQ52q3InNrs/s0/PaymentCard.png "Payment Card")

![enter image description here](https://lh6.googleusercontent.com/-vsWZrBzEN-Q/VQlUCxGhACI/AAAAAAAAABw/M0iL8TbUO0w/s0/PaymentSms.png "Sms Form")

![enter image description here](https://lh5.googleusercontent.com/-0T53v7rBTLc/VQlUOHwj2QI/AAAAAAAAAB8/lvM6ikxt9qg/s0/PaymentBanking.png "Banking Form")

- Class MyAmountConverter được implement IAmountConverter interface để chuyển lượng tiền được nạp sang dạng tiền hay item trong game.
Tham khảo ví dụ sau:
```csharp
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
```






## Sử dụng payment classes
 Khởi tạo một object MOGPaymentSDK
```csharp
// create a new instance
MOGPaymentSDK paymentSDK = new MOGPaymentSDK();
```
###Tạo một **banking** charging request
```csharp
// amount: amount of money for charge
MOGBankingTransaction transaction = await paymentSDK.MakeBankingTransaction("<amount>","<payload>");
```

###Tạo một **SMS** charging request
```csharp
// SMS_AMOUNT_VALUE_1: defined in MOGPaymentSDK.SMS_AMOUNT
// you can pass more than one SMS_AMOUNT 

MOGSMSTransaction smsTransaction=await paymentSDK.GetSMSSyntax("<payload>", <SMS_AMOUNT_VALUE_1>,<SMS_AMOUNT_VALUE_2>,..);
```
###Tạo một **Card** charging request

```csharp
// vendor: defined in MOGCardVendor,
// you also create a new Vendor by using new Vendor("NewVendorName");
// card_code: the code of the phone card
// serial: The serial of the phone card
MOGPhoneCardTransaction phonecardTransaction = await paymentSDK.MakePhoneCardTransaction( <vendor>, "<card_code>", "<serial>", "<payload>");
```
`// payload: Some custom string you want to send to your server when transaction finished.`

## Kiểm tra trạng thái của transaction

```
// transaction_id: Id of transaction
 MOGStatusTransaction status = await paymentSDK.CheckStatusOfTransaction("<transaction_id>");
```
#Release Notes

##Version 1.0.0
**Release Date:**  09 Mar 2015

 - Support user management via app-scoped ID
 - Support charging via phone card, SMS and e-banking
 - Support checking transaction status

# Known Issues

There's no known issues for now

# FAQ

**Application id và client key là gì?**

Đây là cặp key, dùng để xác thực ứng dụng (game) của bạn với SDK server.

**Application id và client key có thể lấy ở đâu?**

1. Truy cập https://developers.app360.vn
2. Đăng nhập nếu bạn đã có tài khoản, nếu chưa hãy đăng ký tài khoản mới
3. Mở ứng dụng của bạn trong App360 dashboard. Nếu chưa có hãy tạo mới. Sau đó, chọn tab Information
4. Trong tab này, copy application key và secret key

# Hỗ trợ
Vui lòng liên hệ với [chúng tôi](mailto:support@app360.vn) về những vấn đề chung.

## Về những vấn đề kỹ thuật
Trong trường hợp bạn có những vấn đề về kỹ thuật, vui lòng liên hệ với [đội kỹ thuật của chúng tôi](mailto:support@app360.vn).
Vui lòng cung cấp những thông tin sau khi liên hệ, chúng sẽ giúp chúng tôi hỗ trợ bạn nhanh hơn rất nhiều.

- **Phiên bản của SDK** bạn đang sử dụng. Bạn có thể biết được phiên bản của SDK thông qua việc gọi hàm `App360SDK.getVersion()`.
- **Môi trường** sử dụng để có thể tái hiện lại vấn đề (máy ảo hay thiết bị thật, model nào, android version bao nhiêu).
- **Các bước** để tái hiện vấn đề.
- Nếu có thể, bạn hãy cung cấp **một vài đoạn code**, thậm chí cả project nếu có thể.

> Để biết thêm thông tin chi tiết, vui lòng truy cập https://developers.app360.vn.