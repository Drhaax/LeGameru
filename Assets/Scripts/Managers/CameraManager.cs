using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager 
{
	public Dictionary<CameraType, CameraHandle> CameraHandles;
	public CameraManager() {
		CameraHandles = new Dictionary<CameraType, CameraHandle>();
	}

	public void RegisterCamera(CameraHandle camera) {
		CameraHandles[camera.CameraType] = camera;
		Debug.LogWarning("registered camera: " + camera.CameraType);
	}
}
