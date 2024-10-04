# Maui Admin App

## Dependecies
```
Newtonsoft.Json

ClassLibraryApplication
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

## Injeccion de Dependencias
*  Si es un servicio, se debe agregar en MauiProgram.cs
* Lo mismo aplica para HttpClient
```
// Configurar HttpClient para inyectarlo en los servicios
builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://artema.bsite.net/") });
builder.Services.AddSingleton<AuthService>();
```
