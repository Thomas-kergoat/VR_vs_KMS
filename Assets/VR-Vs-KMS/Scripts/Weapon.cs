using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WS3;

public class Weapon : MonoBehaviourPunCallbacks
{

    [SerializeField] float damage = 1f;

    new public Camera camera;

    public ParticleSystem fireFlash;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("je tire");
            Shoot();
            //photonView.RPC("Shoot", RpcTarget.AllViaServer);
        }

    }

    [PunRPC]
    private void Shoot()
    {
        fireFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
        {
            if(hit.collider.transform.tag == "VRPlayer")
            {
                if (hit.collider.transform.gameObject.GetComponent<Players>() != null)
                {
                    var target = hit.collider.transform.gameObject.GetComponent<Players>();
                    target.OnHit(damage);
                }
            }
            else if (hit.collider.transform.tag == "Shield")
            {
                if (hit.collider.transform.gameObject.GetComponent<Shield>() != null)
                {
                    var target = hit.collider.transform.gameObject.GetComponent<Shield>();
                    target.OnHitShield(damage);
                }
            }
        }
    }
}
