using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersGreg : MonoBehaviourPunCallbacks
{
    public float maxLife = 5;

    public float currentLife = 5;

    private float PercentOfHp;

    public RoundManager roundManager;

    public Image RedBar;

    public Slider slider;

    public Image sliderFill;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        PercentOfHp = currentLife / maxLife * 50;

        if (currentLife <= 0)
        {
            Destroy(gameObject);
            PhotonNetwork.LeaveRoom();
        } 
        else
        {
            RedBar.rectTransform.sizeDelta = new Vector2(PercentOfHp, RedBar.rectTransform.sizeDelta.y);
        }
    }

    public void OnHit(float damage)
    {
        currentLife = currentLife - damage;

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

}
