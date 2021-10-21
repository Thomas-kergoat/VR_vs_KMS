using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBehaviour : MonoBehaviour
{
    private float VirusScore;
    private float AntiVirusScore;
    private float VirusZone;
    private float AntiVirusZone;

    private RoundManager RoundManager;
    // Start is called before the first frame update
    void Start()
    {
        RoundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        AntiVirusScore = RoundManager.VirusKilled;
        VirusScore = RoundManager.AntiVirusKilled;

        GetComponent<TextMeshProUGUI>().text = "Virus kill : " + VirusScore + " / " + AntiVirusScore + " : AntiVirus kill" ;

    }
}
