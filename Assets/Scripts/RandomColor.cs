using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    public List<MeshRenderer> models;
    void Start()
    {
        foreach(MeshRenderer model in models)
        {
            Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
            model.material.color = newColor;
        }
    }


}
