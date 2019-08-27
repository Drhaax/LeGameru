using FixMath.NET;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StatType { 
    Strength,
    Dexterity,
    Vitality,
    Intelligence
}

public class Stat
{
    public StatType Type;
    public int BaseValue { get; private set; }
    public Dictionary<string, int> Modifiers;

    public Stat(StatType type, int baseStat) {
        this.Type = type;
        this.BaseValue = baseStat;
    }

    public void AddModifier(string modifierName, int value) {
        Modifiers[modifierName] = value;
    }

    public void RemoveModifier(string modifierName) {
        Modifiers.Remove(modifierName);
    }

    public int GetTotalValue() {
        var total = BaseValue;
        foreach (var value in Modifiers.Values) {
            total += value;
        }
        return total;
    }
}
