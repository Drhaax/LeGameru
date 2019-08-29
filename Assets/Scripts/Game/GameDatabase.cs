﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDatabase{
	ICommands Commands{ get; }
	Aggregates Aggregates{ get; }
	ICanOpenPopup GameViewController{ get; }
	UserManager UserManager { get; }
	void StartGame(out Action a);
}

public class GameDatabase : IDatabase {
	public ICommands Commands{ get; }
	public Aggregates Aggregates{ get;  }
	public ICanOpenPopup GameViewController{ get; }

	public UserManager UserManager { get; }

	public GameDatabase(ref Action onUpdate, GameObject ui, ICoreMessager coreMessager){
		UserManager = new UserManager();
		Aggregates = new Aggregates(ref onUpdate, coreMessager);
		Commands = new Commands(Aggregates,UserManager);
		GameViewController = new GameViewController(ui);
	}

	public void StartGame(out Action a) {
		var u = UserManager.CheckForExistingUser();
		if (u != null) {
			if (u.CharacterList != null && u.CharacterList.Count > 0) {
				a = () => { };
				//GoToCharacterSelection
			} else {
				a = () => UserManager.GoToCharacterCreation();
			}
		} else {
			UserManager.CreateNewUser();
			a = () => UserManager.GoToCharacterCreation();
		}
	}
	

}
