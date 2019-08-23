using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITesb{
    void OispaBish();
    void prkl(int i);
}
public class TesbAggregate : AggregateBase, ITesb{
    public string s = "prjkl";

    ICoreMessager coreMessager;
    public TesbAggregate(ref Action onUpdate, ICoreMessager coreMessager) : base(ref onUpdate){
        this.coreMessager = coreMessager;
    }


    public void OispaBish(){
        Debug.LogWarning("oispaBish");
    }

    public void prkl(int i){
        Debug.LogWarning("prklrklrklrlkr + " +i);
     }

    internal void Login() {
        coreMessager.Login();
    }
}

public class Aggregates{
    public TesbAggregate tesbAggregate;
    public PlayerAggregate PlayerAggregate;
    public Aggregates(ref Action OnUpdate, ICoreMessager coreMessager){
        tesbAggregate = new TesbAggregate(ref OnUpdate, coreMessager);
        PlayerAggregate = new PlayerAggregate(ref OnUpdate);
    }
}
