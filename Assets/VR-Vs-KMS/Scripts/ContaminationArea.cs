using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WS3;

namespace vr_vs_kms
{
    public class ContaminationArea : MonoBehaviour
    {

        [System.Serializable]
        public struct BelongToProperties
        {
            public Color mainColor;
            public Color secondColor;

        }

        public BelongToProperties nobody;
        public BelongToProperties virus;
        public BelongToProperties scientist;

        private float faerieSpeed;
        public float cullRadius = 5f;

        private float radius = 1f;
        private ParticleSystem pSystem;
        private WindZone windZone;
        private int remainingGrenades;
        public float inTimer = 0f;
        private CullingGroup cullGroup;

        public AudioSource soundCapturing;
        public AudioSource soundCaptured;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private float seizingMax;
        public float seizingSpeed;
        private float seizingCurrent;
        public string capturedBy = "None";
        public bool VRCapturing = false;
        public bool KMSCapturing = false;

        public List<GameObject> playersOnPoint;

        void Start()
        {
            seizingSpeed = AppConfig.Inst.TimeToAreaContaminator;

            populateParticleSystemCache();

            setupCullingGroup();

            BelongsToNobody();
        }

        private void populateParticleSystemCache()
        {
            pSystem = this.GetComponentInChildren<ParticleSystem>();
        }


        /// <summary>
        /// This manage visibility of particle for the camera to optimize the rendering.
        /// </summary>
        private void setupCullingGroup()
        {
            cullGroup = new CullingGroup();
            cullGroup.targetCamera = Camera.main;
            cullGroup.SetBoundingSpheres(new BoundingSphere[] { new BoundingSphere(transform.position, cullRadius) });
            cullGroup.SetBoundingSphereCount(1);
            cullGroup.onStateChanged += OnStateChanged;
        }

        void OnStateChanged(CullingGroupEvent cullEvent)
        {
            if (cullEvent.isVisible)
            {
                pSystem.Play(true);
            }
            else
            {
                pSystem.Pause();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.tag == "KeyboardPlayer")
            {
                KMSCapturing = false;
                other.gameObject.GetComponent<Players>().slider.gameObject.SetActive(false);

                for (int i = 0; i < playersOnPoint.Count; i++)
                {
                    if (playersOnPoint[i].GetInstanceID() == other.GetInstanceID())
                    {
                        playersOnPoint.RemoveAt(i);
                    }
                }
            }
            if (other.tag == "VRPlayer")
            {
                VRCapturing = false;
                other.gameObject.GetComponentInParent<VR_CameraRigMultiUser>().slider.gameObject.SetActive(false);

                for (int i = 0; i < playersOnPoint.Count; i++)
                {
                    if (playersOnPoint[i].GetInstanceID() == other.GetInstanceID())
                    {
                        playersOnPoint.RemoveAt(i);
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "KeyboardPlayer")
            {
                if (capturedBy == "VRPlayer")
                {
                    other.gameObject.GetComponent<Players>().slider.value = 0;
                    seizingCurrent = 0;
                }
                KMSCapturing = true;
                playersOnPoint.Add(other.gameObject);
                other.gameObject.GetComponent<Players>().slider.gameObject.SetActive(true);
                Debug.Log("un Antivirus rentre dans la zone");

                
            }
            if (other.tag == "VRPlayer")
            {
                if (capturedBy == "KeyboardPlayer")
                {
                    other.gameObject.GetComponentInParent<VR_CameraRigMultiUser>().slider.value = 0;
                    seizingCurrent = 0;
                }
                VRCapturing = true;
                playersOnPoint.Add(other.gameObject);
                Debug.Log(other.tag + " " + other.name);
                other.gameObject.GetComponentInParent<VR_CameraRigMultiUser>().slider.gameObject.SetActive(true);
                Debug.Log("un virus rentre dans la zone");

                
            }
        }

        void Update()
        {

            if (KMSCapturing == false && VRCapturing == false && capturedBy == "None")
            {
                BelongsToNobody();
                seizingCurrent = 0;
                sprite.color = nobody.secondColor;
            }
            else if (KMSCapturing && VRCapturing)
            {
                BelongsToNobody();
                sprite.color = nobody.secondColor;
            }
            else if (VRCapturing)
            {
                BelongsToVirus();
                if (seizingCurrent < seizingMax)
                {
                    seizingCurrent = seizingCurrent + seizingSpeed * Time.deltaTime;
                    if (!soundCapturing.isPlaying) soundCapturing.Play();
                }
                foreach (GameObject player in playersOnPoint)
                {
                    if (player.tag == "VRPlayer")
                    {
                        player.GetComponentInParent<VR_CameraRigMultiUser>().slider.gameObject.SetActive(true);

                        player.GetComponentInParent<VR_CameraRigMultiUser>().slider.value = seizingCurrent / seizingMax;

                        player.GetComponentInParent<VR_CameraRigMultiUser>().slider.GetComponentsInChildren<Image>()[1].color = virus.secondColor;

                    }
                }
                if (seizingCurrent > seizingMax)
                {
                    if (capturedBy != "VRGameObject") soundCaptured.Play();
                    capturedBy = "VRGameObject";
                    sprite.color = virus.secondColor;
                }
            }
            else if (KMSCapturing)
            {
                BelongsToScientists();
                if (seizingCurrent < seizingMax)
                {
                    seizingCurrent = seizingCurrent + seizingSpeed * Time.deltaTime;
                    if (!soundCapturing.isPlaying) soundCapturing.Play();
                }
                foreach (GameObject player in playersOnPoint)
                {
                    if (player.tag == "KeyboardPlayer")
                    {
                        player.GetComponent<Players>().slider.gameObject.SetActive(true);

                        player.GetComponent<Players>().slider.value = seizingCurrent / seizingMax;

                        player.GetComponent<Players>().slider.GetComponentsInChildren<Image>()[1].color = scientist.secondColor;

                    }
                }
                if (seizingCurrent > seizingMax)
                {
                    if (capturedBy != "KeyboardPlayer") soundCaptured.Play();
                    capturedBy = "KeyboardPlayer";
                    sprite.color = scientist.secondColor;
                }

            }

        }

        private void ColorParticle(ParticleSystem pSys, Color mainColor, Color accentColor)
        {

            var system = pSys.main;
            system.startColor = mainColor;
        }

        public void BelongsToNobody()
        {
            ColorParticle(pSystem, nobody.mainColor, nobody.secondColor);
        }

        public void BelongsToVirus()
        {
            ColorParticle(pSystem, virus.mainColor, virus.secondColor);
        }

        public void BelongsToScientists()
        {
            ColorParticle(pSystem, scientist.mainColor, scientist.secondColor);
        }

        void OnDestroy()
        {
            if (cullGroup != null)
                cullGroup.Dispose();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, cullRadius);
        }
    }
}