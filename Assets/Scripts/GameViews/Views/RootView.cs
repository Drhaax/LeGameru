using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootView : BaseGameView
{

    protected override void OnRegistered() {
        
    }

 
    public void Close() {
        base.CloseView(false);
    }
  
    public void OpenPopup() {
        base.OpenPopup(typeof(TesbPopup));
    }
}
