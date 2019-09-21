using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDatabase{
	ICommands Commands{ get; }
	Aggregates Aggregates{ get; }
	ICanOpenPopup GameViewController{ get; }
	Dictionary<KeyPositionType, KeyPosition> KeyPositions { get; }
	UIFlowAction StartGame();
}

public class GameDatabase : IDatabase {
	public ICommands Commands{ get; }
	public Aggregates Aggregates{ get;  }
	public ICanOpenPopup GameViewController{ get; }

	public UserManager UserManager { get; }
	public Dictionary<KeyPositionType, KeyPosition> KeyPositions { get; }
	public GameDatabase(ref Action onUpdate, GameObject ui, ICoreMessager coreMessager, Dictionary<KeyPositionType, KeyPosition> keyPositions){
		this.KeyPositions = keyPositions;
		UserManager = new UserManager();
		Aggregates = new Aggregates(ref onUpdate, coreMessager);
		Commands = new Commands(Aggregates,UserManager);
		GameViewController = new GameViewController(ui);
	}

	public UIFlowAction StartGame() {
		var UIAction = UIFlowAction.Undefined;
		var u = UserManager.ReadUser();
		if (u != null) {
			if (u.CharacterList != null && u.CharacterList.Count > 0) {
				UIAction = UIFlowAction.CharacterSelection;
			} else {
				UIAction = UIFlowAction.CharacterCreation;
			}
		} else {
			u = UserManager.CreateNewUser();
			UIAction = UIFlowAction.CharacterCreation;
		}
		Aggregates.CharacterSelectionAggregate.SetCurrentUser(u);
		return UIAction;
	}
	

}
public enum UIFlowAction{
	Undefined,
	CharacterSelection,
	CharacterCreation

}