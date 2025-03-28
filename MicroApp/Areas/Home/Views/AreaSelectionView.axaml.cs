using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MicroApp.Areas.Home.ViewModels;
using MicroApp.Services;

namespace MicroApp.Areas.Home.Views;

public partial class AreaSelectionView : UserControl
{
    public AreaSelectionView()
    {
        InitializeComponent();
        
        DataContext = this.CreateInstance<AreaSelectionViewModel>();
    }
}