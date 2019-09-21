using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreationView :BaseGameView
{
	public TMPro.TMP_InputField nameInput;
	GameObject playerCharacter;

	protected override void OnRegistered() {
		
	}

	public void InstantiatePlayerCharacter() {
		if (playerCharacter != null) {
			return;
		}
		var pos = GameDatabase.KeyPositions[KeyPositionType.CharacterCreationModeldPlace];
		var go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		go.transform.position = pos.transform.position;
		playerCharacter = go;
	}

	public void CreateNewCharacter() {
		if (nameInput.text == "") {
			Debug.LogWarning("Invalid name");
			return;
		}
		var character = new Character(nameInput.text);
		character.CharacterInfo.Race = Race.Human;
		GameDatabase.Aggregates.CharacterSelectionAggregate.CreateNewCharacter(character);
		OpenView(typeof(CharacterSelectionView));
		CloseView(this);
	}

}
