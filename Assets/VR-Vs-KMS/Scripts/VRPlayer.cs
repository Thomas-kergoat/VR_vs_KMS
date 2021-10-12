using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "VRPlayer";

        Players vPlayer = new Players();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
