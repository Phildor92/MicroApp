using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MicroApp.Areas.PeopleApp.ViewModels;
using MicroApp.Areas.RecipeApp.ViewModels;

namespace MicroApp.Areas.Home.ViewModels;

public class AreaSelectionViewModel : ViewModel
{
    public ICommand RecipesCommand { get; set; }
    public ICommand PeopleCommand { get; set; }

    public AreaSelectionViewModel()
    {
        RecipesCommand = new RelayCommand(() =>
        {
            WeakReferenceMessenger.Default.Send(new ViewModelChangedMessage(typeof(RecipeListViewModel), 0));
        });
        PeopleCommand = new RelayCommand(() =>
        {
            WeakReferenceMessenger.Default.Send(new ViewModelChangedMessage(typeof(PeopleListViewModel), 0));
        });
    }
}