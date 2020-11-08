using SQLite4Unity3d;
using UnityEngine;
using System.Linq;
// DataService is a bridge to SQlite 
// =================================
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService  {

	private SQLiteConnection _connection;
    public SQLiteConnection Connection { get { return _connection; } }

    public DataService(string DatabaseName){

#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
            _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);     

	}


    public void SaveLocations(List<Location> Locations)
    {
        _connection.DropTable<Location>();
        _connection.CreateTable<Location>();
        _connection.InsertAll(Locations);
    }

    public List<Location> LoadLocations()
    {
        return _connection.Table<Location>().ToList<Location>();
    }

    public bool HaveLocations()
    {
        try {
            return LoadLocations().Count > 0;
        }
        catch
        {
            return false;
        }

    }


    /* ====== */
	public void CreateDB(){
        // remove these once testing is sorted
       // _connection.DropTable<Location>(); 
       // _connection.DropTable<ToFrom>();
       // _connection.DropTable<Player>();

        // creating the schema
        _connection.CreateTable<Location>();
        _connection.CreateTable<ToFrom>();
        _connection.CreateTable<Player>();

        GameModel.jsDrop.Create<Player, JsnReceiver>(new Player
        {
            Name = "ododododododod",
            Password = "*********",
            HighScore = 0
        },GameModel.jsnReceiverDel);


    }



    // Locations and their relationships 
    public IEnumerable<Location> GetLocations()
    {
        return _connection.Table<Location>();
    }

    public Location GetFirstLocation()
    {
        Location aLocation = _connection.Table<Location>().FirstOrDefault<Location>();
        return aLocation;
    }
    public bool haveLocations()
    {
        return _connection.Table<Location>().Count() > 0;
    }

    public ToFrom GetToFrom(int pFromID, string pDirection)
    {
        return _connection.Table<ToFrom>().Where(tf => tf.FromID == pFromID
                                                      && tf.Direction == pDirection).FirstOrDefault();      
    }

    public Location GetLocation(int pLocationID)
    {
        return _connection.Table<Location>().Where(l => l.Id == pLocationID).FirstOrDefault();
    }

    public Location GetPlayerLocation (Player aPlayer)
    {
        return GetLocation(aPlayer.LocationId);
    }

    public void PlayerLocation (Player player)
    {
        _connection.Update(player);
    }
    public Location storeNewLocation(string pName, string pStory)
    {
        Location newLocation = new Location
        {
            Name = pName,
            Story = pStory
        };
        _connection.Insert(newLocation); // Store the location 
        return newLocation;  // return the location
    }
    public Location storeLocation(Location pLocation)
    {
  
        _connection.InsertOrReplace(pLocation); // Store the location 
        return pLocation; 
    }


    public void storeFromTo(int pFromID, int pToID, string pDirection)
    {
        ToFrom f = new ToFrom
        {
            ToID = pToID,
            FromID = pFromID,
            Direction = pDirection
        };
        _connection.Insert(f);

    }


    // Player
    public Player storeNewPlayer(string pName,   string pPassword , 
                            int pLocationId,  int pHealth,
                            int pWealth)
    {
        Player player = new Player
        {
            Name = pName,
            Password = pPassword,
            LocationId = pLocationId,
            Health = pHealth,
            Wealth = pWealth

        };
        _connection.Insert(player);
        return player;
    }

    public void storePlayer (Player pPlayer)
    {
        _connection.InsertOrReplace(pPlayer);
    }

    public Player getPlayer(string pPlayerName)
    {
        return _connection.Table<Player>().Where(x => x.Name == pPlayerName).FirstOrDefault();
    }

    //   Example 
    // public Person GetJohnny(){
    //	return _connection.Table<Person>().Where(x => x.Name == "Johnny").FirstOrDefault();
    //}

    //public Person CreatePerson(){
    //	var p = new Person{
    //			Name = "Johnny",
    //			Surname = "Mnemonic",
    //			Age = 21
    //	};
    //	_connection.Insert (p);
    //	return p;
    //}

    //public IEnumerable<Person> GetPersons(){
    //	return _connection.Table<Person>();
    //}

    //public IEnumerable<Person> GetPersonsNamedRoberto(){
    //	return _connection.Table<Person>().Where(x => x.Name == "Roberto");
    //}

}
