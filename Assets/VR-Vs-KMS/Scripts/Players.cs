using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{

    public float maxLife = 5;

    public float currentLife = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentLife <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void OnHit(float damage)
    {
        currentLife = currentLife - damage;
    }
}
