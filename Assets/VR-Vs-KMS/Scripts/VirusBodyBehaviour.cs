using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBodyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    CapsuleCollider collider;
    void Start()
    {
        collider= GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0,0-transform.parent.position.y,0);
        transform.localRotation = new Quaternion(0 - transform.parent.rotation.x, 0 - transform.parent.rotation.y, 0 - transform.parent.rotation.z, 0 - transform.parent.rotation.w);
        collider.height = transform.parent.position.y* 1.5f;
    }
}
