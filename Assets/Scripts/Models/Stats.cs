using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Stats 
{
   public Dictionary<StatType, Stat> StatCollection { get; }
    public Stats() {
        StatCollection = new Dictionary<StatType, Stat>();
    }

    public void AddStat(Stat stat) {
        StatCollection[stat.Type] = stat;
    }
    public Stat GetStat(StatType type) {
        return StatCollection[type];
    }
    
}
