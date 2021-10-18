using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WS3;

public class ChargeAntiViraleBehaviour : MonoBehaviour
{
    public float Damage = 1f;
    public Transform weapon;
    private float viraleR = 0f;
    private float viraleG = 1f;
    private float viraleB = 0f;

    // Start is called before the first frame update

    void Start()
    {
        viraleR = AppConfig.Inst.ColorShotKMS_R;
        viraleG = AppConfig.Inst.ColorShotKMS_G;
        viraleB = AppConfig.Inst.ColorShotKMS_B;
        
        GetComponent<TrailRenderer>().startColor = new Color(viraleR, viraleG, viraleB);
        GetComponent<TrailRenderer>().endColor = new Color(viraleR, viraleG, viraleB);
       
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
