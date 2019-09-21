using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CharacterSelectionAggregate :AggregateBase
{
	User currentUser;
	public event EventHandler<string> UserPropChanged;
	public CharacterSelectionAggregate(ref Action onUpdate) : base(ref onUpdate) {
	}

	public void SetCurrentUser(User u) {
		currentUser = u;
		u.PropertyChanged -= UserPropertyChanged;
		u.PropertyChanged += UserPropertyChanged;
	}

	private void UserPropertyChanged(object sender, PropertyChangedEventArgs e) {
		if (UserPropChanged != null) {
			UserPropChanged.Invoke(sender, e.PropertyName);
		}
	}

	public List<Character> GetUserCharacters() {
		return currentUser.CharacterList;
	}
	public Character GetSelectedCharacter() {
		if (currentUser.SelectedCharacter == null && currentUser.CharacterList.Count > 0) {
			currentUser.SelectedCharacter = currentUser.CharacterList[0];
		}
		return currentUser.SelectedCharacter;
	}

	public void SelectCharacter(string name) {
		currentUser.SetSelectedCharacter(name);
	}

	public void CreateNewCharacter(Character ch) {
		currentUser.CreateNewCharacter(ch);
	}

	public GameObject GetSelectedCharacterMesh(Character c) {
		GameObject go = null;
		switch (c.CharacterInfo.Race) {
			case Race.Human:
			go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
				break;
			case Race.Orc:
			go = GameObject.CreatePrimitive(PrimitiveType.Cube);
				break;
			case Race.Dwarf:
			go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				break;
		}
		return go;
	}
}
