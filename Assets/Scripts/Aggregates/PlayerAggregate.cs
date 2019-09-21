using ByteSerializer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAggregate : AggregateBase
{
    public Characters Characters;
    public Queue<Action> MovementQueue { get; private set; }
    public Character LocalCharacter;
    PlayerService playerService;
	public CharacterAggregate(ref Action onUpdate) : base(ref onUpdate) {
		MovementQueue = new Queue<Action>();
		Characters = new Characters();
		playerService = new PlayerService();
	}

    internal void PlayerLogin(Character character) {
        var p = Resources.Load("Prefabs/Player") as GameObject;
        var instance = GameObject.Instantiate(p);
        LocalCharacter = character;
		character.isLocal = true;
        var pView = instance.GetComponent<PlayerView>();
		character.CharacterModelChanged -= pView.HandleModelChanged;
		character.CharacterModelChanged += pView.HandleModelChanged;
        pView.p = character;
        Characters.AddPlayer(character);
        pView.ActivateView();
       
    }

    public void HandlePlayerMovement(List<PlayerState> players) {
        foreach (var p in players) {
			Character character = Characters.FindCharacter(p.Name);
             if (character != null) {
                MovementQueue.Enqueue(() => character.SetPosition(p.Position.ToVector3()));
             }
        }
    }

    protected override void Update() {
        if (MovementQueue.Count > 0) {
            while (MovementQueue.Count > 0) {
                MovementQueue.Dequeue().Invoke();
            }

        }
    }
}
