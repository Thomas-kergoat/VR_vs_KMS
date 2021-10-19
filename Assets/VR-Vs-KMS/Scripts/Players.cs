using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WS3;

public class Players : MonoBehaviourPunCallbacks
{
    public float maxLife;

    public float currentLife = 5;

    private float PercentOfHp;

    public RoundManager roundManager;

    public Image RedBar;
    NetworkManager net;
    // Start is called before the first frame update
    void Start()
    {
        maxLife = AppConfig.Inst.LifeNumber;
        net = GameObject.FindObjectOfType<NetworkManager>();
        roundManager = GameObject.FindObjectOfType<RoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PercentOfHp = currentLife / maxLife * 100;

        if (currentLife <= 0)
        {

            
            roundManager.KillPlayer(gameObject);
            PhotonNetwork.Destroy(gameObject);
            Debug.Log("Arghh je meurs !!!");
            if (net)
            {
                net.respawn();
               
            }
            else
            {
                Debug.Log("Network manager nout found disconnection");
                PhotonNetwork.LeaveLobby();
                
            }
        } 
        else
        {
            RedBar.rectTransform.sizeDelta = new Vector2(PercentOfHp, RedBar.rectTransform.sizeDelta.y);
        }

    }

    public void OnHit(float damage)
    {
        if (photonView.IsMine)
        {
            currentLife = currentLife - damage;

            Debug.Log("KMSUSER :  je suis touché il me reste : " + currentLife + " hp !!!");
        }
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentLife);
        }
        else
        {
            currentLife = (int)stream.ReceiveNext();
        }
    }

    IEnumerator DelayRespawn()
    {
        PhotonNetwork.Destroy(gameObject);
        yield return new WaitForSeconds(1.5f);
        net.respawn();

    }
}
