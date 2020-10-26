using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;

public class Location
{
    private string name;
    private string story;

    // what about location asessts??

    // No longer need this because it is implemented as ToFrom
    // private Dictionary<string, Location> Locations = new Dictionary<string, Location>();
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get => name; set => name = value; }
    public string Story { get => story; set => story = value; }


    public void addLocation(string pDirection, string pName, string pStory)
    {
        // Store location now creates and stores a location
        // in Sqlite.

        //Location newLocation = new Location
        //{
        //    Name = pName,
        //    Story = pStory
        //};

        Location newLocation = GameModel.ds.storeNewLocation(pName, pStory);


        addDirection(pDirection, newLocation);

    }

    public void addDirection(string pDirection, Location toLocation)
    {
        GameModel.ds.storeFromTo(Id, toLocation.Id, pDirection);
    }
    public void addLocation(string pDirection, Location pLocation)
    {
        addDirection(pDirection, pLocation);
    }

    public Location getLocation(string pDirection)
    {
        Location thisLocation = null;

        //if(! Locations.TryGetValue(pDirection, out thisLocation)) {
        //    Debug.Log(" Bad location");
        //}
        ToFrom tf = GameModel.ds.GetToFrom(Id, pDirection);
        if (tf != null)
            thisLocation = GameModel.ds.GetLocation(tf.ToID);

        return thisLocation;
    }
}
