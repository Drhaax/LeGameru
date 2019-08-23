﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesbGameView : BaseGameView{
	
	protected override void OnRegistered(){
		base.OnRegistered();
		Debug.LogWarning("Registered½!s");
	}

	public void Tebs(){
		Debug.LogWarning(GameDatabase.Aggregates.tesbAggregate.s);
		GameDatabase.CommandInvoker.Invoke<KaljaCommand>();
	}

	public void Benis(){
		GameDatabase.CommandInvoker.Invoke<IntCommand,int>(5);
	}
	public void Close(){
		base.CloseView(false);
	}
    public  void Login() {
        GameDatabase.Aggregates.tesbAggregate.Login();
        Close();
    }
	public void OpenPopup(){
		base.OpenPopup(typeof(TesbPopup));
	}
}
