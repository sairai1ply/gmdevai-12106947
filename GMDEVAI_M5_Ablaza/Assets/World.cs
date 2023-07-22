using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class World : MonoBehaviour
{
    private static readonly World instance = new World();

    private static GameObject[] _hidingSpots;

    static World()
    {
        _hidingSpots = GameObject.FindGameObjectsWithTag("Hide");
    }

    private World() { }

    public static World Instance 
    { 
        get { return instance; } 
    }

    public GameObject[] GetHidingSpots() 
    {
        return _hidingSpots;
    }
}
