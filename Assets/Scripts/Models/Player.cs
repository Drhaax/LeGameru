
using System;
using System.Collections.Generic;
using UnityEngine;
using ByteSerializer;
using FixMath.NET;

public class Characters
{
    List<Character> characters;

    public Characters() {
        characters = new List<Character>();
    }
    public Character FindCharacter(string id) {
        foreach (var u in characters) {
            if (u.Id == id) {
                return u;
            }
        }
        return null;
    }

    public void AddPlayer(Character u) {
        characters.Add(u);
    }
}

public class Character
{
    public string Id;
    public Stats CharacterStats;
    public Inventory Inventory;
    public Equipments Equipments;
	public CharacterInfo CharacterInfo;
    public CustomVector CurrentPosition;
    public CustomVector TargetPosition;
    public bool isLocal;
	[field: NonSerialized]
	public Action<object, string> CharacterModelChanged = (o, s) => { };
    public Character(string Id) {
        this.Id = Id;
		CharacterInfo = new CharacterInfo();
        CharacterStats = new Stats();
        Inventory = new Inventory();
        Equipments = new Equipments();
        CurrentPosition = new CustomVector(Fix64.Zero, (Fix64)3, Fix64.Zero);
        TargetPosition = new CustomVector(Fix64.Zero, Fix64.Zero, Fix64.Zero);
    }
    public PlayerState GetPlayerState() {

        if (Input.GetMouseButton(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                TargetPosition = hit.point.ToCustomVector();
                return new PlayerState() {
                    Name = Id,
                    Position = CurrentPosition,
                    TargetPosition = TargetPosition,
                };
            }
        }

        return null;
    }

    public void SetPosition(Vector3 pos) {
        CurrentPosition = pos.ToCustomVector();
        CharacterModelChanged.Invoke(this, "POS");
    }
}
