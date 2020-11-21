using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Accelerometer : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.acceleration.sqrMagnitude >= 5f)
        {
            SceneManager.LoadScene("LoginRegister");
        }
    }
}
