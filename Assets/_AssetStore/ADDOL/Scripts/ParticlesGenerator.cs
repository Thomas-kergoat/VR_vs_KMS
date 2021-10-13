using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesGenerator : MonoBehaviour {

    [SerializeField] private ParticleSystem particles;

    [SerializeField] private Rigidbody rb;

    private float oldAngle;

    private Vector3 oldForward;

    private float diff;

    private float angleRotation = 720.0f;



	// Use this for initialization
	void Start () {
        oldAngle = transform.localEulerAngles.x;

        oldForward = transform.up;
    }
	
	// Update is called once per frame
	void Update () {

        // Tells if it turns clockwise or counterclockwise
        Vector3 cross = Vector3.Cross(oldForward, transform.up);

        // Calculate the angle traveled since the last frame
        diff = Mathf.Abs(oldAngle - transform.localEulerAngles.x);

        if (oldAngle != transform.localEulerAngles.x)
        {
            // Avoid jumps from 360 to 0
            if (diff < 150)
            {
                if (cross.z > 0f)
                {
                    angleRotation = angleRotation + diff;
                }
                else if (cross.z < 0f)
                {
                    angleRotation = angleRotation - diff;
                }
                else
                {
                    Debug.Log("NONE");
                }
            }
        }

        oldForward = transform.up;

        oldAngle = transform.localEulerAngles.x;
        
        //Debug.Log("angleRotation " + angleRotation);

        if (angleRotation >= 0)
        {
            if (!particles.isPlaying) particles.Play();
            particles.gameObject.SetActive(true);
        }
        else
        {
            if (particles.isPlaying) particles.Pause();
            particles.gameObject.SetActive(false);
        }
        
    }
}
