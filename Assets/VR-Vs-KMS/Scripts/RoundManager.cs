using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using vr_vs_kms;
using WS3;

public class RoundManager : MonoBehaviourPunCallbacks 
{
    public Text score;

    public NetworkManager NetworkManager;
    public List<GameObject> contaminationArea;

    public float NbContAreaVirus;
    public float NbContAreaAntiVirus;
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
        NbContAreaVirus = 0;
        NbContAreaAntiVirus = 0;
        if (VirusKilled == NbContaminatedtedPlayerToVictory )
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

        foreach (GameObject contamination in contaminationArea)
        {
            if (contamination.GetComponent<ContaminationArea>().capturedBy == "VRPlayer")
            {
                NbContAreaVirus++;
            }
            else if (contamination.GetComponent<ContaminationArea>().capturedBy == "KeyboardPlayer")
            {
                NbContAreaAntiVirus++;
            }
        }

        if (NbContAreaVirus == contaminationArea.Count)
        {
            Debug.Log("vPlayers Win");
            photonView.RPC("EndGame",RpcTarget.All);
           
        }
        else if (NbContAreaAntiVirus == contaminationArea.Count)
        {
            Debug.Log("kPlayers Win");
            photonView.RPC("EndGame", RpcTarget.All);
            
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
       NetworkManager.KillRoom();
    }
}
