using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "View", menuName = "ViewContainer", order = 1)]
public class ViewContainer : ScriptableObject{
	public List<GameObject> GameViews;

	public Dictionary<Type,GameObject> ToDict(){
		var Dict = new Dictionary<Type, GameObject>();
		foreach (var gw in GameViews){
			var baseView = gw.GetComponent<BaseGameView>();
			if (baseView != null){
				Dict.Add(baseView.GetType(),gw);
			}
			else{
				Debug.LogError(gw.name + " Did not contain BaseGameView component");
			}
		}

		return Dict;
	}
}
