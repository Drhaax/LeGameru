using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType { 
    Weapon,
    Head,
    Body,
    Legs,
    Feet
}

public class GameData
{
    #region Equipment
    public static Dictionary<string, Equipment> Equipments = new Dictionary<string, Equipment>() { 
        {"LeEquip",new Equipment("E")}
    };
    #endregion

    #region Items
    #endregion
}
