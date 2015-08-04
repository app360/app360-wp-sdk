Other language: [English](https://github.com/app360/app360-wp-sdk/blob/master/README.md)
# Giới thiệu
App360SDK cung cấp cách thức đơn giản nhất để quản lý user và tích hợp thanh toán (bao gồm sms, thẻ điện thoại và e-banking) vào ứng dụng của bạn.

# Yêu cầu

## Môi trường phát triển

| App360SDK Version | Minimum Windows Phone Version Target | 				Notes 			|
|:-----------------:|:------------------:|:----------------------------:|
|1.4.0|8.0|Support for windows phone 8.0 and 8.1 silverlight projects|
# Bắt đầu với Demo project

Đầu tiên,hãy clone hoặc download ví dụ về máy.

 - `git clone https://github.com/app360/app360-wp-sdk.git`
 - hoặc tải về từ https://github.com/app360/app360-wp-sdk

# 5 bước tích hợp App360SDK
## 1. Tạo tài khoản
Việc đầu tiên để bắt đầu là đăng ký một tài khoản miễn phí App360 . Sau khi tạo tài khoản bạn có thể vào dashboard của App360 để tạo mới và quản lý ứng dụng của bạn.

## 2. Tạo một App

Để tích hợp App360SDK, bạn cần tạo một ứng dụng mới. Mỗi ứng dụng có một cặp Application Id và Secret Key dùng để xác thực với server của SDK


## 3. Tích hợp

Mình khuyến cáo sử dụng Nuget để tích hợp App360SDK một cách chính xác và nhanh nhất.

- Tools -> Nuget Package Manager -> Package Manager Console
- Paste and chạy dòng lệnh sau: `Install-Package App360SDK`

## 4. Khai báo các Capabilities

Mở WMAppManifest.xml and chọn capabilities sau:
- ID_CAP_NETWORKING
- ID_CAP_MEDIALIB_AUDIO
- ID_CAP_MEDIALIB_PLAYBACK
- ID_CAP_SENSORS
- ID_CAP_WEBBROWSERCOMPONENT
- ID_CAP_IDENTITY_DEVICE

## 5. Khởi tạo SDK

Mở file `App.xaml.cs` trong project của bạn và chèn thêm code vào cuối của constructor `App()`
```
public App()
{
    	// App.xaml initialization
 	this.Startup += async (sender, args) =>
	{
        	// Initilize App360SDK
        	await App360SDK.App360SDK.Initialize("application_id", "secret");
	};
}
```
Để tạo một session, thay thế những dòng code tương ứng trong Mainpage.xaml.cs của bạn như sau: 
```
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
			// Open session successfully
			Debug.WriteLine("\n -Đăng nhập thành công \n SessionInfo: \n" + session.ToString());
			
			// Get current user info
			MOGScopedUser currentUser = MOGScopedUser.CurrentUser;
			Debug.WriteLine("\n -Current user id: \n" + currentUser.ScopeId);
		} 
		else
		{
			// Open session failed
			Debug.WriteLine("\n -Lỗi \n" + session.Error + "Đăng nhập không thành công");
		}
    }
}
```

>Chú ý: Bạn có thể lấy AppID và App Secret trong code ví dụ trên từ App360 dashboard. Đăng nhập vào tài khoản của bạn và chọn app đang làm việc, và mở tab App credential để lấy các keys như hình dưới.
>
>![image description](http://i.imgur.com/Bp1ymT0.jpg)

#  What’s next?

- Vào [trang tài liệu](http://docs.app360.vn/) để biết thêm chi tiết.
- tích hợp với [Payment API](http://docs.app360.vn/?page_id=271)
- Nếu có vấn đề gì, liên hệ với [FAG page](http://docs.app360.vn/?page_id=228) hoặc gửi yêu cầu hỗ trợ cho chúng tôi.


#Release Notes
## Version 1.4.0
**Release Date:** 19 June 2015
  - Sửa lỗi.
  - Thay đổi parameters trong hàm GetSMSSyntax.
  
## Version 1.5.0
**Release Date:** 4 Aug 2015
  - Update initialize method.
  
# Hỗ trợ
Vui lòng liên hệ với [chúng tôi](mailto:support@app360.vn) về những vấn đề chung.

## Về những vấn đề kỹ thuật
Trong trường hợp bạn có những vấn đề về kỹ thuật, vui lòng liên hệ với [đội kỹ thuật của chúng tôi](mailto:support@app360.vn).
Vui lòng cung cấp những thông tin sau khi liên hệ, chúng sẽ giúp chúng tôi hỗ trợ bạn nhanh hơn rất nhiều.

- **Phiên bản của SDK** bạn đang sử dụng. Bạn có thể biết được phiên bản của SDK thông qua việc gọi hàm `App360SDK.getVersion()`.
- **Môi trường** sử dụng để có thể tái hiện lại vấn đề (máy ảo hay thiết bị thật, model nào, android version bao nhiêu).
- **Các bước** để tái hiện vấn đề.
- Nếu có thể, bạn hãy cung cấp **một vài đoạn code**, thậm chí cả project nếu có thể.

> Để biết thêm thông tin chi tiết, vui lòng truy cập https://docs.app360.vn.
