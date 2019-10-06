using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadManager : MonoBehaviour
{
    static List<GameObject> doNotDestroyList = new List<GameObject>();

    public static void AddToDoNotDestroyList(GameObject go)
    {
        DontDestroyOnLoad(go);
        doNotDestroyList.Add(go);
    }
    public static void DestroyAllDoNotDestroyItems()
    {
        foreach (GameObject go in doNotDestroyList)
        {
            Destroy(go);
        }
    }
}
