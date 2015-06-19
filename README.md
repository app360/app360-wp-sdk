Other language: [Tiếng việt](https://github.com/app360/app360-wp-sdk/blob/master/README-VI.md)
#Introduction
App360SDK provides easiest way to integrate user management and payment methods (including sms, phone card and e-banking) into your application.

#Requirements

## Development Environment

| App360SDK Version | Minimum Windows Phone Version Target | 				Notes 			|
|:-----------------:|:------------------:|:----------------------------:|
|1.4.0|8.0|Support for windows phone 8.0 and 8.1 silverlight projects|
# Getting started with Demo project

Firstly, clone or download this repository to your machine.

 - `git clone https://github.com/app360/app360-wp-sdk.git`
 - or, download from https://github.com/app360/app360-wp-sdk

# 5 Step to intergrate with App360SDK
## 1. Create Account
The first thing you need to do to get started with App360 is sign up for a free account. After create account, you can access App360 dashboard to create or manage your apps.
## 2. Create your app

To integrate with App360SDK you need to create new application. Each application has a pair of key (application id and application secret key) that will be used to authorize with SDK’s server.
## 3. Intergration

I recommend you that you should use Nuget to install App360SDK.
- Go to Tools -> Nuget Package Manager -> Package Manager Console
- Paste and run the following command: `Install-Package App360SDK`

## 4. Set Capabilities

Open WMAppManifest.xml and set capabilities below:
- ID_CAP_NETWORKING
- ID_CAP_MEDIALIB_AUDIO
- ID_CAP_MEDIALIB_PLAYBACK
- ID_CAP_SENSORS
- ID_CAP_WEBBROWSERCOMPONENT
- ID_CAP_IDENTITY_DEVICE

## 5. Initialize SDK

Open your `App.xaml.cs` in project and paste the following code into the last of method `App()`
```
public App()
{
    // App.xaml initialization
       App360SDK.Initialize("application_id", "secret");    
}
```
To create new session, replace the following code into your Mainpage.xaml.cs
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

>Note:You can get appID and appSecret in the code example above from App360 dashboard. Login your account, choose the app you are working on and you will see the keys you need in Information tab App credential
>
>![image description](http://i.imgur.com/Bp1ymT0.jpg)

#  What’s next?

- Checkout [our document](http://docs.app360.vn/) for more infomation of App360SDK
- Integrate with [Payment API](http://docs.app360.vn/?page_id=271)
- If you got any trouble, checkout the [FAG page](http://docs.app360.vn/?page_id=228) or send a support request


#Release Notes
## Version 1.4.0
**Release Date:** 19 June 2015
  - Fix bugs.
  - Change parameters of GetSMSSyntax method.

# Support
Please contact [us](mailto:support@app360.vn) for general inquiries.
## For a technical issue
In case you have a technical issue, you can reach our technical support team. Please provide the following information when you reach out, it'll allow us to help you much more quickly.

 - The steps to reproduce the problem. 
 - If possible, some pieces of code, or even a project.

>For more information, please go to http://docs.app360.vn.