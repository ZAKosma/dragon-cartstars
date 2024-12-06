using System;
using System.Collections;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public CameraController mainCamera;
    public GameObject GameUIRoot;
    public GameObject playerPrefab;
    public GameObject AIPrefab;
    
    public Transform playerSpawnPoint;
    public Transform aiSpawnPoint;

    private bool canStart = false;
    
    IEnumerator Start()
    {
        GameUIRoot.SetActive(false);
        
        //Instantiate AI
        var ai = Instantiate(AIPrefab, aiSpawnPoint.position, aiSpawnPoint.rotation);
        mainCamera.target = ai.transform;

        canStart = false;
        yield return new WaitForSeconds(3);
        canStart = true;
    }

    private void Update()
    {
        //If there is any update tell GameManager to start game
        if (Input.anyKeyDown && canStart)
        {
            StartGame();
            this.gameObject.SetActive(false);
        }
    }

    void StartGame()
    {
        GameUIRoot.SetActive(true);
        
        //Instantiate Player
        var player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        mainCamera.target = player.transform;
        
        //Instantiate another AI
        Instantiate(AIPrefab, aiSpawnPoint.position, aiSpawnPoint.rotation);
    }
}