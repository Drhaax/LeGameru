using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeGame : MonoBehaviour{
    private string[] Scenes = {"UI","GameRoot","LeGame","StartArea"};
    private GameObject UIParent;
    void Start(){
        SceneManager.sceneLoaded += SceneLoaded;
        Init();
    }

    void Init(){
        LoadScenes();
    }

    void LoadScenes(){
        foreach (var s in Scenes){
            var maybeScene = SceneManager.GetSceneByName(s);
            if (!maybeScene.isLoaded){
                SceneManager.LoadSceneAsync(s, LoadSceneMode.Additive);
            }
            else{
                if (maybeScene.name == "GameRoot"){
                    foreach (var obj in maybeScene.GetRootGameObjects()){
                        if (obj.name == "GameRoot"){
                            var gameRoot = obj.GetComponent<GameRoot>();
                            gameRoot.InitGameRoot(UIParent);
                        }
                    }
                }

                if (maybeScene.name == "UI"){
                    foreach (GameObject go in maybeScene.GetRootGameObjects()){
                        if (go.name == "ui"){
                            UIParent = go;
                        }
                    }
                }
            }
        } 
    }
    
    
    private void SceneLoaded(Scene s, LoadSceneMode type){
        if (s.name == "GameRoot"){
            foreach (var obj in s.GetRootGameObjects()){
                if (obj.name == "GameRoot"){
                    var gameRoot = obj.GetComponent<GameRoot>();
                    gameRoot.InitGameRoot(UIParent);
                }
            }
        }

        if (s.name == "UI"){
            foreach (GameObject go in s.GetRootGameObjects()){
                if (go.name == "ui"){
                    UIParent = go;
                }
            }
        }
    }
}
