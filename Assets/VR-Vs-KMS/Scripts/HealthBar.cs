using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public  float curHP= 100.0f;
    public  float maxHP= 100.0f;

    public float HpBarLenght;   
    public float HpBarLenght1;
    public float PercentOfHp;
    public Image HpBarTexture;
    public Image HpBarTexture2;


    // Start is called before the first frame update
    void Start()
    {
        HpBarTexture2.rectTransform.sizeDelta = new Vector2(100, 18f);
        HpBarTexture.rectTransform.sizeDelta = new Vector2(1, 0.2f);
    

    }

    // Update is called once per frame
    void Update()
    {
        PercentOfHp = curHP / maxHP;
        HpBarLenght = PercentOfHp * 1f;
        HpBarLenght1 = PercentOfHp * 100f;
        if ((Input.GetKeyDown("h")) &&(curHP>0) )
        {
            curHP -= 10;
            HpBarTexture.rectTransform.sizeDelta = new Vector2(HpBarLenght,0.2f);
            HpBarTexture2.rectTransform.sizeDelta = new Vector2(HpBarLenght1,18f);
        }
        
    }

}
