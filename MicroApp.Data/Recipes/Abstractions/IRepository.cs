using System.Collections.ObjectModel;
using MicroApp.Data.Recipes.Models;

namespace MicroApp.Data.Recipes.Abstractions;

public interface IRepository<T> where T : Model
{
    public ObservableCollection<Recipe> GetAllModels();
    public T? GetModelById(int id);
    public void AddModel(T model);
    public void UpdateModel(T model);
    public void DeleteModel(T model);
}