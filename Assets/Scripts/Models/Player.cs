using UnityEngine;
using System.Collections;
using SQLite4Unity3d;

public class Player
{

    private string name;
    private int location;
    private int health;
    private int wealth;
    private string password;
    // what about inventory?

        public int PlayerID { get; set; }
    public string Name { get => name; set => name = value; }
    public int LocationId { get => location; set => location = value; }
    //public int LocationId { get => location; set => location = value; }
    public int Health { get => health; set => health = value; }
    public int Wealth { get => wealth; set => wealth = value; }
    public string Password { get => password; set => password = value; }
}
