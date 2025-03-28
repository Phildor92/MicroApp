using Avalonia.Controls;
using MicroApp.Areas.RecipeApp.ViewModels;
using MicroApp.Areas.RecipeApp.ViewModels;
using MicroApp.Services;

namespace MicroApp.Areas.RecipeApp.Views;

public partial class RecipeListView : UserControl
{
    public RecipeListView()
    {
        InitializeComponent();
        
        DataContext = this.CreateInstance<RecipeListViewModel>();
    }
}