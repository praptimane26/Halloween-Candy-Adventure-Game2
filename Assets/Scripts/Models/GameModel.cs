using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;


using System.Text;
using UnityEngine.UI;

// Is this a factory?

public static class GameModel
{
    public static JSONDropService jsDrop = new JSONDropService { Token = "6f26d3ba-60ae-484b-ac42-613fcf21fa19" };
    static String _name;
    public static bool started = false;

    public static string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }

    }

    //Network Connection

    
    public static Location currentLocale;

    public static Player currentPlayer = null;
    public static Location startLocation;
    public static DataService ds = new DataService("HalloweenCandy.db");


    public enum PasswdMode
    {
        NeedName,
        NeedPassword,
        OK,
        AllBad
    }

    public static PasswdMode CheckPassword(string pName, string pPassword)
    {
        PasswdMode result = GameModel.PasswdMode.AllBad;

        Player aPlayer = ds.getPlayer(pName);
        if (aPlayer != null)
        {
            if (aPlayer.Password == pPassword)
            {
                result = GameModel.PasswdMode.OK;
                GameModel.currentPlayer = aPlayer; // << WATCHOUT THIS IS A SIDE EFFECT
                GameModel.currentLocale = GameModel.ds.GetPlayerLocation(GameModel.currentPlayer);
            }
            else
            {
                result = GameModel.PasswdMode.NeedPassword;
            }
        }
        else
            result = GameModel.PasswdMode.NeedName;

        return result;
    }

    public static void jsnReceiverDel(JsnReceiver pReceived)
    {
        Debug.Log(pReceived.JsnMsg + " ..." + pReceived.Msg);
        // To do: parse and produce an appropriate response
    }
    public static void RegisterPlayer(string pName, string pPassword)
    {
        List<Player> PlayerList = new List<Player>();
        PlayerList.Add(currentPlayer);
        GameModel.currentPlayer = GameModel.ds.storeNewPlayer(pName, pPassword, GameModel.currentLocale.Id, 100, 200);

        GameModel.jsDrop.Store<Player, JsnReceiver>(PlayerList, jsnReceiverDel);
        //    (new List<tblPerson>
        //{
        //    new tblPerson{ Name = GameModel.currentPlayer.Name, Password = GameModel.currentPlayer.Password, LocationId = GameModel.currentPlayer.LocationId, HighScore = GameModel.currentPlayer.HighScore, Health = GameModel.currentPlayer.Health, Wealth = GameModel.currentPlayer.Wealth}
        //}, jsnReceiverDel);
    }

    public static void SetupGame()
    {
        ds.CreateDB();

    }

    public static void MakeGame()
    {
        // Only make a  game if we dont have locations
        if (!GameModel.ds.haveLocations())
        {

            Location MirrorRoom , VortexRoom, SpikeRoom, HospitalHallway, GhostAffair ;
            currentLocale = GameModel.ds.storeNewLocation("MirrorRoom ", " \n" + "Quick close your eyes and do not look at yourself in any of the mirrors, you need to get out of here" + "\n" + "hint 'go north'");

            MirrorRoom = currentLocale;

            MirrorRoom.addLocation("North", "Vortex Room","\n" + "Ah welcome! You have found your way into the North side of the house, the Vortex Room, maintain your balance and try getting to the end of this room" + "\n" + " hint : go east / go south!");

            VortexRoom = MirrorRoom.getLocation("North");
            VortexRoom.addLocation("South", MirrorRoom);

            
            VortexRoom.addLocation("East", "Spike Room","\n" + "Are you afraid of getting hurt? Well then you shouldn't have walked in here, is there really a safe place in this house?" + "\n" + " hint : go west / go north!");
            SpikeRoom = VortexRoom.getLocation("East");
            SpikeRoom.addLocation("North", VortexRoom);

            SpikeRoom.addLocation("West", "Hospital Hallway","\n" + "The patients are sleeping try not to wake them up while you're here, they won't be happy about it!" + "\n" + " hint : go right / go east!");
            HospitalHallway = SpikeRoom.getLocation("West");
            HospitalHallway.addLocation("East", SpikeRoom);

            HospitalHallway.addLocation("Right", "Ghost Affair", "\n" + "Are you here to romance the ghost? Be careful she may not be in a good mood, how about you avoid her tonight!" + "\n" + " hint : go west" );
            GhostAffair = HospitalHallway.getLocation("Right");
            GhostAffair.addLocation("West", HospitalHallway);

            //startLocation = currentLocale; // this might be redundant
        }
        else
            currentLocale = GameModel.ds.GetFirstLocation();

    }




}

