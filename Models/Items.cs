using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp5.Models
{
    internal class Items
    {
        public ObservableCollection<ListItems> AllCollectionz { get; set; } = new ObservableCollection<ListItems>();

        public string NewCollectionName { get; set; }

        public Items() => LoadCollections();

        public void LoadCollections()
        {
            AllCollectionz.Clear();
            IEnumerable<ListItems> collection = Directory.EnumerateFiles(FileSystem.AppDataDirectory, $"*_collection.txt").Select(collName => new ListItems() { CollectionName = File.ReadAllLines(collName).First(), });
            foreach (ListItems singleColl in collection)
            {
                AllCollectionz.Add(singleColl);
            }
        }
    }
}
