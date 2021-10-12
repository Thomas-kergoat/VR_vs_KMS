using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public Text score;

    private bool gameOnGoing = false;

    private List<GameObject> VRPlayer;

    private List<GameObject> keyboardPlayer;

    void Start()
    {
        foreach (GameObject kPlayer in GameObject.FindGameObjectsWithTag("KeyboardPlayer"))
        {

            keyboardPlayer.Add(kPlayer);
            Debug.Log("Object added");
        }

        foreach (GameObject vPlayer in GameObject.FindGameObjectsWithTag("VRPlayer"))
        {

            VRPlayer.Add(vPlayer);
            Debug.Log("Object added");
        }

        gameOnGoing = false;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(VRPlayer.Count);

        if(VRPlayer.Count <= 0)
        {
            score.text = "kPlayers Win";
            gameOnGoing = false;

        } else if(keyboardPlayer.Count <= 0)
        {
            score.text = "vPlayers Win";
            gameOnGoing = false;

        } else
        {
            score.text = "Number of kPlayer : " + VRPlayer.Count + " | Number of vPlayer : " + keyboardPlayer.Count;
            gameOnGoing = true;
        }


    }
}
