using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITesb{
    void OispaBish();
    void prkl(int i);
}
public class DebugAggregate : AggregateBase, ITesb{
    public string s = "prjkl";

    ICoreMessager coreMessager;
    public DebugAggregate(ref Action onUpdate, ICoreMessager coreMessager) : base(ref onUpdate){
        this.coreMessager = coreMessager;
    }


    public void OispaBish(){
        Debug.LogWarning("oispaBish");
    }

    public void prkl(int i){
        Debug.LogWarning("prklrklrklrlkr + " +i);
     }

    internal void Login() {
        var maybePlayer = PlayerPrefs.GetString("Player");
        if (maybePlayer == null) {
           var playerGuid = Guid.NewGuid().ToString();
            PlayerPrefs.SetString("Player",playerGuid);
            maybePlayer = playerGuid;
        }

        coreMessager.Login(maybePlayer);
    }
}

public class Aggregates{
    public DebugAggregate DebugAggregate;
    public CharacterAggregate CharacterAggregate;
    public Aggregates(ref Action OnUpdate, ICoreMessager coreMessager){
        DebugAggregate = new DebugAggregate(ref OnUpdate, coreMessager);
		CharacterAggregate = new CharacterAggregate(ref OnUpdate);
    }
}
