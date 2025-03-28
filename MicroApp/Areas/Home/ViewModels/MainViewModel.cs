using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MicroApp.Areas.Home.ViewModels;

public partial class MainViewModel : ViewModel
{
    [ObservableProperty] private string _greeting = "Welcome to Avalonia!";
    private ViewModel _currentViewModel;
    private int _currentRecipeId;
    private static bool _isRegistered;
    
    public ICommand BackCommand { get; set; }

    public ViewModel CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }

    public MainViewModel()
    {
        BackCommand = new RelayCommand(( )=> CurrentViewModel = new AreaSelectionViewModel());
        CurrentViewModel = new AreaSelectionViewModel();
        
        WeakReferenceMessenger.Default.Register<ViewModelChangedMessage>(this, (r,m) =>
        {
            _currentRecipeId = m.Value.id;
            CurrentViewModel = (ViewModel)Activator.CreateInstance(m.Value.viewModel);
        });
        
        if(!_isRegistered)
        {
            WeakReferenceMessenger.Default.Register<MainViewModel, ViewModelRequestIdMessage>(this, (r,m) =>
            m.Reply(r._currentRecipeId));
            _isRegistered = true;
        }
    }
}

public class ViewModelChangedMessage(Type value, int id) : ValueChangedMessage<(Type viewModel, int id)>((value,id));
public class ViewModelRequestIdMessage : RequestMessage<int>;