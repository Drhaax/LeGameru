using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StartGameView : BaseGameView
{
	protected override void OnRegistered() {
		base.OnRegistered();
	}


	public void StartGame() {
		var promise = GameSceneManager.Instance.LoadCharacterCreation();
		promise.OnSuccess(s => {
			Debug.LogWarning(s);
			var action = GameDatabase.StartGame();
			if (action == UIFlowAction.CharacterCreation) {
				OpenView(typeof(CharacterCreationView));
			} else if (action == UIFlowAction.CharacterSelection) {
				OpenView(typeof(CharacterSelectionView));
			} else {
				Debug.LogError("Undefined action!");
				return;
			}
			CloseView(this);
		});

		
	}
}
