﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WS3;

public class Weapon : MonoBehaviourPunCallbacks
{

    [SerializeField] private float damage = 1f;
    [SerializeField] private ParticleSystem fireFlash;


    new public Camera camera;
    public GameObject bullet;
    public Transform BulletSpawn;
    public float speed = 20f;
    public Transform weapon;
    private bool Shot = true;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1") && Shot)
        {
            Debug.Log("je tire");
            //Shoot();
            photonView.RPC("ShootAntiVirus", RpcTarget.AllViaServer, BulletSpawn.position, speed*weapon.forward);

            StartCoroutine(DelayShot());
        }

    }

   

    [PunRPC]
    void ShootAntiVirus(Vector3 position, Vector3 directionAndSpeed, PhotonMessageInfo info)
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
        // Tips for Photon lag compensation. Il faut compenser le temps de lag pour l'envoi du message.
        // donc décaler la position de départ de la balle dans la direction
        float lag = (float)(PhotonNetwork.Time - info.SentServerTime);
       // Debug.LogFormat("PunRPC: ThrowVirus {0} -> {1} lag:{2}", position, directionAndSpeed, lag);
        //Debug.Log("rotation du spawn"+ BulletSpawn.transform.rotation);

        // Create the Snowball from the Snowball Prefab
        GameObject Bullet = Instantiate(
            bullet,
            position + directionAndSpeed * Mathf.Clamp(lag, 0, 1.0f),Quaternion.Euler(BulletSpawn.transform.eulerAngles));



        // Add velocity to the Snowball
        Bullet.GetComponent<Rigidbody>().velocity = directionAndSpeed;

        // Destroy the Snowball after 5 seconds
        Destroy(Bullet, 5.0f);
    }

    IEnumerator DelayShot()
    {
        Shot = false;
        yield return new WaitForSeconds(AppConfig.Inst.DelayShot);
        Shot = true;
    }
}
