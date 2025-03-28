using Avalonia.Controls;
using MicroApp.Services;
using RecipeDisplayViewModel = MicroApp.Areas.RecipeApp.ViewModels.RecipeDisplayViewModel;

namespace MicroApp.Areas.RecipeApp.Views;

public partial class RecipeDisplayView : UserControl
{
    public RecipeDisplayView()
    {
        InitializeComponent();
        
        DataContext = this.CreateInstance<RecipeDisplayViewModel>();
    }
}