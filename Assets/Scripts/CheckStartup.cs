using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckStartup : MonoBehaviour
{
    public static bool isInitailized = false;

    public void Awake()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            isInitailized = true;
        }
        if(!isInitailized)
        {
            SceneManager.LoadScene("MainMenu");
        }


    }

}
