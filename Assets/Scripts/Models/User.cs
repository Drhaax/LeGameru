using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User 
{
	public string UserId;
	public List<Character> CharacterList;
	public Character CurrentCharacter;
	
	public User(string id) {
		UserId = id;
		CharacterList = new List<Character>();
	}
}
