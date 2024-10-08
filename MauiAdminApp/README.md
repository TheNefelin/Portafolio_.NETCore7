# Maui Admin App

## Edit SVG Icons
* [photopea](https://www.photopea.com/)

## Dependencies
```
Newtonsoft.Json
CommunityToolkit.Maui.MediaElement
ClassLibraryDTOs
```

* MauiProgram.cs
```
using CommunityToolkit.Maui;
...
var builder = MauiApp.CreateBuilder();
builder
    .UseMauiApp<App>()
    .UseMauiCommunityToolkitMediaElement()
    ...
```
* Namespace on Page xaml and Code
```
xmlns:toolkit="http://schemas.microsoft.com/dotnet/2022/maui/toolkit"

<toolkit:MediaElement
    x:Name="MediaElement"
    Aspect="AspectFill"
    ShouldAutoPlay="True"
    ShouldLoopPlayback="True"
    ShouldShowPlaybackControls="False"
    VerticalOptions="FillAndExpand"
    HorizontalOptions="FillAndExpand"
    BackgroundColor="Transparent"
    Source="embed://video01.mp4">
</toolkit:MediaElement>
```

## Structure
```
MauiAdminApp/
│
├── Pages/
│   ├── Home/
│   │   ├── HomePage.xaml
│   │   └── HomePage.xaml.cs
│   ├── Login/
│   │   ├── LoginPage.xaml
│   │   └── LoginPage.xaml.cs
│   ├── Messages/
│   │   ├── MessagesPage.xaml
│   │   └── MessagesPage.xaml.cs
│   ├── Profile/
│   │   ├── ProfilePage.xaml
│   │   └── ProfilePage.xaml.cs
│   └── Settings/
│       ├── SettingsPage.xaml
│       └── SettingsPage.xaml.cs
├── Resources/
│   ├── AppIcon/
│   ├── Fonts/
│   ├── Images/
│   ├── raw/
│   ├── Splash/
│   └── Styles/
├── Services/
│   └── AuthenticationService.cs
├── App.xaml
├── App.xaml.cs
├── AppShell.xaml
├── AppShell.xaml.cs
├── MauiProgram.cs
└── README.md
```

## App
* Create Services folder
```
```
* Create Pages folder
```
```

## ContentPage
* Solo un hijo raiz
* StackLayout: contenerdor de elementos

## Navegation
### Simple navigation
```
Application.Current.MainPage = new NewPage();
```

### Navigation by NavigationPage
```
MainPage = new NavigationPage(new HomePage());
await Navigation.PushAsync(new DetailPage());
await Navigation.PopAsync();
```

### Navigation by NavigationPage
```
await Shell.Current.GoToAsync("//LoginPage");

<ShellContent
    Title="Login"
    ContentTemplate="{DataTemplate local:LoginPage}"
    Route="LoginPage" />
```

### Navigation by Modal
```
await Navigation.PushModalAsync(new ModalPage());
await Navigation.PopModalAsync();
```
*  Example
```
// Antes de comenzar la tarea asíncrona
await Navigation.PushModalAsync(new LoadingPage());

// Realiza la tarea asíncrona (ejemplo: cargar datos, autenticación, etc.)
await Task.Delay(3000); // Simulando una tarea larga

// Cerrar la página de Loading después de completar la tarea
await Navigation.PopModalAsync();
```

<hr>

## CSS
```
<Color x:Key="PrimaryColor">#003087</Color>
<Color x:Key="BackgroundColor">#1A1A1A</Color>
<Color x:Key="TextColor">#FFFFFF</Color>
```
```
<ContentPage BackgroundColor="{StaticResource BackgroundColor}">
    <Label Text="Bienvenido a PS App" TextColor="{StaticResource TextColor}" />
</ContentPage>
```
