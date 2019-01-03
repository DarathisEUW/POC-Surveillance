using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawnParent : MonoBehaviour {

    //public List<Material> Materials;
    //public List<GameObject> SpawningShoppers;

    public Dictionary<GameObject, int> LevelEntityList;

    //Use this for initialization
    public virtual void Start ()
    {
		
	}
	
	// Update is called once per frame
	//void Update () {}

    public virtual GameObject RandomSpawn()
    {
        return null;
    }
    public virtual Material RandomMaterial()
    {
        return null;
    }

    public virtual int RandomSpawnNo()
    {
        return 0;
    }
    public virtual int RandomMaterialNo()
    {
        return 0;
    }

    public virtual Dictionary<GameObject, int> getEntityMap()
    {
        return null;
    }
}
