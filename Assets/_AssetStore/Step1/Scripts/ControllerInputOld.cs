using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Photon.Pun;

public class ControllerInputOld : MonoBehaviour
{
    public SteamVR_Input_Sources input;
    private bool isGrabbingPinch;
    private GameObject SelectedObject;
    public GameObject CameraRig;
    public GameObject Camera;
    private ControllerPointer pointer;
    //public delegate void OnGrabDel(ControllerInput controller);

    //public event OnGrabDel OnGrabPressedEvent;
    //public event OnGrabDel OnGrabReleasedEvent;

    private void Awake()
    {
        input = this.GetComponent<SteamVR_Behaviour_Pose>().inputSource;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Actions._default.GrabPinch.GetStateDown(input))
        {
            isGrabbingPinch = true;
            Debug.Log(input + " GrabbingPinch : " + isGrabbingPinch);
            if (SelectedObject != null)
            {
                this.GetComponent<PhotonView>().RPC("GrabSelectedObject", RpcTarget.AllViaServer);
            }

            /*if (OnGrabPressedEvent != null)
            {
                OnGrabPressedEvent(this);
            }*/
        }
        if (SteamVR_Actions._default.GrabPinch.GetStateUp(input))
        {
            if (SelectedObject!=null)
            {
                this.GetComponent<PhotonView>().RPC("UngrabTouchedObject", RpcTarget.AllViaServer);
            }
            

           /* if (OnGrabReleasedEvent != null)
            {
                OnGrabReleasedEvent(this);
            }*/
        }

        if (SteamVR_Actions._default.Teleport.GetStateDown(input)){
            TeleportPressed();
        }
        if (SteamVR_Actions._default.Teleport.GetStateUp(input))
        {
            TeleportReleased();
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GrabbableObject>())
        {
            SelectedObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (SelectedObject == other.gameObject)
        {
            SelectedObject = null;
        }
    }

    [PunRPC]
    public void GrabSelectedObject()
    {
        FixedJoint fx  = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        fx.connectedBody = SelectedObject.GetComponent<Rigidbody>();
       
    }

    [PunRPC]
    public void UngrabTouchedObject()
    {
        FixedJoint fx = gameObject.GetComponent<FixedJoint>();
        if (fx)
        {

            fx.connectedBody = null;
            Destroy(fx);
            Rigidbody body = SelectedObject.GetComponent<Rigidbody>();
            body.velocity = gameObject.GetComponent<SteamVR_Behaviour_Pose>().GetVelocity();
            body.angularVelocity = gameObject.GetComponent<SteamVR_Behaviour_Pose>().GetAngularVelocity();
        }
    }

    public void TeleportPressed()
    {
        pointer = gameObject.GetComponent<ControllerPointer>();
        if (pointer==null)
        {
            pointer = gameObject.AddComponent<ControllerPointer>();
            pointer.UpdateColor(Color.green);
        }
        else
        {
            pointer.UpdateColor(Color.green);
        }
        

    }
    public void TeleportReleased()
    {
        if (pointer.CanTeleport)
        {
            CameraRig.transform.position = pointer.TargetPosition + new Vector3(CameraRig.transform.position.x - Camera.transform.position.x, 0, CameraRig.transform.position.z - Camera.transform.position.z); 
            pointer.DesactivatePointer();
            Destroy(pointer);
        }
    }

}
