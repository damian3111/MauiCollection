﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp5.Models
{
    internal class ListItems
    {
        public string CollectionName { get; set; }
        public string NewCollectableName { get; set; }
        public float NewCollectablePrice { get; set; }
        public int NewCollectableRating { get; set; }
        public string NewCollectableStatus { get; set; }
        public string NewCollectableComment { get; set; }

        public ObservableCollection<Item> Collectables { get; set; } = new ObservableCollection<Item>();
    }
}
