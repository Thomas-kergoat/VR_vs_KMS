using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBodyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    CapsuleCollider collider;
    ParticleSystem particleSystem;
    float maxsize = 1.8f;
    float minsize = 1f;
    void Start()
    {
        collider= GetComponent<CapsuleCollider>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0,0-transform.parent.position.y,0);
        collider.height = transform.parent.position.y* 1.5f;
        particleSystem.startLifetime = Mathf.Clamp(transform.parent.position.y, minsize, maxsize) *1.1f;
    }
}
