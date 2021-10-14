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
            //Shoot();
            photonView.RPC("Shoot", RpcTarget.AllViaServer);
        }

    }

    [PunRPC]
    private void Shoot()
    {
        fireFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
        {
            Debug.Log("touché : " + hit.transform.tag);
            if(hit.transform.tag == "VRPlayer")
            {
                
                if (hit.transform.gameObject.GetComponentInParent<VR_CameraRigMultiUser>() != null)
                {
                    Debug.Log("il est touché !");
                    var target = hit.transform.gameObject.GetComponentInParent<VR_CameraRigMultiUser>();
                    target.OnHitKMS(damage);
                }
            }
        }
    }
}
