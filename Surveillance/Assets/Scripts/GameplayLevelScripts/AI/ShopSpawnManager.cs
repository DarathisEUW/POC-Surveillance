using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpawnManager : MonoBehaviour {

    public float maxShoppers;
    public float shopperCount;

    public float shopperSpawnTime;
    public float shopperSpawnTimer;

    Transform ShoppersContainer;

    public LevelSpawnParent SpawningParent;

	// Use this for initialization
	void Start ()
    {
        ShoppersContainer = GameObject.Find("Entities").transform;

        SpawningParent = GameObject.Find("Entities").GetComponent<LevelSpawnParent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        shopperCount = ShoppersContainer.childCount;

        if (shopperCount < maxShoppers)
        {
            shopperSpawnTimer += Time.deltaTime;
            if (shopperSpawnTimer >= shopperSpawnTime)
            {
                //Debug.Log("Gotta Spawn more Shoppers!");

                //load level specific materials and entities to spawn
                Material randomMaterial = SpawningParent.RandomMaterial(); // material of spawned ai entity
                GameObject randomSpawn = SpawningParent.RandomSpawn();

                //Spawn in Entity
                if (randomSpawn != null)
                {
                    GameObject Shopper = Instantiate(randomSpawn, ShoppersContainer);
                    Shopper.GetComponent<MeshRenderer>().material = randomMaterial;
                }
                shopperSpawnTimer = 0;
            }
        }
	}
}
