using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerService
{
    public Player SendLoginRequest(string id, Action OnSuccess) {
        TextAsset file = Resources.Load<TextAsset>("LocalDB/Player");
        Debug.LogWarning("File:  " + file.name);
        Debug.LogWarning("Text: " + file.text);
        var p = JsonConvert.DeserializeObject<Player>(file.text);
        if (p == null) {
            Debug.LogWarning("null player -------------");
        } else { 
        
        }
        return p;
    }
}
