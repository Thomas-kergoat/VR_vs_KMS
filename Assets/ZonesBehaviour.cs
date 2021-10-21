using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZonesBehaviour : MonoBehaviour
{
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
        VirusZone = RoundManager.NbContAreaVirus;
        AntiVirusZone = RoundManager.NbContAreaAntiVirus;

        GetComponent<TextMeshProUGUI>().text = "Virus zone : " + VirusZone + " / " + AntiVirusZone + " : AntiVirus Zone";

    }
}
