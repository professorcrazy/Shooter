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

    public Transform GetRandomSpawnPoint() {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }
}
