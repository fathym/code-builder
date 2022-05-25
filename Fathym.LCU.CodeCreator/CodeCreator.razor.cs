using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Fathym.LCU.CodeCreator
{
    public class CodeCreatorBase : ComponentBase
    {
        protected List<DropItem> items = new()
        {
            new DropItem() { Name = "Apple", Group = "Basket", Image = "img/fruit/apple.png", Fruit = true },
            new DropItem() { Name = "Bananas", Group = "Basket", Image = "img/fruit/bananas.png", Fruit = true },
            new DropItem() { Name = "Lemon", Group = "Fruit", Image = "img/fruit/lemon.png", Fruit = true },
            new DropItem() { Name = "Broccoli", Group = "Basket", Image = "img/fruit/broccoli.png" },
            new DropItem() { Name = "Strawberry", Group = "Fruit", Image = "img/fruit/strawberry.png", Fruit = true },
            new DropItem() { Name = "Cherry", Group = "Basket", Image = "img/fruit/cherry.png", Fruit = true },
            new DropItem() { Name = "Cabbage", Group = "Vegetable", Image = "img/fruit/cabbage.png" },
        };

        protected void ItemDropped(DraggableDroppedEventArgs<DropItem> dropItem)
        {
            dropItem.Item.Group = dropItem.DropZoneName;
        }
    }
    public class DropItem
    {
        public string Name { get; init; }

        public string Group { get; set; }

        public string Image { get; set; }

        public bool Fruit { get; set; }
    }
}