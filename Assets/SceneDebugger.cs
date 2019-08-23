using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDebugger : MonoBehaviour
{
    Renderer rendere;
    // Start is called before the first frame update
    void Start()
    {
       rendere = GetComponent<Renderer>();
    }
    void OnDrawGizmosSelected() {

#if UNITY_EDITOR
        Gizmos.color = Color.red;
        Debug.LogWarning(rendere.bounds.center.ToString()+ "   " + rendere.bounds.size.ToString());
        Gizmos.DrawWireCube(rendere.bounds.center, rendere.bounds.size);

#endif
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
