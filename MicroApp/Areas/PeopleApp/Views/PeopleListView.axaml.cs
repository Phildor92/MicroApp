using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MicroApp.Areas.PeopleApp.ViewModels;
using MicroApp.Services;

namespace MicroApp.Areas.PeopleApp.Views;

public partial class PeopleListView : UserControl
{
    public PeopleListView()
    {
        InitializeComponent();
        
        DataContext = this.CreateInstance<PeopleListViewModel>();
    }
}