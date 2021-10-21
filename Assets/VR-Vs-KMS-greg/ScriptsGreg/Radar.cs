using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{

    public GameObject redDotPrefab;

    public GameObject player;

    public GameObject blackDot;

    public List<GameObject> KMSPlayersDots;

    public List<GameObject> KMSPlayers;

    private float mapH, mapW, oldFrame, differenceFrames;
    private Vector3 distancePlayers;
    private Image map;

    void Start()
    {

        InvokeRepeating("RefreshPlayers", 0f, 2.0f);

        oldFrame = player.transform.eulerAngles.y;

        map = GetComponent<Image>();

        mapH = 50;

        mapW = 50;

        foreach (GameObject radarDot in GameObject.FindGameObjectsWithTag("RadarDot"))
        {

            Destroy(radarDot);

        }

        foreach (GameObject vPlayer in GameObject.FindGameObjectsWithTag("KeyboardPlayer"))
        {

            GameObject newDot = Instantiate(redDotPrefab);

            newDot.transform.localScale = new Vector3(1, 1, 1);

            newDot.transform.SetParent(blackDot.transform, false);

            newDot.tag = "RadarDot";

            KMSPlayersDots.Add(newDot);

            KMSPlayers.Add(vPlayer);

        }

        Debug.Log( "ICIIIIIIIIIIII "  +  KMSPlayers);

    }

    void Update()
    {

        for (int i = 0; i < KMSPlayersDots.Count; i++)
        {

            distancePlayers = KMSPlayers[i].transform.position - player.transform.position;

            differenceFrames = oldFrame - player.transform.eulerAngles.y;

            if (differenceFrames < 10 && differenceFrames > -10) map.rectTransform.Rotate(0, 0, -differenceFrames);

            KMSPlayersDots[i].GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(distancePlayers.x * mapW * 0.5f, distancePlayers.z * mapH * 0.5f);

        }

        oldFrame = player.transform.eulerAngles.y;

    }

    private void RefreshPlayers()
    {
        KMSPlayers.Clear();

        KMSPlayersDots.Clear();

        foreach (GameObject radarDot in GameObject.FindGameObjectsWithTag("RadarDot"))
        {

            Destroy(radarDot);

        }

        foreach (GameObject vPlayer in GameObject.FindGameObjectsWithTag("KeyboardPlayer"))
        {

            GameObject newDot = Instantiate(redDotPrefab);

            newDot.transform.localScale = new Vector3(1, 1, 1);

            newDot.transform.SetParent(blackDot.transform, false);

            newDot.tag = "RadarDot";

            KMSPlayersDots.Add(newDot);

            KMSPlayers.Add(vPlayer);

        }

        Debug.Log("ICIIIIIIIIIIII2 " + KMSPlayers);

    }

}