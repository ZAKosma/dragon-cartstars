using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject terrainMap;
    
    // Delegate event for start race
    public delegate void StartRaceDelegate();
    public StartRaceDelegate onStartRace;
    
    public delegate void EndRaceDelegate();
    public EndRaceDelegate onEndRace;

    private void Awake()
    {
        //Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartRace()
    {
        onStartRace?.Invoke();
    }
    
    public void EndRace()
    {
        onEndRace?.Invoke();
    }
}