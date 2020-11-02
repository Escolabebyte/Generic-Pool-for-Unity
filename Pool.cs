using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pool
{
    /// <summary>
    /// Pool of object instantiated
    /// </summary>
    private static Dictionary<object, List<object>> Pools = new Dictionary<object, List<object>>();

    /// <summary>
    ///  Pool of object instantiated temporary
    /// </summary>
    private static Dictionary<object, List<object>> TempPool = new Dictionary<object, List<object>>();

    /// <summary>
    /// Instantiate an object or reuse an object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="original">Original object</param>
    /// <param name="pos">Instantiate position</param>
    /// <param name="rot">Instantiate rotation</param>
    /// <param name="usePool">Add object in temporary pool and add in the pool of object later with AddRange request</param>
    /// <returns>Instantiated object</returns>
    public static T Instantiate<T>(T original, Vector3 pos, Quaternion rot, bool usePool = true) where T : UnityEngine.Object
    {
        if (!Pools.ContainsKey(original))
            Pools.Add(original, new List<object>());

        UnityEngine.Object temp = default;

        if (Pools[original].Count > 0 && usePool)
        {
            temp = (T)Pools[original][0];
            Pools[original].RemoveAt(0);
            ((MonoBehaviour)temp).gameObject.SetActive(true);
        } 
        else
        {
            temp = MonoBehaviour.Instantiate(original, pos, rot);
        }

        if (usePool)
        {
            if (!TempPool.ContainsKey(original))
                TempPool.Add(original, new List<object>());

            TempPool[original].Add(temp);
        }

        return temp as T;
    }

    /// <summary>
    /// Instantiate an object or reuse an object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="original">Original object</param>
    /// <param name="usePool">Add object in temporary pool and add in the pool of object later with AddRange request</param>
    /// <returns>Instantiated object</returns>
    public static T Instantiate<T>(T original, bool usePool = true) where T : UnityEngine.Object
    {
        return Instantiate(original, Vector3.zero, Quaternion.identity, usePool);
    }

    /// <summary>
    /// Desactive all objects in the epscific pool of object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="original">Original object</param>
    public static void DesactiveAll<T>(T original) where T : UnityEngine.Object
    {
        if (!Pools.ContainsKey(original))
            Pools.Add(original, new List<object>());
        else
        {
            foreach (var item in Pools[original])
            {
                MonoBehaviour t = (MonoBehaviour)item;
                t.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="original"></param>
    public static void AddRange<T>(T original) where T : UnityEngine.Object
    {
        if (TempPool.ContainsKey(original))
        {
            foreach (var item in TempPool[original])
            {
                if (!Pools[original].Contains(item))
                    Pools[original].Add(item);
            }

            TempPool[original].Clear();
        }
    }
}
