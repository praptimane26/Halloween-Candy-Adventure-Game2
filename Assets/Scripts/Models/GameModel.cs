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

    public static void RegisterPlayer(string pName, string pPassword)
    {

        GameModel.currentPlayer = GameModel.ds.storeNewPlayer(pName, pPassword, GameModel.currentLocale.Id, 100, 200);
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

            Location MirrorRoom , VortexRoom, SpikeRoom, HospitalHallway ;
            currentLocale = GameModel.ds.storeNewLocation("MirrorRoom ", " \n" + "Quick close your eyes and do not look at yourself in any of the mirrors, you need to get out of here  hint 'go north'");

            MirrorRoom = currentLocale;

            MirrorRoom.addLocation("North", "Vortex Room","\n" + "Ah welcome! You have found your way into the North side of the house, the Vortex Room, maintain your balance and try getting to the end of this room hint 'go east or go south' it's your call really!");

            VortexRoom = MirrorRoom.getLocation("North");
            VortexRoom.addLocation("South", MirrorRoom);

            
            VortexRoom.addLocation("East", "Spike Room","\n" + "Are you afraid of getting hurt? Well then you shouldn't have walked in here, is there really a safe place in this house? hint 'go west'");
            SpikeRoom = VortexRoom.getLocation("East");
            SpikeRoom.addLocation("North", VortexRoom);

            SpikeRoom.addLocation("West", "Hospital Hallway","\n" + "The patients are sleeping try not to wake them up while you're here, they won't be happy about it!");
            HospitalHallway = SpikeRoom.getLocation("West");
            HospitalHallway.addLocation("East", SpikeRoom);


            
            


            //startLocation = currentLocale; // this might be redundant
        }
        else
            currentLocale = GameModel.ds.GetFirstLocation();

    }




}

