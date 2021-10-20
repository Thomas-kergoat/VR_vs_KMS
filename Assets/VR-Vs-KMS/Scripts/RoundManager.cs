using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WS3;

public class RoundManager : MonoBehaviourPunCallbacks 
{
    public Text score;

    public NetworkManager NetworkManager;

    public float VirusKilled = 0;
    public float AntiVirusKilled = 0;
    private float NbContaminatedtedPlayerToVictory;

    void Start()
    {
        NbContaminatedtedPlayerToVictory = AppConfig.Inst.NbContaminationPlayerToVictory;
        NetworkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(VirusKilled == NbContaminatedtedPlayerToVictory )
        {
            //score.text = "kPlayers Win";
            Debug.Log("kPlayers Win");
            NetworkManager.KillRoom();
            //photonView.RPC("EndGame", RpcTarget.AllViaServer);
           

        } else if(AntiVirusKilled == NbContaminatedtedPlayerToVictory)
        {
            //score.text = "vPlayers Win";
            Debug.Log("vPlayers Win");
            NetworkManager.KillRoom();
            //photonView.RPC("EndGame", RpcTarget.AllViaServer);
            
        }
    }


    public void KillPlayer(GameObject player)
    {
        Debug.Log("je rentre dans la fonction");
        Debug.Log(player.transform.tag);
        
        if (player.transform.tag == "KeyboardPlayer")
        {
            AntiVirusKilled++;
            Debug.Log("+1 antivirus");
        }
        else if (player.transform.tag == "VRGameObject")
        {
            VirusKilled++;
            Debug.Log("+1 virus");
        }
    }

   /* public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(VirusKilled);
        }
        else
        {
            VirusKilled = (float)stream.ReceiveNext();
        }
    }*/

    [PunRPC]
    public void EndGame()
    {
       PhotonNetwork.LeaveRoom();
    }
}
