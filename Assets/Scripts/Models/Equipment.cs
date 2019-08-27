using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : InventoryItem
{
    public string Name;
    public Stats EquipmentStats;
    public int ReuiredLevel;
    public EquipmentType type;
    public Equipment(string itemId):base(itemId) { 

    }
}
