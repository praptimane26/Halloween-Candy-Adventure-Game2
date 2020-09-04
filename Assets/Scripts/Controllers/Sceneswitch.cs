using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneswitch : MonoBehaviour
{
    public void mapScreen()
    {

        SceneManager.LoadScene(1);
    }

    public void GamePlay()
    {

        SceneManager.LoadScene(0);
    }

    public void inventoryScreen()
    {

        SceneManager.LoadScene(2);
    }



}
