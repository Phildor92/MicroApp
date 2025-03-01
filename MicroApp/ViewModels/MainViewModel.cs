using CommunityToolkit.Mvvm.ComponentModel;

namespace MicroApp.ViewModels;

public partial class MainViewModel : ViewModel
{
    [ObservableProperty] private string _greeting = "Welcome to Avalonia!";
}