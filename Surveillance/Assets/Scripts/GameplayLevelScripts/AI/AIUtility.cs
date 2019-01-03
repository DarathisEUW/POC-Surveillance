using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AIUtility
{
    /// <summary>
    /// Returns closest transform found to position from a gameobject search using tag inputted
    /// </summary>
    /// <param name="position"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static Transform getClosest(Vector3 position, string tag)
    {
        GameObject[] found = findObjects(tag);

        Transform closest = null;
        float closestDistance = 9999;

        if((position != null) && (found.Length > 0)) {
            foreach(GameObject obj in found) {
                float Distance = Vector3.Distance(position, obj.transform.position);

                if(Distance < closestDistance) {
                    closestDistance = Distance;
                    closest = obj.transform;
                }
            }
            return closest;
        }
        return null;
    }
    /// <summary>
    /// Returns random GameObject found from search for tags
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static GameObject getRandom(string tag)
    {
        GameObject[] found = findObjects(tag);
        int rand = Random.Range(0, found.Length);
        //Debug.Log(rand);
        return found[rand];

    }
    /// <summary>
    /// searches for tags and returns array of Gameobjects
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    static GameObject[] findObjects(string tag)
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag(tag);
        return temp;
    }
}
