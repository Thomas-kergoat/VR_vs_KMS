using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.ThirdPerson;

namespace WS3
{
    public class UserManager : MonoBehaviourPunCallbacks
    {
        public static GameObject UserMeInstance;
        public GameObject ChargeVirale;
        public Transform ChargeSpawner;
        public float speed = 5f;

        public Material PlayerLocalMat;
        /// <summary>
        /// Represents the GameObject on which to change the color for the local player
        /// </summary>

        /// <summary>
        /// The FreeLookCameraRig GameObject to configure for the UserMe
        /// </summary>
        GameObject goFreeLookCameraRig = null;

        
        void Awake()
        {
            if (photonView.IsMine)
            {
                Debug.LogFormat("Avatar UserMe created for userId {0}", photonView.ViewID);
                UserMeInstance = gameObject;

            }
        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("isLocalPlayer:" + photonView.IsMine);
            Health = 3;
            updateGoFreeLookCameraRig();
            followLocalPlayer();
            activateLocalPlayer();
          
        }

                /// <summary>
        /// Get the GameObject of the CameraRig
        /// </summary>
        protected void updateGoFreeLookCameraRig()
        {
            if (!photonView.IsMine) return;
            try
            {
                // Get the Camera to set as the followed camera
                goFreeLookCameraRig = transform.Find("/FreeLookCameraRig").gameObject;
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Warning, no goFreeLookCameraRig found\n" + ex);
            }
        }

        /// <summary>
        /// Make the CameraRig following the LocalPlayer only.
        /// </summary>
        protected void followLocalPlayer()
        {
            if (photonView.IsMine)
            {
                if (goFreeLookCameraRig != null)
                {
                    // find Avatar EthanHips
                    Transform transformFollow = transform.Find("EthanSkeleton/EthanHips") != null ? transform.Find("EthanSkeleton/EthanHips") : transform;
                    // call the SetTarget on the FreeLookCam attached to the FreeLookCameraRig
                    goFreeLookCameraRig.GetComponent<FreeLookCam>().SetTarget(transformFollow);
                    Debug.Log("ThirdPersonControllerMultiuser follow:" + transformFollow);
                }
            }
        }

        protected void activateLocalPlayer()
        {
            // enable the ThirdPersonUserControl if it is a Loacl player = UserMe
            // disable the ThirdPersonUserControl if it is not a Loacl player = UserOther
            GetComponent<ThirdPersonUserControl>().enabled = photonView.IsMine;
            GetComponent<Rigidbody>().isKinematic = !photonView.IsMine;
            if (photonView.IsMine)
            {
                try
                {
                    // Change the material of the Ethan Glasses
                }
                catch (System.Exception)
                {

                }
            }
        }


       
        // Update is called once per frame
        void Update()
        {
            // Don't do anything if we are not the UserMe isLocalPlayer
            

        }


        [SerializeField] private Material Health3;
        [SerializeField] private Material Health2;
        [SerializeField] private Material Health1;

        private int previousHealth;
        public int Health { get; private set; }
        /// <summary>
        /// The Transform from which the snow ball is spawned
        /// </summary>
        [SerializeField] float ForceHit;

        public void UpdateHealthMaterial()
        {
            try
            {

            }
            catch (System.Exception)
            {

            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(Health);
            }
            else
            {
                Health = (int)stream.ReceiveNext();
            }

            if (previousHealth != Health) UpdateHealthMaterial();
            previousHealth = Health;
        }

        public void HitByVirus()
        {
            if (!photonView.IsMine) return;
            Debug.Log("Got me");
            var rb = GetComponent<Rigidbody>();
            rb.AddForce((-transform.forward + (transform.up * 0.1f)) * ForceHit, ForceMode.Impulse);


            // Manage to leave room as UserMe
            if (--Health <= 0)
            {
                PhotonNetwork.LeaveRoom();
            }
        }



    }
}
