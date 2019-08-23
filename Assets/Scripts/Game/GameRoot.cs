using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;


public interface IEventProcessor {
     void AddEventToQueue(Action a);
}

public class GameRoot : MonoBehaviour, IEventProcessor
{
    private Action Tick;
    private Action OnUpdate;
    private IDatabase gameDatabase;
    float time = 0;
    public Queue<Action> EventQueue { get; private set; }
    
    public void InitGameRoot(GameObject ui){
        EventQueue = new Queue<Action>();
        CoreMessager coreMessager = new CoreMessager();
        gameDatabase = new GameDatabase(ref OnUpdate, ui, coreMessager);
        var gameLogic = new GameLogic(this,ref Tick, coreMessager,gameDatabase.Aggregates);
        
        gameLogic.StartLogic();
    }

    // Update is called once per frame
    void Update(){
        time += Time.deltaTime; 
        if (time >= 1 / CoreMessager.TickDuration) {
            if (Tick != null) {
                Tick.Invoke();
            }
        }
        if (EventQueue.Count > 0) {
            while (EventQueue.Count > 0) {
                EventQueue.Dequeue().Invoke();
            }
            
        }
        if (OnUpdate != null){
            OnUpdate.Invoke();
        }
    }

    public void RegisterGameViewObject(GameView bgv){
        bgv.GameDatabase = gameDatabase;

    }
    
    IEnumerator Register(BaseGameView bgv) {
        Debug.LogWarning("started corotuinre");
        while (true){
            if (gameDatabase == null){
                yield return new WaitForSeconds(.1f);
            }
            else{
                bgv.GameDatabase = gameDatabase;
                yield break;
            }
        }
       
    }

    public void AddEventToQueue(Action a) {
        EventQueue.Enqueue(a);
    }
}
