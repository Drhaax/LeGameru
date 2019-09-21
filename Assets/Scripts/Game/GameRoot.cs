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
	public static CameraManager CameraManager;
	private Action Tick;
    private Action OnUpdate;
    private IDatabase gameDatabase;
    float time = 0;
	Dictionary<KeyPositionType, KeyPosition> keyPositions;

	public Queue<Action> EventQueue { get; private set; }
	GameLogic gameLogic;
    public void InitGameRoot(GameObject ui, out Action initDone, Dictionary<KeyPositionType, KeyPosition> keyPositions) {
		this.keyPositions = keyPositions;
		CameraManager = new CameraManager();
		EventQueue = new Queue<Action>();
        CoreMessager coreMessager = new CoreMessager();
		gameDatabase = new GameDatabase(ref OnUpdate, ui, coreMessager,keyPositions);
		initDone = InitializationDone;

		gameLogic = new GameLogic(this,ref Tick, coreMessager,gameDatabase.Aggregates);
        
       
    }

	public void InitializationDone() {
		gameLogic.StartLogic();

	}

    void Update(){
        time += Time.deltaTime;
        if (time >= (1f / CoreMessager.TickDuration)) {
            time = 0;
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

	internal void KeyPositionsChanged(object sender, KeyPosition e) {
		
		Debug.LogWarning(e.KeyPositionType + "   " + keyPositions.Count);
		//keyPositions.Add(e.KeyPositionType,e);
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
