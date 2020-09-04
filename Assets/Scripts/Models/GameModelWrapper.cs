using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModelWrapper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameModel.Name = "Halloween Candy";
        if (GameModel.currentLocale == null)
            GameModel.MakeGame();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
