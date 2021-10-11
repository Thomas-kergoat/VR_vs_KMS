using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APICall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetText()
    {
        
        using (UnityWebRequest www = UnityWebRequest.Get(AppConfig.Inst.WebAPILink))
        {
            yield return www.SendWebRequest();
            

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                Debug.Log("J'ai call err");
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);
                Meteo.Inst.UpdateValuesFromJSON(www.downloadHandler.text);
                Debug.Log(Meteo.Inst.weather[0].main);
                Debug.Log("J'ai call");

            }
        }
    }

}

[System.Serializable]
public class Meteo
{
    public List<Weather> weather;

    private static Meteo inst;
    public static Meteo Inst
    {
        get
        {
            if (inst == null)
            {
                inst = new Meteo();
            }
            return inst;
        }
    }

    public void UpdateValuesFromJSON(string jsonString)
    {
        JsonUtility.FromJsonOverwrite(jsonString, Inst);
    }
}

[System.Serializable]
public class Weather
{
    public int id;
    public string main;
    public string description;
    public string icon;
}

