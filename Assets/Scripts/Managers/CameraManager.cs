using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager 
{
	public Dictionary<CameraType, CameraHandle> CameraHandles;
	List<CameraType> PendingActivations = new List<CameraType>();
	public CameraManager() {
		CameraHandles = new Dictionary<CameraType, CameraHandle>();
	}

	public void RegisterCamera(CameraHandle camera) {
		CameraHandles[camera.CameraType] = camera;
		if (PendingActivations.Contains(camera.CameraType)) {
			SetCameraStatus(camera.CameraType, true);
			PendingActivations.Remove(camera.CameraType);
		}
	}

	public void SetCameraStatus(CameraType c,bool active) {
		if (CameraHandles.ContainsKey(c)) {
			CameraHandles[c].SetCameraActive(active);
		}
		else {
			PendingActivations.Add(c);

		}
	}
}
