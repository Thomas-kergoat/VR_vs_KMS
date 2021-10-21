using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace WS3
{
    public class VR_CameraRigMultiUser : MonoBehaviourPunCallbacks, IPunObservable
    {
        // reference to SteamController
        public GameObject SteamVRLeft, SteamVRRight, SteamVRCamera;
        public GameObject UserOtherLeftHandModel, UserOtherRightHandModel;
        private GameObject goFreeLookCameraRig;
        public GameObject ChargeVirale;

        public Transform ChargeSpawner;

        public AudioSource shootSound;
        public AudioSource spawnSound;
        public AudioSource deathSound;

        public float speed = 15f;
        public float Health;
        public float maxLife;
        private float PercentOfHp;

        public SteamVR_Input_Sources source;

        private bool Shot = true;

        public NetworkManager net;
        public RoundManager roundManager;

        public Image RedBar;
        public Image sliderFill;

        public Slider slider;


        void Awake()
        {
            source = SteamVRRight.GetComponent<SteamVR_Behaviour_Pose>().inputSource;

        }


        // Start is called before the first frame update
        void Start()
        {
            Health = AppConfig.Inst.LifeNumber;
            maxLife = Health;
            Debug.Log("ma vie en tant que joueur vr est de : " + Health);
            updateGoFreeLookCameraRig();
            steamVRactivation();
            ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
            ps.enableEmission = !photonView.IsMine;
            net = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
            roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
            spawnSound.Play();
        }

        private void updateGoFreeLookCameraRig()
        {
            // Client execution ONLY LOCAL
            if (!photonView.IsMine) return;

            goFreeLookCameraRig = null;

            try
            {
                // Get the Camera to set as the follow camera
                goFreeLookCameraRig = transform.Find("/KeyboardPlayer/Camera").gameObject;
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
            SteamVRLeft.GetComponent<SteamVR_Behaviour_Pose>().enabled = photonView.IsMine;

            SteamVRRight.GetComponent<SteamVR_Behaviour_Pose>().enabled = photonView.IsMine;

            SteamVRCamera.GetComponent<Camera>().enabled = photonView.IsMine;
            
            if (!photonView.IsMine)
            {
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
            if (!photonView.IsMine) return;
            updateGoFreeLookCameraRig();
            if (photonView.IsMine && SteamVR_Actions._default.GrabPinch.GetStateDown(source) && Shot)
            {
                Debug.Log("je tire");
                photonView.RPC("ShootVirus", RpcTarget.AllViaServer, ChargeSpawner.position, speed * ChargeSpawner.forward);
                StartCoroutine(DelayShotVr());
            }
            PercentOfHp = (Health * 100) / maxLife;
           

        }

        [PunRPC]
        void ShootVirus(Vector3 position, Vector3 directionAndSpeed, PhotonMessageInfo info)
        {
            float lag = (float)(PhotonNetwork.Time - info.SentServerTime);
            Debug.LogFormat("PunRPC: ThrowVirus {0} -> {1} lag:{2}", position, directionAndSpeed, lag);

            // Create the Snowball from the Snowball Prefab
            GameObject chargeVirale = Instantiate(
                ChargeVirale,
                position + directionAndSpeed * Mathf.Clamp(lag, 0, 1.0f),
                Quaternion.identity);

            shootSound.Play();

            // Add velocity to the Snowball
            chargeVirale.GetComponent<Rigidbody>().velocity = directionAndSpeed;

            // Destroy the Snowball after 5 seconds
            Destroy(chargeVirale, 5.0f);
        }

        public void OnHitKMS(float damage)
        {
            if (photonView.IsMine)
            {
                Health = Health - damage;
                Debug.Log("VRUSER :  je suis touché il me reste : " + Health + " hp !!!");
            }

            
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(Health);
                Debug.Log(Health);
                if (Health <= 0)
                {
                    Debug.Log("Arghh je meurs !!!");

                    deathSound.Play();

                    roundManager.KillPlayer(gameObject);

                    if (net)
                    {
                        StartCoroutine(Delay(gameObject));
                      
                    }
                    else
                    {
                        Debug.Log("Network manager nout found disconnection");
                        PhotonNetwork.LeaveLobby();
                    }
                }
                else
                {
                    RedBar.rectTransform.sizeDelta = new Vector2(PercentOfHp, RedBar.rectTransform.sizeDelta.y);
                }
            }
            else
            {
                Health = (float)stream.ReceiveNext();
                Debug.Log(Health + "iuiseghfiusgiegh");
                if (Health <= 0)
                {
                    Debug.Log("je détruit l'autre !!!");
                    roundManager.KillPlayer(gameObject);
                    Destroy(gameObject);

                }
            }
        }

      

        IEnumerator DelayShotVr()
        {
            Shot = false;
            yield return new WaitForSeconds(AppConfig.Inst.DelayShot);
            Shot = true;
        }

        IEnumerator Delay(GameObject gameObject)
        {
            yield return new WaitForSeconds(0f);
            Debug.Log("je me détrui");
            Destroy(gameObject);
            net.respawn();
        }

    }
}

