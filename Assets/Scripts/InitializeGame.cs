using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeGame : MonoBehaviour{
    private GameObject UIParent;
	private int LoadedSceneCount;
	Action InitDoneAction;
	event EventHandler<KeyPosition> KeyPositionsChanged;
	Dictionary<KeyPositionType, KeyPosition> KeyPositions = new Dictionary<KeyPositionType, KeyPosition>();
	private string[] StartUpScenes = { "UI", "GameRoot", "LeGame", "StartArea" };
	bool InitializationDone;
	void Start(){
		Init();
    }

    void Init(){
		GameSceneManager.Instance.AddSceneLoadedAction(HandleGameRootLoaded);
		GameSceneManager.Instance.AddSceneLoadedAction(HandleUILoaded);
		GameSceneManager.Instance.AddSceneLoadedAction(GetKeyPositions);
		GameSceneManager.Instance.AddSceneLoadedAction(CountScenes);
        LoadScenes();
    }

	void HandleGameRootLoaded(Scene s) {
		if (s.name == "GameRoot") {
			foreach (var obj in s.GetRootGameObjects()) {
				if (obj.name == "GameRoot") {
					var gameRoot = obj.GetComponent<GameRoot>();
					gameRoot.InitGameRoot(UIParent, out InitDoneAction, KeyPositions);
					KeyPositionsChanged -= gameRoot.KeyPositionsChanged;
					KeyPositionsChanged += gameRoot.KeyPositionsChanged;

				}
			}
		}
	}

	void HandleUILoaded(Scene s) {
		if (s.name == "UI") {
			foreach (GameObject go in s.GetRootGameObjects()) {
				if (go.name == "ui") {
					UIParent = go;
				}
			}
		}
	}
    void LoadScenes(){
		
        foreach (var s in StartUpScenes) {
			GameSceneManager.Instance.LoadSceneAsync(s,LoadSceneMode.Additive);
            
        } 
    }

	void CountScenes(Scene s) {
		foreach (var scene in StartUpScenes) {
			if (scene == s.name) {
				LoadedSceneCount++;
			}
			if (LoadedSceneCount == StartUpScenes.Length) {
				if (InitDoneAction != null) {
					InitDoneAction.Invoke();
					GameSceneManager.Instance.RemoveSceneLoadedAction(CountScenes);
				} else {
					Debug.LogError("Init action was null");
				}
			}
		}
	}
    
	private void GetKeyPositions(Scene s) {
		
		foreach (var go in s.GetRootGameObjects()) {
			var keyPos = go.GetComponent<KeyPosition>();
			if (keyPos != null) {
				AddKeyPosition(keyPos);
				return;
			}
			keyPos = go.GetComponentInChildren<KeyPosition>();
			if (keyPos != null) {
				AddKeyPosition(keyPos);
				return;
			}
		}
	}

	void AddKeyPosition(KeyPosition kp) {
		KeyPositions.Add(kp.KeyPositionType,kp);
		KeyPositionsChanged.Invoke(this,kp);
	}
}
