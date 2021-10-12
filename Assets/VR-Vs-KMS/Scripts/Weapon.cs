using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] float damage = 1f;

    public Camera camera;

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
            if(hit.transform.tag == "VRPlayer")
            {
                if (hit.transform.gameObject.GetComponent<Players>() != null)
                {
                    var target = hit.transform.gameObject.GetComponent<Players>();
                    target.OnHit(damage);
                }
            }
        }
    }
}
