using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipments : MonoBehaviour
{
    public Equipment Weapon;
    public Equipment Head;
    public Equipment Body;
    public Equipment Legs;
    public Equipment Feet;

    public void EquipEquipment(Equipment e) {
        switch (e.type) {
            case EquipmentType.Weapon:
            break;
            case EquipmentType.Head:
            break;
            case EquipmentType.Body:
            break;
            case EquipmentType.Legs:
            break;
            case EquipmentType.Feet:
            break;
        }
    }
}
