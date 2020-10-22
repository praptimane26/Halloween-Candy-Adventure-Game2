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

    //public static Location currentLocale;
    //public static Location eastLocation;
    //public static Player currentPlayer;
    //public static DataService ds = new DataService("HalloweenCandy.db");

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

            Location forest, castle;
            currentLocale = GameModel.ds.storeNewLocation("Forest", " Run!! ");

            forest = currentLocale;

            forest.addLocation("North", "Castle", "Crocodiles");

            castle = forest.getLocation("North");
            castle.addLocation("South", forest);


            startLocation = currentLocale; // this might be redundant
        }
        else
            currentLocale = GameModel.ds.GetFirstLocation();

    }
    //{
    //    Location hospitalHallway, mirrorRoom;
    //    currentLocale = new Location
    //    {
    //        Name = "Hospital Hallway",
    //        Story = " It is a dark room, you hear something coming towards you\n You need to get out of this room before that thing comes any closer\n\n Hint : 'go North' and maybe you will survive!"
    //    };
    //    hospitalHallway = currentLocale;

    //    hospitalHallway.addLocation("North", "MirrorRoom", "You're surrounded by mirrors, but there is a way to get out only if you figure out which mirror to look behind \n \n Hint:'go south'");


    //    mirrorRoom = hospitalHallway.getLocation("North");
    //    mirrorRoom.addLocation("South", hospitalHallway);

    //    Location vortexRoom;
    //    eastLocation = new Location
    //    {
    //        Name = "Vortex",
    //        Story = "hello welcome to the vortex room"
    //    };

    //    vortexRoom = eastLocation;

    //    vortexRoom.addLocation("East", "VortexRoom", "this is the vortext room and place you don't want to be in");

    //    vortexRoom = mirrorRoom.getLocation("South");
    //    vortexRoom.addLocation("East", mirrorRoom);

    //}

    public static string DisplayStory()
    {
        return GameModel.currentLocale.Name + "\n " + GameModel.currentLocale.Story;
    }

}

