using MauiApp5.Models;
using System.Collections;
using System.Diagnostics;

namespace MauiApp5.Views;
    public partial class ItemPage : ContentPage
    {

        public string collectionName;

        public string CollectionName
        {
            get => collectionName;
            set
            {
                if (value != null)
                {
                    collectionName = value;
                    LoadCollection(value);
                }
            }
        }

        public ItemPage()
        {
            InitializeComponent();
            BindingContext = new ListItems();
        }

        private void LoadCollection(string collectionName)
        {
            LogFilePath(collectionName);
            var context = (ListItems)BindingContext;
            string filePath = GetCollectionFilePath(collectionName);

            if (!File.Exists(filePath))
                return;

            string[] collectablesPerLine = File.ReadAllLines(filePath).Skip(1).ToArray();

            foreach (string line in collectablesPerLine)
            {
                string[] data = line.Split(';');

                if (data.Length != 5)
                    continue;

            Item collectable = new Item()
                {
                    Name = data[0],
                    Price = float.Parse(data[1]),
                    Rating = int.Parse(data[2]),
                    Comment = data[3]
                };

                context.Collectables.Add(collectable);
            }
        }

        private void AddCollectable_Clicked(object sender, EventArgs e)
        {
            var context = (ListItems)BindingContext;

            if (string.IsNullOrEmpty(context.NewCollectableName))
                return;

            if (context.NewCollectableRating < 1 || context.NewCollectableRating > 10)
            {
                DisplayAlert("Error", "Wrong data", "OK");
                return;
            }

            string collectableLine = $"{Environment.NewLine}{context.NewCollectableName};{context.NewCollectablePrice};{context.NewCollectableRating};{context.NewCollectableStatus};{context.NewCollectableComment}";
            string filePath = GetCollectionFilePath(CollectionName);

            File.AppendAllText(filePath, collectableLine);

        Item collectable = new Item()
            {
                Name = context.NewCollectableName,
                Price = context.NewCollectablePrice,
                Rating = context.NewCollectableRating,
                Comment = context.NewCollectableComment
            };

            context.Collectables.Add(collectable);
        }

        private async void DeleteCollectable_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var collectable = (Item)button.BindingContext;
            var context = (ListItems)BindingContext;

            DeleteCollectableFromFile(collectable);
            context.Collectables.Remove(collectable);
        }

        private async void EditCollectable_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var collectableToEdit = (Item)button.BindingContext;

            var editPage = new ItemModification(collectableToEdit, updatedCollectable =>
            {
                var context = (ListItems)BindingContext;
                context.Collectables.Remove(collectableToEdit);
                DeleteCollectableFromFile(collectableToEdit);
                AppendUpdatedCollectableToFile(updatedCollectable);
                context.Collectables.Add(updatedCollectable);
            });

            await Navigation.PushModalAsync(editPage);
        }

        private void DeleteCollectableFromFile(Item collectable)
        {
            string filePath = GetCollectionFilePath(CollectionName);
            string[] lines = File.ReadAllLines(filePath);
            var remainingLines = lines.Where(line => !line.StartsWith(collectable.Name + ";"));
            File.WriteAllLines(filePath, remainingLines);
        }

        private void AppendUpdatedCollectableToFile(Item updated)
        {
            string filePath = GetCollectionFilePath(CollectionName);
            string newLine = $"{Environment.NewLine}{updated.Name};{updated.Price};{updated.Rating};{updated.Comment}";
            File.AppendAllText(filePath, newLine);
        }

        private void LogFilePath(string collectionName)
        {
            string filePath = GetCollectionFilePath(collectionName);
            Debug.WriteLine($"Collection file path for {collectionName}: {filePath}");
        }

        private string GetCollectionFilePath(string collectionName)
        {
            return Path.Combine(FileSystem.AppDataDirectory, $"{collectionName}_collection.txt");
        }
    }
