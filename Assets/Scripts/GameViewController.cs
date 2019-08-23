using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanOpenPopup{
   void OpenPopup(Type t);
}

public class GameViewController : ICanOpenPopup{
   private Dictionary<Type, GameObject> ViewDict = new Dictionary<Type, GameObject>();
   private Dictionary<Type, GameObject> PopupDict = new Dictionary<Type, GameObject>();
   
   private ViewContainer Views;
   private ViewContainer Popups;
   private GameObject UIParent;
   public GameViewController(GameObject ui){
      Views = Resources.Load<ViewContainer>("ScriptableObjects/Views");
      Popups = Resources.Load<ViewContainer>("ScriptableObjects/Popups");
      ViewDict = Views.ToDict();
      PopupDict = Popups.ToDict();
      UIParent = ui;
      InitController();
   }

   void InitController(){
      foreach (var view in ViewDict){
         var go = GameObject.Instantiate(view.Value,UIParent.transform);
         
         var viewBase = go.GetComponent<BaseGameView>();
         go.SetActive(viewBase.ActiveOnStart);
      }
   }

   public void OpenPopup(Type t){
      var popup = PopupDict[t];
      if (popup != null){
         GameObject.Instantiate(popup,UIParent.transform);
      }
      else{
         Debug.LogError("Didn't find popup registered with type: " + t);
      }
   }
}
