using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WS3;

public class ChargeViraleBehaviour : MonoBehaviour
{
    public float Damage = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
