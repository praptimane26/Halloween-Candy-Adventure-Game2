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
    public static Location eastLocation;
    public static Player currentPlayer;

    public static void MakeGame()
    {
        Location hospitalHallway, mirrorRoom;
        currentLocale = new Location
        {
            Name = "Hospital Hallway",
            Story = " It is a dark room, you hear something coming towards you\n You need to get out of this room before that thing comes any closer\n\n Hint : 'go North' and maybe you will survive!"
        };
        hospitalHallway = currentLocale;

        hospitalHallway.addLocation("North", "MirrorRoom", "You're surrounded by mirrors, but there is a way to get out only if you figure out which mirror to look behind \n \n Hint:'go south'");


        mirrorRoom = hospitalHallway.getLocation("North");
        mirrorRoom.addLocation("South", hospitalHallway);

        Location vortexRoom;
        eastLocation = new Location
        {
            Name = "Vortex",
            Story = "hello welcome to the vortex room"
        };

        vortexRoom = eastLocation;

        vortexRoom.addLocation("East", "VortexRoom", "this is the vortext room and place you don't want to be in");

        vortexRoom = mirrorRoom.getLocation("South");
        vortexRoom.addLocation("East", mirrorRoom);

    }

    public static string DisplayStory()
    {
        return GameModel.currentLocale.Name + "\n " + GameModel.currentLocale.Story;
    }

}

