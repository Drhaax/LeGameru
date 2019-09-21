using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesbGameView : BaseGameView{
	
	protected override void OnRegistered(){
		base.OnRegistered();
	//	ActiveOnStart = GameDatabase.Aggregates.CharacterAggregate
	}

	
	public void Close(){
		base.CloseView(false);
	}
    public  void Login() {
        GameDatabase.Aggregates.DebugAggregate.Login();
        Close();
        
    }
	public void OpenPopup(){
		base.OpenPopup(typeof(TesbPopup));
	}

	public void OpenView() {
		base.OpenView(typeof(RootView));
	}
}
