using ServerConsoleApp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using ByteSerializer;
using FixMath.NET;

public interface ICoreMessager{
    void Login();
}

public class CoreMessager : ICoreMessager
{
    public const int TickDuration = 10;
    UdpService UdpService;
    public CoreMessager() {
        UdpService = new UdpService(8000,"127.0.0.1");
      
    }

    public void Start() {
        try {
            UdpService.StartService();
        } catch (Exception e) {
            Debug.LogError(e.ToString());
        }
        
    }
    public void AddMessageCallback(Action<Message, UdpState> HandleMessageReceived ) {
        UdpService.AddMessageCallback(HandleMessageReceived);
    }

    public void Login() {

        var msg = new Message() {
            type = MessageType.LogIn,
            name = Guid.NewGuid().ToString(),
            message = "leTeesb",
            playerState = new PlayerState() {
                Position = new CustomVector(Fix64.One,Fix64.One,Fix64.One)
            }
        };
        var bytes = Serializer.Serialize<Message>(msg);
        UdpService.SendMessage(msg);

    }

    public void SendPlayerState(PlayerState s) {
       if (s == null) {
            UdpService.SendMessage(new Message() {
                type = MessageType.None
            });
        } else {
            UdpService.SendMessage(new Message() {
            type = MessageType.Movement,
                name = s.Name,
                playerState = s
            });
        }
    }

}
public static class Print
{
    public static void Line(string msg) {
        Debug.Log(msg);
    }
    public static void ErrorLine(string msg) {
            Debug.LogError("Error: " + msg);
    }
}