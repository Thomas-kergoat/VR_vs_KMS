using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WS3;

public class ChargeViraleBehaviour : MonoBehaviour
{
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


        UserManager um = hit.GetComponent<UserManager>();
        if (um != null)
        {
            Debug.Log("  It is a player !!");
            um.HitByVirus();
        }
        Destroy(gameObject);
    }
}
