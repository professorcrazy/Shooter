using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private CinemachineVirtualCamera aimVC;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;
    private void Start() {
        Instance = this;
    }

    public void SpawnPlayerCamSetup() {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        GameObject player = Instantiate(playerPrefab, spawnPoints[randomIndex].position, spawnPoints[randomIndex].rotation);
        aimVC.Follow = player.transform;
        virtualCamera.Follow = player.transform;
    }
}
