using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WS3;

public class ChargeAntiViraleBehaviour : MonoBehaviour
{
    public float Damage = 1f;
    public Transform weapon;
    // Start is called before the first frame update
    void Start()
    {
        //transform.localRotation = weapon.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        Debug.Log("ChargeAntiVirale hit something:" + hit);


        VR_CameraRigMultiUser um = hit.GetComponentInParent<VR_CameraRigMultiUser>();
        if (um != null)
        {
            Debug.Log("  It is a Vr player !!");
            um.OnHitKMS(Damage);
        }
        Destroy(gameObject);
    }
}
