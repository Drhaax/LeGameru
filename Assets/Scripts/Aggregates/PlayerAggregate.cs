using ByteSerializer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAggregate : AggregateBase
{
    public Players Players;
    public Queue<Action> MovementQueue { get; private set; }
    public PlayerAggregate(ref Action onUpdate) : base(ref onUpdate) {
        MovementQueue = new Queue<Action>();
        Players = new Players();
    }
    public Player LocalPlayer;
    internal void PlayerLogin(Player player) {
        Debug.LogWarning("LOGIN");
        var p = Resources.Load("Prefabs/Player") as GameObject;
        var instance = GameObject.Instantiate(p);
        LocalPlayer = player;
        player.isLocal = true;
        var pView = instance.GetComponent<PlayerView>();
        player.PlayerModelChanged -= pView.HandleModelChanged;
        player.PlayerModelChanged += pView.HandleModelChanged;
        pView.p = player;
        Players.AddPlayer(player);
        pView.ActivateView();
       
    }

    public void HandlePlayerMovement(List<PlayerState> players) {
        foreach (var p in players) {
            Player player = Players.FindPlayer(p.Name);
             if (player != null) {
                MovementQueue.Enqueue(() => player.SetPosition(p.Position.ToVector3()));
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
