using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{

    public GameObject redDotPrefab;

    public GameObject player;

    public GameObject blackDot;

    public List<GameObject> VRPlayersDots;

    public List<GameObject> VRPlayers;

    private float mapH, mapW, oldFrame, differenceFrames; 
    private Vector3 distancePlayers;
    private Image map;

    void Start()
    {

        InvokeRepeating("RefreshPlayers", 0f, 2.0f);

        oldFrame = player.transform.eulerAngles.y;

        map = GetComponent<Image>();

        mapH = map.rectTransform.sizeDelta.y;

        mapW = map.rectTransform.sizeDelta.x;

    }

    void Update()
    {

        for (int i = 0; i < VRPlayersDots.Count; i++)
        {

            distancePlayers = VRPlayers[i].transform.position - player.transform.position;

            differenceFrames = oldFrame - player.transform.eulerAngles.y;

            if (differenceFrames < 10 && differenceFrames > -10) map.rectTransform.Rotate(0, 0, -differenceFrames * 0.5f);

            VRPlayersDots[i].GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(distancePlayers.x * mapW * 0.5f, distancePlayers.z * mapH * 0.5f);

        }

        oldFrame = player.transform.eulerAngles.y;

    }

    private void RefreshPlayers()
    {
        VRPlayers.Clear();

        VRPlayersDots.Clear();

        foreach (GameObject radarDot in GameObject.FindGameObjectsWithTag("RadarDot"))
        {

            Destroy(radarDot);

        }

        Debug.Log("Refreshing");

        foreach (GameObject vPlayer in GameObject.FindGameObjectsWithTag("KMSPlayer"))
        {

            GameObject newDot = Instantiate(redDotPrefab);

            newDot.transform.SetParent(blackDot.transform, false);

            newDot.tag = "RadarDot";

            VRPlayersDots.Add(newDot);

            VRPlayers.Add(vPlayer);

        }

    }

}