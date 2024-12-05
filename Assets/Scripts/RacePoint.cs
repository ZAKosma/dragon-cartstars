using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Race))]
public class RacePoint : MonoBehaviour, IBuilding
{
    private Race race;

    public GameObject racePanel;
    public TMP_Text countdownText; // Text for countdown
    public GameObject player;

    private void Start()
    {
        race = GetComponent<Race>();
        racePanel.SetActive(false);
        countdownText.gameObject.SetActive(false);
    }

    public void TriggerBuilding()
    {
        Debug.Log("Triggered race building.");
        racePanel.SetActive(!racePanel.activeSelf);
        SetupRacePanel();
    }

    private void SetupRacePanel()
    {
        Button startButton = racePanel.transform.GetChild(0).GetComponent<Button>();
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(StartRace);

        TMP_Text raceNameText = racePanel.transform.GetChild(1).GetComponent<TMP_Text>();
        raceNameText.text = race.raceName;

        TMP_Text raceTypeText = racePanel.transform.GetChild(2).GetComponent<TMP_Text>();
        raceTypeText.text = race.raceType.ToString();

        TMP_Text rewardText = racePanel.transform.GetChild(3).GetComponent<TMP_Text>();
        rewardText.text = "Reward: " + race.raceReward;
    }

    public void StartRace()
    {
        Debug.Log("Starting race");
        GameManager.instance.StartRace();
        
        racePanel.SetActive(false);
        MovePlayerToStart();
        StartCoroutine(StartCountdown());
    }

    private void MovePlayerToStart()
    {
        if (race.raceType == RaceType.Circuit || race.raceType == RaceType.Sprint || race.raceType == RaceType.Drag)
        {
            player.transform.position = race.startingPoint.transform.position;
            player.transform.rotation = race.startingPoint.transform.rotation;
        }
    }

    private IEnumerator StartCountdown()
    {
        CheckpointManager.instance.InitializeCheckpoints();

        countdownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);

        CheckpointManager.instance.currentRace = race;
    }
}
