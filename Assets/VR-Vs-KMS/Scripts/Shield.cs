using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private float maxShieldLife = 5;

    [SerializeField] private float currentShieldLife = 5;

    // Start is called before the first frame update
    void Start()
    {
        transform.tag = "Shield";

    }

    // Update is called once per frame
    void Update()
    {

        if (currentShieldLife <= 0)
        {
            Destroy(transform);
        }

    }
    public void OnHitShield(float damage)
    {
        currentShieldLife = currentShieldLife - damage;

        transform.localScale += new Vector3(0, -0.2f, -0.2f);

    }
}
