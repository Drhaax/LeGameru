using ServerConsoleApp;
using System;
using System.Collections.Generic;
using UnityEngine;
using ByteSerializer;
using FixMath.NET;

public class GameLogic
{

    CoreMessager coreMessager;
    IEventProcessor eventProsessor;
  //  Players Players;
    Player localPlayer;
    Aggregates aggregates;
    public GameLogic(IEventProcessor eventProsessor, ref Action OnTick, CoreMessager coreMessager,Aggregates aggregates) {
        this.eventProsessor = eventProsessor;
        this.coreMessager = coreMessager;
        this.aggregates = aggregates;
        coreMessager.AddMessageCallback(HandleMessageReceived);
        OnTick += Tick;
    
    }

    private void HandleMessageReceived(Message msg, UdpState state) {
        switch (msg.type) {
            case MessageType.LogIn:
                Login(msg,state);
                break;
            
            case MessageType.LogOut:
                Logout(msg,state);
                break;

            case MessageType.Tick:
                ServerTick(msg,state);
                break;
            case MessageType.Movement:
                Movement(msg,state);
                break;
        }
    }

    public void StartLogic() {
        coreMessager.Start();
    }
    public void ServerTick(Message msg, UdpState s) {
        var players = msg.players;
        aggregates.PlayerAggregate.HandlePlayerMovement(players);
    }
    public void Tick() {
        if (aggregates.PlayerAggregate.LocalPlayer != null) {
            coreMessager.SendPlayerState(aggregates.PlayerAggregate.LocalPlayer.GetPlayerState());
        }
        
    }
    public void Login(Message msg, UdpState s) {
        var player = new Player(msg.name);
        eventProsessor.AddEventToQueue(() => {
            aggregates.PlayerAggregate.PlayerLogin(player);
        });
        
    }

    public void Logout(Message msg, UdpState s) { 
    }

    public void Movement(Message msg, UdpState s) {
        //eventProsessor.AddEventToQueue(() => {
        //    var pos = msg.playerState.Position.ToVector3();
        //    aggregates.PlayerAggregate.LocalPlayer.SetPosition(pos);
        //});
    }

}
