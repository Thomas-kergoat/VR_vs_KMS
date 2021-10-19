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

    private float mapH, mapW, oldFrame, differenceFrames, clockwise = 1f; 
    private Vector3 distancePlayers, distanceDots;
    private Image map;

    void Start()
    {
        oldFrame = player.transform.eulerAngles.y;

        map = GetComponent<Image>();

        mapH = map.rectTransform.sizeDelta.y;

        mapW = map.rectTransform.sizeDelta.x;

        Debug.Log(mapH + " " + mapW);

        foreach (GameObject vPlayer in GameObject.FindGameObjectsWithTag("VRPlayer"))
        {

            GameObject newDot = Instantiate(redDotPrefab);

            newDot.transform.SetParent(blackDot.transform, false);

            VRPlayersDots.Add(newDot);

            VRPlayers.Add(vPlayer);

        }

    }

    void Update()
    {


        for (int i = 0; i < VRPlayersDots.Count; i++)
        {

            distancePlayers = VRPlayers[i].transform.position - player.transform.position;

            differenceFrames = oldFrame - player.transform.eulerAngles.y;

            if (differenceFrames < 10 && differenceFrames > -10) map.rectTransform.Rotate(0, 0, -differenceFrames );

            Debug.Log(differenceFrames);

            VRPlayersDots[i].GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(distancePlayers.x * mapW * 0.5f, distancePlayers.z * mapH * 0.5f);

        }

        oldFrame = player.transform.eulerAngles.y;

    }
}