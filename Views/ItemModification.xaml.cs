using MauiApp5.Models;

namespace MauiApp5.Views;

public partial class ItemModification : ContentPage
{
    private readonly Item _item;
    private readonly Action<Item> _onSave;

    public ItemModification(Item item, Action<Item> onSave)
    {
        InitializeComponent();
        _item = item;
        _onSave = onSave;

        InitializeFields();
    }

    private void InitializeFields()
    {
        NameEntry.Text = _item.Name;
        PriceEntry.Text = _item.Price.ToString();
        RatingEntry.Text = _item.Rating.ToString();
        CommentEntry.Text = _item.Comment;
    }

    private void SaveChanges_Clicked(object sender, EventArgs e)
    {
        UpdateItem();
        SaveItem();
        NavigateBack();
    }

    private void UpdateItem()
    {
        _item.Name = NameEntry.Text;
        _item.Price = float.Parse(PriceEntry.Text);
        _item.Rating = int.Parse(RatingEntry.Text);
        _item.Comment = CommentEntry.Text;
    }

    private void SaveItem()
    {
        _onSave?.Invoke(_item);
    }

    private void NavigateBack()
    {
        Navigation.PopModalAsync();
    }
}