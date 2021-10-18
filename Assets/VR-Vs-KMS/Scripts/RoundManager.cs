using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public Text score;

    private bool gameOnGoing = false;

    public List<GameObject> VRPlayer;

    public List<GameObject> keyboardPlayer;

    public float VirusKilled = 0;
    public float AntiVirusKilled = 0;
    private float NbContaminatedtedPlayerToVictory;

    void Start()
    {
        NbContaminatedtedPlayerToVictory = AppConfig.Inst.NbContaminationPlayerToVictory;
        gameOnGoing = false;

    }

    // Update is called once per frame
    void Update()
    {

        if(VirusKilled == NbContaminatedtedPlayerToVictory )
        {
            score.text = "kPlayers Win";
            gameOnGoing = false;

        } else if(AntiVirusKilled == NbContaminatedtedPlayerToVictory)
        {
            score.text = "vPlayers Win";
            gameOnGoing = false;

        } else
        {
            //score.text = "kPlayer kills : " + VirusKilled + "\n VRPlayer kills : " + AntiVirusKilled;
            gameOnGoing = true;
        }


    }

    public void KillPlayer(GameObject player)
    {
        if (player.tag == "KeyboardPlayer")
        {
            AntiVirusKilled++;
        }
        else if (player.tag == "VRPlayer")
        {
            VirusKilled++;
        }
    }
}
