using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace WS3
{
    public class VR_CameraRigMultiUser : MonoBehaviourPunCallbacks
    {
        // reference to SteamController
        public GameObject SteamVRLeft, SteamVRRight, SteamVRCamera;
        public GameObject UserOtherLeftHandModel, UserOtherRightHandModel;
        private GameObject goFreeLookCameraRig;

        // Start is called before the first frame update
        void Start()
        {
            updateGoFreeLookCameraRig();
            steamVRactivation();
        }

        private void updateGoFreeLookCameraRig()
        {
            // Client execution ONLY LOCAL
            if (!photonView.IsMine) return;

            goFreeLookCameraRig = null;

            try
            {
                // Get the Camera to set as the follow camera
                goFreeLookCameraRig = transform.Find("/FreeLookCameraRig").gameObject;
                // Deactivate the FreeLookCameraRig since we are using the SteamVR camera
                //...
                goFreeLookCameraRig.SetActive(false);
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Warning, no goFreeLookCameraRig found\n" + ex);
            }
        }

        private void steamVRactivation()
        {
            // client execution for ALL

            // Left activation if UserMe, deactivation if UserOther
            //...
            SteamVRLeft.GetComponent<SteamVR_Behaviour_Pose>().enabled = photonView.IsMine;
            // Left SteamVR_RenderModel activation if UserMe, deactivation if UserOther
            //...
            SteamVRLeft.GetComponentInChildren<SteamVR_RenderModel>().enabled = photonView.IsMine;
            //SteamVRLeft.GetComponentInChildren<SkinnedMeshRenderer>().enabled = photonView.IsMine;
            //SteamVRLeft.transform.Find("Model").gameObject.SetActive(photonView.IsMine);
            // Right activation if UserMe, deactivation if UserOther
            //...
            SteamVRRight.GetComponent<SteamVR_Behaviour_Pose>().enabled = photonView.IsMine;
            // Right SteamVR_RenderModel activation if UserMe, deactivation if UserOther
            //...
            //SteamVRRight.GetComponentInChildren<SkinnedMeshRenderer>().enabled = photonView.IsMine;
            SteamVRRight.GetComponentInChildren<SteamVR_RenderModel>().enabled = photonView.IsMine;
           // SteamVRRight.transform.Find("Model").gameObject.SetActive(photonView.IsMine);
            // Camera activation if UserMe, deactivation if UserOther
            //...
            SteamVRCamera.GetComponent<Camera>().enabled = photonView.IsMine;
            

            if (!photonView.IsMine)
            {
                // ONLY for player OTHER

                // Create the model of the LEFT Hand for the UserOther, use a SteamVR model  Assets/SteamVR/Models/vr_glove_left_model_slim.fbx
                var modelLeft = Instantiate(UserOtherLeftHandModel);
                // Put it as a child of the SteamVRLeft Game Object
                modelLeft.transform.parent = SteamVRLeft.transform;

                // Create the model of the RIGHT Hand for the UserOther Assets/SteamVR/Models/vr_glove_right_model_slim.fbx
                var modelRight = Instantiate(UserOtherRightHandModel);
                // Put it as a child of the SteamVRRight Game Object
                modelRight.transform.parent = SteamVRRight.transform;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

