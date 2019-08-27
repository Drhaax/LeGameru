using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryItem {
    public string ItemId;
    public string InventoryAction { 
        get {
            var type = GetType();
            if (type == typeof(Item)) {
                return "Use";
            } else if (type == typeof(Equipment)) {
                return "Equip";
            } else {
                return "NOT DEFINED";
            }

        } 
    }
    public InventoryItem(string id) {
        ItemId = id;
    }
    public virtual void Use() { 
    }
    public virtual void Drop() {
    }
}
public class Inventory 
{
    public List<InventoryItem> Items { get; }

    int ItemCap = 40;

    public Inventory() { 
        
    }

    public void AddItemToInventory(InventoryItem i) {
        if (Items.Contains(i)) {
            Debug.LogWarning("Already in inventory");
            return;
        }
        Items.Add(i);
    }

}
