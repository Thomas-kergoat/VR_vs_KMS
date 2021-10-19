using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WS3;

public class ChargeViraleBehaviour : MonoBehaviour
{
    public float Damage = 1f;
    private float viraleR = 1f;
    private float viraleG = 0f;
    private float viraleB = 0f;

    ParticleSystem ParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
        viraleR = AppConfig.Inst.ColorShotVirus_R;
        viraleG = AppConfig.Inst.ColorShotVirus_G;
        viraleB = AppConfig.Inst.ColorShotVirus_B;
        ParticleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var main = ParticleSystem.main;
        main.startColor = new Color(viraleR, viraleG, viraleB);
    } 

    private void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        Debug.Log("ChargeVirale hit something:" + hit);


        Players um = hit.GetComponent<Players>();
        if (um != null)
        {
            Debug.Log("  It is a player !!");
            um.OnHit(Damage);
        }
        Destroy(gameObject);
    }
}
