﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Photon.Pun;

public class ControllerInput : MonoBehaviour {

    public SteamVR_Input_Sources source;

    private ControllerPointer pointer;

    //private bool isGrabbingPinch;
    public GameObject CameraRig;
    public GameObject Camera;

    private Vector3 velocity;
    private Vector3 angularVelocity;

    private GameObject SelectedObject = null;

    private bool Teleport = true;
    
    void Awake()
    {
        source = GetComponent<SteamVR_Behaviour_Pose>().inputSource;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (SteamVR_Actions._default.GrabPinch.GetStateDown(source))
        {
            Debug.Log("Down");
            //isGrabbingPinch = true;
            //if(SelectedObject != null) this.GetComponent<PhotonView>().RPC("GrabSelectedObject", RpcTarget.AllViaServer);
            if (SelectedObject != null) GrabSelectedObject();

        }

        if (SteamVR_Actions._default.GrabPinch.GetStateUp(source))
        {
            //isGrabbingPinch = false;
            //this.GetComponent<PhotonView>().RPC("UngrabTouchedObject", RpcTarget.AllViaServer);
            if (SelectedObject != null) UngrabTouchedObject();
        }

        if (SteamVR_Actions._default.Teleport.GetStateDown(source)) {
            TeleportPressed();
        }

        if (SteamVR_Actions._default.Teleport.GetStateUp(source) && Teleport)
        {
            TeleportReleased();
            StartCoroutine(DelayTeleportVr());
        }

    }
    [PunRPC]
    public void GrabSelectedObject()
    {
        Debug.Log("je grab");
        SelectedObject.GetComponent<Rigidbody>().useGravity = false;
        if (gameObject.GetComponent<FixedJoint>() == null)
        {
            FixedJoint fx = gameObject.AddComponent<FixedJoint>();
            fx.connectedBody = SelectedObject.GetComponent<Rigidbody>();
            fx.breakForce = 20000;
            fx.breakTorque = 20000;
        }
    }
    [PunRPC]
    public void UngrabTouchedObject()
    {
        if (SelectedObject != null)
        {
            if (gameObject.GetComponent<FixedJoint>())
            {
                gameObject.GetComponent<FixedJoint>().connectedBody = null;
                Destroy(gameObject.GetComponent<FixedJoint>());

                SelectedObject.GetComponent<Rigidbody>().useGravity = true;
            }

        }

    }

    public void TeleportPressed()
    {
        pointer = gameObject.GetComponent<ControllerPointer>();
        if (pointer == null)
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
            CameraRig.transform.position = pointer.TargetPosition + new Vector3(CameraRig.transform.position.x - Camera.transform.position.x, 0, CameraRig.transform.position.z - Camera.transform.position.z) ;
            pointer.DesactivatePointer();
            Destroy(pointer);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Throwable")
        {

            Debug.Log("TriggerEnter");
            Debug.Log(other.name);
            SelectedObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UngrabTouchedObject();
        if (other.tag == "Throwable")
        {
            Debug.Log("TriggerExit");
            Debug.Log(other.name);
            if (SelectedObject == other.gameObject)
            {
                SelectedObject = null;
            }
        }
    }

    IEnumerator DelayTeleportVr()
    {
        Teleport = false;
        yield return new WaitForSeconds(AppConfig.Inst.DelayTeleport);
        Teleport = true;
    }

}
