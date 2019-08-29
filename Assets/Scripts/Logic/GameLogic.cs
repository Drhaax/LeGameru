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
        aggregates.CharacterAggregate.HandlePlayerMovement(players);
    }
    public void Tick() {
        if (aggregates.CharacterAggregate.LocalCharacter != null) {
            coreMessager.SendPlayerState(aggregates.CharacterAggregate.LocalCharacter.GetPlayerState());
        }
        
    }
    public void Login(Message msg, UdpState s) {
        var character = new Character(msg.name);
        eventProsessor.AddEventToQueue(() => {
            aggregates.CharacterAggregate.PlayerLogin(character);
        });
        
    }

    public void Logout(Message msg, UdpState s) { 
    }

    public void Movement(Message msg, UdpState s) {
     
    }

}
