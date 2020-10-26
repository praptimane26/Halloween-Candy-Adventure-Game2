using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectGameModel : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        if (GameModel.started == false)
        GameModel.Name = "Halloween Candy";
        GameModel.SetupGame();
        GameModel.MakeGame();
        GameModel.started = true;
    }

}
