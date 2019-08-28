using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public interface IGameView {
    IDatabase GameDatabase { get; set; }
}
public class GameView :MonoBehaviour {

    public IDatabase GameDatabase { get; set; }
    
    void Start() {
        var scene = SceneManager.GetSceneByName("GameRoot");
        if (scene.isLoaded) {
            RegisterGameView(scene);
        } else {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene s, LoadSceneMode m) {
        if (s.name == "GameRoot") {
            RegisterGameView(s);
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void RegisterGameView(Scene s) {
        foreach (var go in s.GetRootGameObjects()) {
            if (go.name == "GameRoot") {
                var gameRoot = go.GetComponent<GameRoot>();
                if (gameRoot != null) {
                    gameRoot.RegisterGameViewObject(this);
                    OnRegistered();
                }
            }
        }
    }

    protected virtual void OnRegistered() {

    }

    private void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
public class BaseGameView :GameView, IGameView
{
    public bool ActiveOnStart;
    public void RegisterCallBack(IDatabase db){
	    Debug.LogWarning("DB: CB");
	    GameDatabase = db;
    }

    protected virtual void OpenPopup(Type t){
	    GameDatabase.GameViewController.OpenPopup(t);
    }
	protected virtual void ClosePopup(Type t) {
		GameDatabase.GameViewController.ClosePopup(t);
	}
	protected virtual void OpenView(Type t) {
		GameDatabase.GameViewController.OpenView(t);
	}
	protected virtual void CloseView(Type t) {
		GameDatabase.GameViewController.CloseView(t);
	}
	protected virtual void CloseView(bool DestroyGameObject){
		gameObject.SetActive(false);
	    if (DestroyGameObject){
		    Destroy(this);
	    }
	    
	    
    }
}
