
using System;
using System.Collections.Generic;
using UnityEngine;
using ByteSerializer;
using FixMath.NET;

public class Players
{
    List<Player> players;

    public Players() {
        players = new List<Player>();
    }
    public Player FindPlayer(string id) {
        foreach (var p in players) {
            if (p.Id == id) {
                return p;
            }
        }
        return null;
    }

    public void AddPlayer(Player p) {
        players.Add(p);
    }
}

public class Player
{
    public string Id;
    public CustomVector CurrentPosition;
    public CustomVector TargetPosition;
    public bool isLocal;
    public Action<object, string> PlayerModelChanged = (o, s) => { };
    public Player(string Id) {
        this.Id = Id;
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
        PlayerModelChanged.Invoke(this, "POS");
    }
}
