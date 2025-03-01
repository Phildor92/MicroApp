using Avalonia.Controls;
using MicroApp.Services;
using MicroApp.ViewModels;

namespace MicroApp.Views;

public partial class RecipeDisplayView : UserControl
{
    public RecipeDisplayView()
    {
        InitializeComponent();
        
        DataContext = this.CreateInstance<RecipeDisplayViewModel>();
    }
}