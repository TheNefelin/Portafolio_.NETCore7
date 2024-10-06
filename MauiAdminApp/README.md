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
* Create Pages folder
* Create Services folder

### LoginPage
* Login.xaml
```
<StackLayout Padding="30" VerticalOptions="Center" Background="{StaticResource BackgroundColor}">
    <Label Text="Iniciar Sesión" FontSize="24" HorizontalOptions="Center" TextColor="{StaticResource TextColor}"/>
    <Entry Placeholder="Usuario" x:Name="UsernameEntry"/>
    <Entry Placeholder="Contraseña" IsPassword="True" x:Name="PasswordEntry"/>
    <Button Text="Iniciar Sesión" Clicked="OnLoginButtonClicked"/>
    <Button Text="¿Olvidaste tu contraseña?" HorizontalOptions="Center" />
</StackLayout>
```
* Login.xaml.cs
```
private async void OnLoginButtonClicked(object sender, EventArgs e)
{
    string email = UsernameEntry.Text;
    string password = PasswordEntry.Text;

    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
    {
        await DisplayAlert("Error", "Por favor ingresa un correo electrónico y contraseña", "OK");
        return;
    }

    // Lógica para autenticación aquí (ejemplo: llamada a un servicio de API)
    //bool isAuthenticated = await Authenticate(UsernameEntry.Text, PasswordEntry.Text);
    bool isAuthenticated = UsernameEntry.Text.Equals("") || PasswordEntry.Text.Equals("") ? false : true;

    if (isAuthenticated)
    {
        // Redirigir a la pantalla principal de la aplicación
        await Navigation.PushAsync(new MainPage());
    }
    else
    {
        await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
    }
}
```

### App.xaml.cs
```
```

### AppShell.xaml
```
<!-- Menú inferior tipo tab -->
<TabBar>
    <Tab Title="Inicio" Icon="config.png">
        <ShellContent ContentTemplate="{DataTemplate local:HomePage}" />
    </Tab>
    <Tab Title="Mensajes" Icon="config.png">
        <ShellContent ContentTemplate="{DataTemplate local:MessagesPage}" />
    </Tab>
    <Tab Title="Perfil" Icon="config.png">
        <ShellContent ContentTemplate="{DataTemplate local:ProfilePage}" />
    </Tab>
</TabBar>

<!-- Menú lateral (hamburger menu) -->
<FlyoutItem Title="Configuraciones" Icon="config.png">
    <ShellContent ContentTemplate="{DataTemplate local:SettingsPage}" />
</FlyoutItem>
```

### AuthenticationService.cs
```
public class AuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ResponseApiDTO<LoggedinDTO>> Login(string email, string password)
    {
        string apiUrl = "https://artema.bsite.net/api/auth/login";  // URL de la API

        LoginDTO loginDTO = new()
        {
            Email = email,
            Password = password
        };

        var json = JsonConvert.SerializeObject(loginDTO);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResponseApiDTO<LoggedinDTO>>(jsonResponse);

                // Almacenar los tokens de manera segura
                await SecureStorage.SetAsync("jwt_token", result.Data.ApiToken);
                await SecureStorage.SetAsync("sql_token", result.Data.SqlToken);

                return result;
            }
            else
            {
                // Manejo de respuesta cuando no es éxito
                var errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode} - {errorResponse}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error durante el inicio de sesión: {ex.Message}");
        }

        return null;
    }
}
```

### CSS
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
