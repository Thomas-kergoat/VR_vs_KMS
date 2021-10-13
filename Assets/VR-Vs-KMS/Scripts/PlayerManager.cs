using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerManager : MonoBehaviourPun
{
    public List<MonoBehaviour> monoBehaviours;
    public List<GameObject> gameObjects;
    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine)
        {
            foreach (var item in monoBehaviours)
            {
                item.enabled = false;
            }

            GetComponentInChildren<Camera>().enabled = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
