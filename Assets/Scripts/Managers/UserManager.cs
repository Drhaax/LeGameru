using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserManager
{
	public User user;
	public UserManager() {
		CheckForExistingUser();
		if (user != null) {
		} else {
			CreateNewUser(); 
			
		}
	}

	private void CheckForExistingUser() {
		var maybePlayer = PlayerPrefs.GetString("User");
		TextAsset file = Resources.Load<TextAsset>("LocalDB/Player");
		user = JsonConvert.DeserializeObject<User>(file.text);
	}

	private void CreateNewUser() {
		var userGuid = Guid.NewGuid().ToString();
		PlayerPrefs.SetString("User", userGuid);
		user = new User(userGuid);
		StreamWriter writer = new StreamWriter("Assets/Resources/LocalDB/Player.json", true);
		writer.Write(JsonConvert.SerializeObject(user));
	}

	public void GoToCharacterCreation() { 
	}
}
