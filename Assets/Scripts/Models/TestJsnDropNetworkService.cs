using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestJsnDropNetworkService : MonoBehaviour
{
    public void jsnReceiverDel(JsnReceiver pReceived)
    {
        Debug.Log(pReceived.JsnMsg + " ..." + pReceived.Msg);
        // To do: parse and produce an appropriate response
    }

    public void jsnListReceiverDel(List<Player> pReceivedList)
    {
        Debug.Log("Received items " + pReceivedList.Count());
        foreach (Player lcReceived in pReceivedList)
        {
            Debug.Log("Received {" + lcReceived.Name + "," + lcReceived.Password + "," + lcReceived.HighScore.ToString()+"}");
        }// for

        // To do: produce an appropriate response
    }
    // Start is called before the first frame update
    void Start()
    {
        #region Test jsn drop
        JSONDropService jsDrop = new JSONDropService { Token = "6f26d3ba-60ae-484b-ac42-613fcf21fa19" };

        // Create table person
        jsDrop.Create<Player, JsnReceiver>(new Player
        {
            Name = "ododododododod",
            Password = "*********",
            HighScore = 0,
            Score = 0
        }, jsnReceiverDel);

        
    
        /*
        // Store people records
        jsDrop.Store<tblPerson,JsnReceiver> (new List<tblPerson>
        {
            new tblPerson{PersonID = "Jack",HighScore = 100,Password ="test"},
            new tblPerson{PersonID = "Jonny",HighScore = 200,Password ="test1"},
            new tblPerson{ PersonID = "Jill",HighScore = 300,Password ="test2"}
         }, jsnReceiverDel);
        
        // Retreive all people records
        jsDrop.All<tblPerson, JsnReceiver>(jsnListReceiverDel, jsnReceiverDel);
        
        jsDrop.Select<tblPerson,JsnReceiver>("HighScore > 200",jsnListReceiverDel, jsnReceiverDel);
        
        jsDrop.Delete<tblPerson, JsnReceiver>("PersonID = 'Jonny'", jsnReceiverDel);
        */
        jsDrop.Drop<Player, JsnReceiver>(jsnReceiverDel);
        
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

