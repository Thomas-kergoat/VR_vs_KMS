using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



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

        private Slider slider;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private float seizingMax;
        [SerializeField] private float seizingSpeed;
        private float seizingCurrent;
        private string capturedBy;

        void Start()
        {
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
            if (seizingCurrent < seizingMax)
            {
                seizingCurrent = 0;
                sprite.color = nobody.secondColor;
                BelongsToNobody();
            }
            if (slider != null) slider.gameObject.SetActive(false);

        }

        private void OnTriggerStay(Collider other)
        {
            slider = other.gameObject.GetComponentInChildren<Slider>(true);
            Debug.Log(slider);
            if (other.tag == "KeyboardPlayer")
            {
                if (capturedBy == "Virus")
                {
                    capturedBy = "";
                    seizingCurrent = 0;
                }
                slider.gameObject.SetActive(true);
                BelongsToScientists();
                if(seizingCurrent < seizingMax)
                {
                    seizingCurrent = seizingCurrent + seizingSpeed * Time.deltaTime;

                    slider.value = seizingCurrent / seizingMax;
                }
                else
                {
                    capturedBy = "Scientists";
                    sprite.color = scientist.secondColor;
                }
            }
            else if (other.tag == "VRPlayer")
            {
                if (capturedBy == "Scientists")
                {
                    capturedBy = "";
                    seizingCurrent = 0;
                }
                slider.gameObject.SetActive(true);
                BelongsToVirus();
                if (seizingCurrent < seizingMax)
                {
                    seizingCurrent = seizingCurrent + seizingSpeed * Time.deltaTime;

                    slider.value = seizingCurrent / seizingMax;
                }
                else
                {
                    capturedBy = "Virus";
                    sprite.color = virus.secondColor;
                }
            }
        }

        void Update()
        {
            
        }

        private void ColorParticle(ParticleSystem pSys, Color mainColor, Color accentColor)
        {
            
            var system = pSys.main;
            system.startColor = mainColor;
            if (slider != null) slider.GetComponentsInChildren<Image>()[1].color = accentColor;
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