﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesbPopup : BaseGameView{
		
	protected override void OnRegistered(){
		base.OnRegistered();
		Debug.LogWarning("Registered½!s");
	}

	public void Close(){
		base.CloseView(false);
	}

	public void OpenPopup(){
		base.OpenPopup(typeof(TesbPopup));
	}
}
