using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User 
{
	public string UserId;
	public List<Player> CharacterList;
	public Player CurrentCharacter;
	
	public User(string id) {
		UserId = id;
	}
}
