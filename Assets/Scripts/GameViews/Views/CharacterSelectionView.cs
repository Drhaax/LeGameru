using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionView : BaseGameView
{
	[UsedImplicitly]
	public TMPro.TMP_InputField nameInput;
	[UsedImplicitly]
	public Transform charactersParent;

	public GameObject characterSelectionPrefab;
	GameObject selectedCharacterPrefab;
	Transform characterModelPosition;

	protected override void OnRegistered() {
		characterModelPosition =  GameDatabase.KeyPositions[KeyPositionType.CharacterCreationModeldPlace].transform;
		GameDatabase.Aggregates.CharacterSelectionAggregate.UserPropChanged -= UserPropChanged;
		GameDatabase.Aggregates.CharacterSelectionAggregate.UserPropChanged += UserPropChanged;
		foreach (var c in GameDatabase.Aggregates.CharacterSelectionAggregate.GetUserCharacters()) {
			var prefab = Instantiate(characterSelectionPrefab,charactersParent);
			var SelectCharacter = prefab.GetComponent<SelectCharacter>();
			SelectCharacter.SetContext(GameDatabase.Commands.SelectCharacterCommand,c.Id);
		}

		SetSelectedCharacter(GameDatabase.Aggregates.CharacterSelectionAggregate.GetSelectedCharacter());
	}

	private void UserPropChanged(object sender, string e) {
		var user = (User)sender;
		if (e == "CurrentCharacter") {
			Debug.LogWarning("Selected char changed");
			SetSelectedCharacter(user.SelectedCharacter);
		}
	}

	void SetSelectedCharacter(Character c) {
		var go = GameDatabase.Aggregates.CharacterSelectionAggregate.GetSelectedCharacterMesh(c);
		if (selectedCharacterPrefab == null) {
			selectedCharacterPrefab = Instantiate(go,characterModelPosition);
		} else {
			selectedCharacterPrefab = go;
		}
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
