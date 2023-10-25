using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapLoader: MonoBehaviour
{
    public Transform map;
    private GameObject prefab;
    public List<ObjectCorrespondance> correspondance;


    void Start()
    {
        //LoadMap();
    }


    public void LoadMap()
    {
        string filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/map.json";

        Debug.Log("test");

        if(File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            Objectwrapper wrapper = JsonUtility.FromJson<Objectwrapper>(json);
            Debug.Log(JsonUtility.FromJson<Objectwrapper>(json));

            foreach(MapObject.MapObjects mapObject in wrapper.data)
            {
                foreach(ObjectCorrespondance pair in correspondance)
                {
                    if(pair.type == mapObject.type)
                    {
                        prefab = pair.prefab;
                    }
                    
                }
                Instantiate(prefab, mapObject.position / 2, mapObject.rotation, map);
                Debug.Log("Generating object");
            }
        }
    }
}

[Serializable]
public class Objectwrapper
{
    public List<MapObject.MapObjects> data;
}

[Serializable]
public class ObjectCorrespondance
{
    public MapObject.types type;
    public GameObject prefab;
}

