using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WS3;

public class Players : MonoBehaviourPunCallbacks, IPunObservable
{
    public float maxLife;
    public float currentLife = 5;
    private float PercentOfHp;

    public RoundManager roundManager;

    public AudioSource spawnSound;
    public AudioSource deathSound;

    public AudioSource soundOnHit;

    public Image RedBar;

    public Slider slider;

    public Image sliderFill;

    private NetworkManager net;
    private GameObject netObj;
    // Start is called before the first frame update
    void Start()
    {
       
        maxLife = AppConfig.Inst.LifeNumber;
        netObj = GameObject.Find("NetworkManager");
        net = netObj.GetComponent<NetworkManager>();
        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
        spawnSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!photonView.IsMine) return;
       

        

    }

    public void OnHit(float damage)
    {
        if (photonView.IsMine)
        {
            currentLife = currentLife - damage;
            Debug.Log("KMSUSER :  je suis touché il me reste : " + currentLife + " hp !!!");
            soundOnHit.Play();
        }
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentLife);
            PercentOfHp = (currentLife * 100) / maxLife;
            if (currentLife <= 0)
            {
                Debug.Log("Arghh je meurs !!!");
                deathSound.Play();


                roundManager.KillPlayer(gameObject);

                if (net)
                {
                    StartCoroutine(Delay(gameObject));

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
        else
        {
            currentLife = (float)stream.ReceiveNext();
            Debug.Log(currentLife + "iuiseghfiusgiegh");
            if (currentLife <= 0)
            {
                Debug.Log("je détruit l'autre !!!");
                roundManager.KillPlayer(gameObject);
                Destroy(gameObject);

            }
        }
    }

    
    IEnumerator Delay(GameObject gameObject)
    {
        yield return new WaitForSeconds(0f);
        Debug.Log("je me détrui");
        Destroy(gameObject);
        net.respawn();
    }
}
