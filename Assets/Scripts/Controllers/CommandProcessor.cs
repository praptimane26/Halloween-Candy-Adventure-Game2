using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;


    public class CommandProcessor
    {
        public CommandProcessor()
        {
        }


        public String Parse(String pCmdStr)
        {
            String strResult = "Do not understand"; ;
            pCmdStr = pCmdStr.ToLower();
            String[] parts = pCmdStr.Split(' '); // tokenise the command
            Location nextLocale;

            if (parts.Length > 0)
            {// process the tokens
                switch (parts[0])
                {
                    case "pick":
                        if (parts[1] == "up")
                        {
                            Debug.Log("Got Pick up");
                            strResult = "Got Pick up";

                            if (parts.Length == 3)
                            {
                                String param = parts[2];
                            }// do pick up command
                             // GameModel.Pickup();
                        }
                        break;
                    case "go":
                        switch (parts[1])
                        {
                            case "north":
                                Debug.Log("Going North");
                                nextLocale = GameModel.currentLocale.getLocation("North");
                                if (nextLocale == null)
                                {
                                    strResult = "Sorry can't go North " + GameModel.currentLocale.Name + "  " + GameModel.currentLocale.Story;
                                }
                                else
                                {
                                    GameModel.currentLocale = nextLocale;
                                    GameModel.currentPlayer.LocationId = nextLocale.Id;
                                    strResult = GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                                }

                                break;
                            case "south":
                                nextLocale = GameModel.currentLocale.getLocation("South");
                                if (nextLocale == null)
                                {
                                    strResult = "Sorry can't go South " + GameModel.currentLocale.Name + "  " + GameModel.currentLocale.Story;
                                }
                                else
                                {
                                    GameModel.currentLocale = nextLocale;
                                    GameModel.ds.storePlayer(GameModel.currentPlayer);
                                    GameModel.currentPlayer.LocationId = nextLocale.Id;
                                    strResult = GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                                }
                                break;
                        case "east":
                            nextLocale = GameModel.currentLocale.getLocation("East");
                            if (nextLocale == null)
                            {
                                strResult = "Sorry can't go East" + GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                            }
                            else
                            {
                                GameModel.currentLocale = nextLocale;
                                GameModel.ds.storePlayer(GameModel.currentPlayer);
                                GameModel.currentPlayer.LocationId = nextLocale.Id;
                                strResult = GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                            }
                            break;

                        case "west":
                            nextLocale = GameModel.currentLocale.getLocation("West");
                            if (nextLocale == null)
                            {
                                strResult = "Sorry can't go West" + GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                            }
                            else
                            {
                                GameModel.currentLocale = nextLocale;
                                GameModel.ds.storePlayer(GameModel.currentPlayer);
                                GameModel.currentPlayer.LocationId = nextLocale.Id;
                                strResult = GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                            }
                            break;

                        case "right":
                            nextLocale = GameModel.currentLocale.getLocation("Right");
                            if (nextLocale == null)
                            {
                                strResult = "Sorry can't go Right" + GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                            }
                            else
                            {
                                GameModel.currentLocale = nextLocale;
                                GameModel.ds.storePlayer(GameModel.currentPlayer);
                                GameModel.currentPlayer.LocationId = nextLocale.Id;
                                strResult = GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                            }
                            break;
                        default:
                                Debug.Log(" do not know how to go there");
                                strResult = "Do not know how to go there";
                                break;
                        }// end switch
                        break;



                    default:
                        Debug.Log("Do not understand");
                        strResult = "Do not understand";
                        break;

                }// end switch
            }
            else
            {
                Debug.Log("Do not understand");
                strResult = "Do not understand";
            }

            return strResult;

        }// Parse
    }


