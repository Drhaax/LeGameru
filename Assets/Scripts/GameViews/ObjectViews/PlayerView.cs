using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : GameView
{   [SerializeField]
    GroundRaycaster GroundRaycaster;
    [SerializeField]
    float groundOffset;
    public Character p;
    Camera cam;
    protected override void OnRegistered() {
        base.OnRegistered();
    }

    internal void HandleModelChanged(object sender, string arg2) {
        var character = sender as Character;
        if (arg2 == "POS") {
            gameObject.transform.localPosition = new Vector3((float)character.CurrentPosition.x, GroundRaycaster.GetGroundLevel()+groundOffset, (float)character.CurrentPosition.z);
        }
        
    }

    public void ActivateView() {
        cam = GetComponentInChildren<Camera>();
        cam.gameObject.SetActive(p.isLocal);
        //gameObject.transform.position = p.CurrentPosition.ToVector3();
    }
}
