using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] float damage = 1f;

    new public Camera camera;

    public ParticleSystem fireFlash;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1")) Shoot();

    }

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
