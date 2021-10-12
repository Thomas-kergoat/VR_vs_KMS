using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{

    private bool gameOnGoing;

    private List<GameObject> VRPlayer;

    private List<GameObject> keyboardPlayer;

    void Start()
    {
        foreach (GameObject kPlayer in GameObject.FindGameObjectsWithTag("KeyboardPlayer"))
        {

            keyboardPlayer.Add(kPlayer);
        }

        foreach (GameObject vPlayer in GameObject.FindGameObjectsWithTag("VRPlayer"))
        {

            VRPlayer.Add(vPlayer);
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
