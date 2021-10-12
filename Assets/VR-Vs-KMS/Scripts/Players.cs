﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Players : MonoBehaviour
{

    public float maxLife = 5;

    public float currentLife = 5;

    private float PercentOfHp;
    public Image RedBar;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PercentOfHp = currentLife / maxLife * 100;

        if (currentLife <= 0)
        {
            Destroy(gameObject);
        } else
        {
            RedBar.rectTransform.sizeDelta = new Vector2(PercentOfHp, RedBar.rectTransform.sizeDelta.y);
        }

    }

    public void OnHit(float damage)
    {
        currentLife = currentLife - damage;
    }
}
