using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanOpenPopup
{
	void OpenPopup(Type t);
	void ClosePopup(Type t);

	void OpenView(Type t);
	void CloseView(Type t);
}

public class GameViewController :ICanOpenPopup
{
	private Dictionary<Type, GameObject> ViewDict = new Dictionary<Type, GameObject>();
	private Dictionary<Type, GameObject> PopupDict = new Dictionary<Type, GameObject>();

	private ViewContainer Views;
	private ViewContainer Popups;
	private GameObject UIParent;
	public GameViewController(GameObject ui) {
		Views = Resources.Load<ViewContainer>("ScriptableObjects/Views");
		Popups = Resources.Load<ViewContainer>("ScriptableObjects/Popups");
		UIParent = ui;
		ViewDict = Views.ToDict(UIParent.transform);
		PopupDict = Popups.ToDict(UIParent.transform);
		InitController();
	}

	void InitController() {
		foreach (var view in ViewDict) {
			//var go = GameObject.Instantiate(view.Value, UIParent.transform);

			//var viewBase = go.GetComponent<BaseGameView>();
			//go.SetActive(viewBase.ActiveOnStart);
		}
	}

	public void OpenPopup(Type t) {
		var popup = PopupDict[t];
		if (popup != null) {
			if (popup.activeInHierarchy) {
				Debug.Log("popup is already active");
			} else {
				popup.SetActive(true);
			}
		} else {
			Debug.LogError("Didn't find popup registered with type: " + t);
		}
	}
	public void ClosePopup(Type t) {
		var popup = PopupDict[t];
		if (popup != null) {
			if (popup.activeInHierarchy) {
				popup.SetActive(false);
			} else {
				Debug.Log("popup is already closed");
			}
		} else {
			Debug.LogError("Didn't find popup registered with type: " + t);
		}
	}

	public void OpenView(Type t) {
		var view = ViewDict[t];
		if (view != null) {
			if (view.activeInHierarchy) {
				Debug.Log("view is already active");
			} else {
				view.SetActive(true);
			}
		} else {
			Debug.LogError("Didn't find view registered with type: " + t);
		}
	}

	public void CloseView(Type t) {
		var view = ViewDict[t];
		if (view != null) {
			if (view.activeInHierarchy) {
				view.SetActive(false);
			} else {
				Debug.Log("view is already closed");
			}
		} else {
			Debug.LogError("Didn't find view registered with type: " + t);
		}
	}
}
