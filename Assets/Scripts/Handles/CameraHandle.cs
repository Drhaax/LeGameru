using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType { 
	UI,
	Player,
	CharacterCreation
}

public class CameraHandle : GameView
{
	Camera camera;
	public bool ActivateOnStartUp = false;
	public CameraType CameraType;
	protected override void OnRegistered() {
		camera = GetComponent<Camera>();
		if (camera == null) {
			Debug.LogError("CameraComponent Missing in: " + gameObject.name);
		}
		SetCameraActive(ActivateOnStartUp);
		GameRoot.CameraManager.RegisterCamera(this);
	}

	public void SetCameraActive(bool active) {
		camera.gameObject.SetActive(active);
	}

}
