using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRaycaster : MonoBehaviour
{
    LayerMask LayerMask;
    // Start is called before the first frame update
    void Start()
    {
        LayerMask = 1 << 8;
        LayerMask = ~LayerMask;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public float GetGroundLevel() {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 20, Color.yellow);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity,LayerMask)) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            
        }
        return hit.point.y;
    }
}
