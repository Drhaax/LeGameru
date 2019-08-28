using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface ISceneManager { 

}
public class GameSceneManager : ISceneManager 
{
	public const string CharacterCreation = "CharacterCreation";
	public GameSceneManager() {
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
		switch (s.name) {
			case CharacterCreation:
			break;
		}
	}

	public void GetSceneByName(string s) {
		SceneManager.GetSceneByName(s);
	}

	public void LoadSceneAsync(string s, LoadSceneMode m) {
		SceneManager.LoadSceneAsync(s,m);
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
	public void LoadCharacterCreation() {
		LoadSceneAsync(CharacterCreation,LoadSceneMode.Additive);
	}
}
