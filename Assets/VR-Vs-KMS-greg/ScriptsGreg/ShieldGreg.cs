using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldGreg : MonoBehaviourPunCallbacks
{

    public AudioSource soundOnHit;

    [SerializeField] private float maxShieldLife = 5;

    [SerializeField] private float currentShieldLife = 5;

    private float PercentOfShield;

    public Image blueBar;

    // Start is called before the first frame update
    void Start()
    {
        transform.tag = "Shield";
    }

    // Update is called once per frame
    void Update()
    {
        PercentOfShield = (currentShieldLife * 50 )/maxShieldLife;

        if (currentShieldLife <= 0)
        {
            Destroy(gameObject);
            blueBar.gameObject.SetActive(false);
        }
        else
        {
            blueBar.rectTransform.sizeDelta = new Vector2(PercentOfShield, blueBar.rectTransform.sizeDelta.y);
        }

    }
    public void OnHitShield(float damage)
    {
        currentShieldLife = currentShieldLife - damage;
        Debug.Log("shield : intégrité  " + currentShieldLife);
        transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);
        if (!soundOnHit.isPlaying) soundOnHit.Play();

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentShieldLife);
        }
        else
        {
            currentShieldLife = (int)stream.ReceiveNext();
        }
    }
}
