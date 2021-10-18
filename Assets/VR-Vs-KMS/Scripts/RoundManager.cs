using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public TextMeshPro score;

    private bool gameOnGoing = false;

    public List<GameObject> VRPlayer;

    public List<GameObject> keyboardPlayer;

    void Start()
    {
        foreach (GameObject kPlayer in GameObject.FindGameObjectsWithTag("KMSPlayer"))
        {

            keyboardPlayer.Add(kPlayer);
        }

        foreach (GameObject vPlayer in GameObject.FindGameObjectsWithTag("VRPlayer"))
        {

            VRPlayer.Add(vPlayer);
        }

        gameOnGoing = false;

    }

    // Update is called once per frame
    void Update()
    {

        if(VRPlayer.Count <= 0)
        {
            score.text = "KMSPlayer Win";
            gameOnGoing = false;

        } else if(keyboardPlayer.Count <= 0)
        {
            score.text = "VRPlayer Win";
            gameOnGoing = false;

        } else
        {
            score.text = VRPlayer.Count + " ||| " + keyboardPlayer.Count;
            gameOnGoing = true;
        }


    }

    public void DestroyPlayer(GameObject player)
    {
        if(player.tag == "KMSPlayer")
        {
            for (int i = 0; i< keyboardPlayer.Count; i++)
            {
                if (keyboardPlayer[i].GetInstanceID() == player.GetInstanceID())
                {
                    keyboardPlayer.RemoveAt(i);
                }
            }
        }
        else if (player.tag == "VRPlayer")
        {
            for (int i = 0; i< VRPlayer.Count; i++)
            {
                if (VRPlayer[i].GetInstanceID() == player.GetInstanceID())
                {
                    VRPlayer.RemoveAt(i);
                }
            }
        }
    }
}
