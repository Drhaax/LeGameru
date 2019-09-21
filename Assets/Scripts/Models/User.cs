using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class User : INotifyPropertyChanged
{
	
	public string UserId;
	[field: NonSerialized]
	public event PropertyChangedEventHandler PropertyChanged;

	public List<Character> CharacterList;
	
	Character selectedChar;
	public Character SelectedCharacter { 
		get { return selectedChar; }
		set { 
			if (selectedChar != value) {
				selectedChar = value;
				if (PropertyChanged != null) {
					OnPropertyChanged("CurrentCharacter");
				}
			} 
		}
	}
	
	public User(string id) {
		UserId = id;
		if (CharacterList != null) {
			Debug.LogWarning(CharacterList.Count + "douasndas d-------------------");
		} else {
			CharacterList = new List<Character>();
		}

	}

	public bool SetSelectedCharacter(string name) {
		foreach (var c in CharacterList) {
			if (c.Id == name) {
				SelectedCharacter = c;
				return true;
			}
		}
		return false;
	}

	public void CreateNewCharacter(Character c) {
		CharacterList.Add(c);
	}
	  protected void OnPropertyChanged(string name) {
		PropertyChangedEventHandler handler = PropertyChanged;
		if (handler != null) {
			handler(this, new PropertyChangedEventArgs(name));
		}
	}
}
