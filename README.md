## Build status
![Build status](https://github.com/dhindrik/TinyMvvm/actions/workflows/main.yml/badge.svg)

# TinyMvvm
TinyMvvm is a tiny helper library for using MVVM with .NET MAUI.

This is the repo containging the TinyMvvm for .NET MAUI. If you are looking for the code for TinyMvvm for Xamarin.Forms you find it in the old repo, https://github.com/TinyStuff/TinyMvvm

## Get started

### Initialize
In **MauiProgram.cs** add the following code to initialize TinyMvvm.
```csharp
builder.UseTinyMvvm();
```

To use the **ApplicationSleep** and **ApplicationResume** overrides from **TinyViewModel** you need to add **TinyApplication** as the base class for your **App** class.

```csharp
public partial class App : TinyApplication
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
```

If you want to run code asynchronous when the app starts you can override the **Initialize** method of **TinyApplication**.

```csharp
public App()
{
	InitializeComponent();

	MainPage = new AppShell();
}

protected override async Task Initialize()
{
        
}
```

### TinyView
TinyMvvm has a base view called **TinyView** that you need to use if you want to use the overides from **TinyViewModel**. TinyView has ContentPage as it's base class so you can use it exactly as a normal ContentPage.

```xml
<mvvm:TinyView xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:mvvm="clr-namespace:TinyMvvm;assembly=TinyMvvm.Maui"
             x:Class="TinyMvvm.Sample.Views.MainView"
             Title="Main View">
```

### TinyViewModel
TinyMvvm has a class that you can use as the base class for your viewmodels. It is called **TinyViewModel**.
```csharp
public partial class MainViewModel : TinyViewModel
{
}
```

#### CommunityToolkit.Mvvm
TinyMvvm has CommunityToolkit.Mvvm as a dependency. This means that TinyView has **ObservableObject** as it's base class and implementing the INotifyPropertyChanged interface via that base class. Read morea about CommunityToolkit.Mvvm here, https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/

The Xamarin.Forms version of TinyMvvm had it's own implementation of **ICommand**. For .NET MAUI that implementation is removed, and the recommendation is to use the one from CommunityToolkit instead.

#### Methods to override
**Initialize**
This method runs when the viewmodel is set as **BindingContext** of a view. This method can be used for initial load of data. Note that **NavigationParameter** and **QueryParameters** is not set when this method runs.
```csharp
public override Task Initialize()
{

}
```

**OnParameterSet**
This metod will run after that navigation parameters has been set. Read more about that under **Navigation**.

```csharp
public override async Task OnParameterSet()
{
    IsBusy = true;

    if (NavigationParameter is City city)
    {
        City = city;
    }
    else
    {
        var text = QueryParameters["city"];
        City = await cityService.Get(text.ToString());
    }

    IsBusy = false;
 }
```

**OnAppearing**
OnAppearing will run when the view is a appearing, this method is triggerd from OnAppearing in **TinyView**.
```csharp
public override Task OnAppearing()
{
}
```

**OnDisappearing**
OnDisappearing will run when the view is a appearing, this method is triggerd from OnDisappearing in **TinyView**.
```csharp
public override Task OnDisappearing()
{
}
```

**OnApplicationResume**
OnApplicationResume will run when/after the application returns from the background. It is triggered by OnSleep from the TinyApplication class. Because if that, your App class need to have **TinyApplication** as base class.

```csharp
public override Task OnApplicationResume()
{
}
````

**OnApplicationSleep**
OnApplicationSleep will run when/before the application goes to the background. It is triggered by OnSleep from the TinyApplication class. Because if that, your App class need to have **TinyApplication** as base class.

```csharp
public override Task OnApplicationSleep()
{
}
```

#### Navigation
**TinyViewModel** has a navigation helper that you can use in your viewmodels. It is more a less a wrapper around Shell navigation, but with some helpers for parameter. But you should pass a route to it that you have defined in your shell file or defined with the Routing.RegisterRoute method.

You can either pass a parameter or use a querystring or do both.

```csharp
[RelayCommand]
public async Task NavigateToPage()
{
    var monkey = new Monkey();

    await Navigation.NavigateTo("//DetailsPage?id=1", monkey); 
}
```

**TinyViewModel** has two ways to access navigation parameters, the **QueryParameters** property and the **NavigationParameter** property.
QueryParameters is of type **Dictionary<string, object>** and NavigationParameter is of type **object**. Querystrings will be splitted up and added to the QueryParameters property. Single parameters will be in the NavigationParameters property. After that properties are set the ****OnParameterSet** method is called. To access the properties you should override OnParameterSet method.

```csharp
public override async Task OnParameterSet()
{
    var id = QueryParameters["id"] as string;
    var monkey = NavigationParameter as Monkey;
}
```

#### IsBusy
TinyViewModel has a **IsBusy** and a **IsNotBusy** property that you can use in your viewmodels and views. IsNotBusy is read-only and is updated when you set IsBusy.

### Video
In this video you can watch Daniel Hindrikes talk about TinyMvvm, https://youtu.be/kqsoG2Ii4w4.
[![TinyMvvm on YouTube](https://user-images.githubusercontent.com/6691971/226563106-4bba3a4e-c64e-4bac-846f-c6f1c744a74a.png)](https://youtu.be/kqsoG2Ii4w4")


## Contribute
You are very welcome to contribute to TinyMvvm. If you want to add a new feature we would like if you create an issue first so we can discuss the the feature before you spend time to implement it.
