using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using MicroApp.Data.Recipes.Models;
using MicroApp.Services;
using MicroApp.ViewModels;

namespace MicroApp.Views;

public partial class RecipeEditView : UserControl
{
    private Step _draggedStep;
    
    public RecipeEditView()
    {
        InitializeComponent();
        
        DataContext = this.CreateInstance<RecipeEditViewModel>();
    }

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if(sender is not Border border)
            return;

        if(border.DataContext is not Step step)
            return;

        //TODO Use dragdrop?
        
        _draggedStep = step;
    }

    private void InputElement_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if(sender is not Border border)
            return;
        
        if(border.DataContext is not Step)
            return;

        var root =  border.GetVisualRoot();
        if(root is not Window window)
            return;

        var visuals = window.GetVisualsAt(e.GetPosition(window), _ => true);

        if(visuals.FirstOrDefault(v => v is Border) is not Border targetBorder)
            return;
        
        if(targetBorder.DataContext is not Step targetStep)
            return;
        
        if(_draggedStep == targetStep)
            return;

        if(DataContext is not RecipeEditViewModel viewModel)
            return;
        
        viewModel.Steps.Remove(_draggedStep);
        viewModel.Steps.Insert(targetStep.Order, _draggedStep);
        var oldSteps = viewModel.Steps.ToList();
        viewModel.Steps.Clear();
        
        var count = 0;
        foreach (var step in oldSteps)
        {
            step.Order = count++;
            viewModel.Steps.Add(step);
        }
    }       
}