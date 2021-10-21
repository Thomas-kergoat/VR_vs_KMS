using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonLevelInstantiate : MonoBehaviour
{
    public GameObject spawnPointPrefab;

    public GameObject contaminationAreaPrefab;

    public GameObject thowableObjectPrefab;

    private string jsonFile;

    void Start()
    {
        jsonFile = File.ReadAllText(Application.streamingAssetsPath + "/level.json");

        if (jsonFile != null)
        {
            spawnPoint spawnPointInJSON = JsonUtility.FromJson<spawnPoint>(jsonFile);

            foreach (spawnPointSingle spawnPoint in spawnPointInJSON.SpawnPoint)
            {   

                Instantiate(spawnPointPrefab, new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z), Quaternion.Euler(90, 0, 0));
            }

            contaminationArea contaminationAreaInJSON = JsonUtility.FromJson<contaminationArea>(jsonFile);

            foreach (contaminationAreaSingle contaminationArea in contaminationAreaInJSON.ContaminationArea)
            {

                Instantiate(contaminationAreaPrefab, new Vector3(contaminationArea.x, contaminationArea.y, contaminationArea.z), Quaternion.Euler(0, 0, 0));
            }

            thowableObject thowableObjectInJSON = JsonUtility.FromJson<thowableObject>(jsonFile);

            foreach (thowableObjectSingle thowableObject in thowableObjectInJSON.ThowableObject)
            {

                Instantiate(thowableObjectPrefab, new Vector3(thowableObject.x, thowableObject.y, thowableObject.z), Quaternion.Euler(0, 0, 0));
            }
        }

        Destroy(gameObject);

    }

}


[System.Serializable]
public class spawnPoint
{
    //employees is case sensitive and must match the string "employees" in the JSON.
    public spawnPointSingle[] SpawnPoint;
}

[System.Serializable]
public class spawnPointSingle
{
    //these variables are case sensitive and must match the strings "firstName" and "lastName" in the JSON.
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class contaminationArea
{
    //employees is case sensitive and must match the string "employees" in the JSON.
    public contaminationAreaSingle[] ContaminationArea;
}

[System.Serializable]
public class contaminationAreaSingle
{
    //these variables are case sensitive and must match the strings "firstName" and "lastName" in the JSON.
    public float x;
    public float y;
    public float z;
}


[System.Serializable]
public class thowableObject
{
    //employees is case sensitive and must match the string "employees" in the JSON.
    public thowableObjectSingle[] ThowableObject;
}

[System.Serializable]
public class thowableObjectSingle
{
    //these variables are case sensitive and must match the strings "firstName" and "lastName" in the JSON.
    public float x;
    public float y;
    public float z;
}