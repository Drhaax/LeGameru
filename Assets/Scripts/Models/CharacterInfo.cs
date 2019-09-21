using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Race { 
	Undefined = 0,
	Human,
	Orc,
	Dwarf
}

public class CharacterInfo
{
	public Race Race = Race.Undefined;
}
