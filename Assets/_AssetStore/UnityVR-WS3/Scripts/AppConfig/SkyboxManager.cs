using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public Material cloudySkyboxFilePath;
    public Material SunnySkyboxFilePath;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Meteo.Inst.weather[0].main);
        if (Meteo.Inst.weather[0].main == "Clouds")
        {
            RenderSettings.skybox = cloudySkyboxFilePath;
        }
        else if (Meteo.Inst.weather[0].main == "Clear")
        {
            RenderSettings.skybox = SunnySkyboxFilePath;
        }
       
    }
}
