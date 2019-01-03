using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Spawning : LevelSpawnParent {

    public int randomMaterial;
    public int randomSpawn;


    //Materials for leve l
    public List<Material> Materials;

    //GameObjects/Prefabs for level 1
    public GameObject Shopper; //Spawns, Enters Shop, Picks up N Items, Pays for Items, Leaves, Despawns
    //public int ShopperSpawnChance; // 60

    public GameObject Thief; // Spawns, Enters Shop, Browses for a bit, Grabs Item, Runs out of store, Despawns
    //public int ThiefSpawnChance; // 40
    public GameObject Carrier;
    //public int 
    public GameObject Muncher;

    //public Dictionary<GameObject, int> LevelEntityList;

	// Use this for initialization
	public override void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //public override GameObject RandomSpawn()
    //{
    //    randomSpawn = RandomSpawnNo();
    //    if (randomSpawn > ShopperSpawnChance ) return Thief;
    //    else return Shopper;
    //}
    //public override int RandomSpawnNo()
    //{
    //    int RandomInt = Random.Range(0, ShopperSpawnChance + ThiefSpawnChance);
    //    return RandomInt;
    //}

    public override GameObject RandomSpawn()
    {
        Dictionary<GameObject, int> map = getEntityMap();
        //Debug.Log("SPAWNING RANDOM -" + map);
        if (map == null) Debug.Log("No Entity Map Found"); //do some "oh shit" debugging

        int count = getTotalSum(map);
        int rndNum = Random.Range(0, count);
        //Debug.Log("Spawning RNG = " + rndNum + " / " + count);

        // this is deffo fixed
        foreach(KeyValuePair<GameObject, int> pair in map)
        {
            //Debug.Log("Checking spawn for: " + pair.Key);
            //Debug.Log("Random No: " + rndNum + " - Pair Val: " + pair.Value + "     - Sum: " + (rndNum - pair.Value));
            rndNum -= pair.Value;//pair.val
            if (rndNum <= 0) return pair.Key;
        }

        Debug.Log("ERROR SELECTING RANDOM UNIT TO SPAWN");
        return null;
    }

    int getTotalSum(Dictionary<GameObject, int> map)
    {
        int count = 0;
        foreach(KeyValuePair<GameObject, int> pair in map)
        {
            count += pair.Value;
        }
        //Debug.Log(count);
        return count;
    }


    public override Material RandomMaterial()
    {
        randomMaterial = RandomMaterialNo();
        Material RandomMaterialMat = Materials[randomMaterial];
        return RandomMaterialMat;
    }

    public override int RandomMaterialNo()
    {
        int randomInt = Random.Range(0, Materials.Count);
        return randomInt;
    }

    public override Dictionary<GameObject, int> getEntityMap()
    {
        Dictionary<GameObject, int> map = new Dictionary<GameObject, int>();
        map.Add(Shopper, 50);
        map.Add(Thief, 30);
        map.Add(Carrier, 10);
        map.Add(Muncher, 10);
        return map;
    }

}
