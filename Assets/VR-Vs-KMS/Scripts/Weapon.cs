using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WS3;

public class Weapon : MonoBehaviourPunCallbacks
{

    [SerializeField] float damage = 1f;

    public Camera camera;
    public GameObject aim;
    public AudioSource shootSound;
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
            Debug.DrawRay(aim.transform.position, aim.transform.forward*100f, Color.cyan, 1.5f);
            RaycastHit hit;
            Ray rayon = new Ray(aim.transform.position, aim.transform.forward);

            if (Physics.Raycast(rayon, out hit))
            {
                //Debug.DrawRay(rayon.origin , hit.point, Color.cyan, 1.5f);
                photonView.RPC("ShootAntiVirus", RpcTarget.AllViaServer, BulletSpawn.position, rayon.direction, hit.point);
            }

            StartCoroutine(DelayShot());
        }

    }

   

    [PunRPC]
    void ShootAntiVirus(Vector3 position, Vector3 directionAndSpeed, Vector3 hit,PhotonMessageInfo info)
    {
        // Tips for Photon lag compensation. Il faut compenser le temps de lag pour l'envoi du message.
        // donc décaler la position de départ de la balle dans la direction
        float lag = (float)(PhotonNetwork.Time - info.SentServerTime);
        // Debug.LogFormat("PunRPC: ThrowVirus {0} -> {1} lag:{2}", position, directionAndSpeed, lag);
        //Debug.Log("rotation du spawn"+ BulletSpawn.transform.rotation);
        //weapon.transform.forward = aim.transform.forward - new Vector3(weapon.position.x-aim.transform.position.x, weapon.position.y - aim.transform.position.y, weapon.position.z - aim.transform.position.z);

        // Create the Snowball from the Snowball Prefab
        GameObject Bullet = Instantiate(
            bullet,
            position+directionAndSpeed* Mathf.Clamp(lag, 0, 1.0f),Quaternion.identity);
        
        shootSound.Play();



        Bullet.transform.LookAt(hit);
        // Add velocity to the Snowball
        Bullet.GetComponent<Rigidbody>().velocity = (directionAndSpeed + Bullet.transform.forward+ new Vector3(0, 0.05f, 0)) *speed;

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
