using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;

public class UserManager
{
	
	public User ReadUser() {
		TextAsset file = Resources.Load<TextAsset>("LocalDB/Player");
		var u = JsonConvert.DeserializeObject<User>(file.text);
		return u;
	}
	public void WriteUser(User u) {
		using (StreamWriter sw = new StreamWriter("Assets/Resources/LocalDB/Player.json")) {
			sw.Write(JsonConvert.SerializeObject(u));
		}
	}

	public User CreateNewUser() {
		var userGuid = Guid.NewGuid().ToString();
		var u = new User(userGuid);
		WriteUser(u);
		return u;
	}

}

