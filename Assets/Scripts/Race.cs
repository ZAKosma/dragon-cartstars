using UnityEngine;

public enum RaceType
{
    Circuit,
    Sprint,
    Drag
}

public class Race : MonoBehaviour
{
    public RaceType raceType;
    public string raceName;

    public GameObject[] checkPoints;
    public GameObject startingPoint;

    public int raceReward;

    public int totalLaps = 1;

    void Start()
    {
        foreach (var c in checkPoints)
        {
            c.SetActive(false);
        }
    }
}