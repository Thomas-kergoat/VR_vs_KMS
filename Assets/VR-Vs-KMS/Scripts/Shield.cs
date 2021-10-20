using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [SerializeField] private float maxShieldLife = 5;

    [SerializeField] private float currentShieldLife = 5;

    private float PercentOfShield;

    public Image blueBar;

    // Start is called before the first frame update
    void Start()
    {
        transform.tag = "Shield";
    }

    // Update is called once per frame
    void Update()
    {
        PercentOfShield = currentShieldLife / maxShieldLife * 50;

        if (currentShieldLife <= 0)
        {
            Destroy(gameObject);
            blueBar.gameObject.SetActive(false);
        }
        else
        {
            blueBar.rectTransform.sizeDelta = new Vector2(PercentOfShield, blueBar.rectTransform.sizeDelta.y);
        }

    }
    public void OnHitShield(float damage)
    {
        currentShieldLife = currentShieldLife - damage;

        transform.localScale += new Vector3(0, -0.1f, -0.1f);
    }
}
