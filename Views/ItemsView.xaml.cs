using MauiApp5.Models;
using System.Collections;

namespace MauiApp5.Views;

public partial class ItemsView : ContentPage
{
    public ItemsView()
    {
        InitializeComponent();
        SetupBinding();
    }

    private void SetupBinding()
    {
        BindingContext = new Items();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadCollections();
    }

    private void LoadCollections()
    {
        if (BindingContext is Items context)
        {
            context.LoadCollections();
        }
    }

    private async void AddCollectionClicked(object sender, EventArgs e)
    {
        if (BindingContext is not Items context || string.IsNullOrEmpty(context.NewCollectionName))
            return;

        string fileName = $"{context.NewCollectionName}_collection.txt";
        string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
        File.WriteAllText(filePath, context.NewCollectionName);

        await GoToItemPage(context.NewCollectionName);
    }

    private async Task GoToItemPage(string collectionName)
    {
        await Shell.Current.GoToAsync($"{nameof(ItemPage)}?{nameof(ItemPage.CollectionName)}={collectionName}");
    }

    private async void ClassButtonClicked(object sender, EventArgs e)
    {
        if (!(sender is Button button) || !(button.BindingContext is ListItems context))
            return;

        await GoToItemPage(context.CollectionName);
    }
}