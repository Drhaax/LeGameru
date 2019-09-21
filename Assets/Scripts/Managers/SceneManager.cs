using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface ISceneManager { 

}
public class GameSceneManager : ISceneManager 
{
	static GameSceneManager instance;
	public static GameSceneManager Instance { 
		get {
			if (instance == null) {
				instance = new GameSceneManager();
			}
			return instance;
		} 
	}	

	public const string CharacterCreation = "CharacterCreation";
	List<Action<Scene>> SceneLoadedActions;
	ICircleHardPromise<Scene> loadScenePromise;
	private GameSceneManager() {
		SceneLoadedActions = new List<Action<Scene>>();
		SceneManager.sceneLoaded += SceneLoaded;
		SceneManager.sceneUnloaded += SceneUnloaded;
	}

	private void SceneUnloaded(Scene s) {
		switch (s.name) {
			case CharacterCreation:
			break;
		}
	}

	private void SceneLoaded(Scene s, LoadSceneMode m) {
		foreach (var action in SceneLoadedActions) {
			action(s);
		}
		switch (s.name) {
			case CharacterCreation:
			if (loadScenePromise != null) {
				loadScenePromise.Resolve(s);
			}
			GameRoot.CameraManager.SetCameraStatus(CameraType.CharacterCreation, true);
			break;
		}
	}

	public void AddSceneLoadedAction(Action<Scene> action) {
		SceneLoadedActions.Add(action);
	}
	public void RemoveSceneLoadedAction(Action<Scene> action) {
		List<Action<Scene>> newList = new List<Action<Scene>>();
		for (int i = 0; i < SceneLoadedActions.Count; i++) {
			var a = SceneLoadedActions[i];
			if (a != action) {
				newList.Add(a);
			}
		}
		SceneLoadedActions = newList;
	}
	public Scene GetSceneByName(string s) {
		return SceneManager.GetSceneByName(s);
	}

	public ICircleHardPromise<Scene> LoadSceneAsync(string s, LoadSceneMode m) {
		loadScenePromise = new CircleHardPromise<Scene>();
		var scene = GetSceneByName(s);
		if (scene.isLoaded) {
			loadScenePromise.Resolve(scene);
			foreach (var action in SceneLoadedActions) {
				action(scene);
			}
		} else {
			SceneManager.LoadSceneAsync(s, m);
		}
		return loadScenePromise;
	}
	public void LoadScene(string s, LoadSceneMode m) {
		SceneManager.LoadScene(s,m);
	}

	public void UnLoadScene(string s) {
		SceneManager.UnloadScene(s);
	}
	public void UnLoadSceneAsync(string s) {
		SceneManager.UnloadSceneAsync(s);
	}
	public ICircleHardPromise<Scene> LoadCharacterCreation() {
		return LoadSceneAsync(CharacterCreation,LoadSceneMode.Additive);
	}
}
