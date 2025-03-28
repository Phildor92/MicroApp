﻿namespace MicroApp.Data.Recipes.Models;

public abstract class Model
{
    public int Id { get; set; }
    public virtual string Key => Id.ToString();
}