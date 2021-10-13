using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    new public Camera camera;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject weapon;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerRotation = new Vector3(0, Input.GetAxis("Mouse X") * -1, 0);
        Vector3 cameraRotation = new Vector3(Input.GetAxis("Mouse Y"), 0, 0);

        player.transform.eulerAngles = player.transform.eulerAngles - playerRotation * speed;
        camera.transform.eulerAngles = camera.transform.eulerAngles - cameraRotation * speed;

        weapon.transform.Rotate(cameraRotation * speed * -1);
    }
}
