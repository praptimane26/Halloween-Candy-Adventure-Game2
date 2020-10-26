using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class TextInput : MonoBehaviour
{
    InputField input;
    InputField.SubmitEvent se;
    InputField.OnChangeEvent ce;
    public Text output;
    private string playerName = "";

    // Use this for initialization
    void Start()
    {
        playerName = GameModel.currentPlayer != null ? "Hello " + GameModel.currentPlayer.Name + "!\n " : "";
        //output.text = GameModel.DisplayStory();
        output.text = playerName + GameModel.currentLocale.Name + "" + GameModel.currentLocale.Story;

        input = this.GetComponent<InputField>();
        se = new InputField.SubmitEvent();
        se.AddListener(SubmitInput);
        input.onEndEdit = se;
    }


    private void SubmitInput(string arg0)
    {
        string currentText = output.text;

        CommandProcessor aCmd = new CommandProcessor();
        output.text = playerName + aCmd.Parse(arg0);

        input.text = "";
        input.ActivateInputField();



    }

    private void ChangeInput(string arg0)
    {
        Debug.Log(arg0);
    }
}
