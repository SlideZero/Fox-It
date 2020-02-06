using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Manager : MonoBehaviour
{
    public static string enemyTag = "Enemy";
    public static string particleTag = "SmokeParticle";
    public static string turretSmokeTag = "TurretSmoke";

    [Header("Manager Parameters")]
    [SerializeField] private float _spawnCooldown = 0;
    [Tooltip("Quitar")]
    [SerializeField] private bool _canSpawn = false;
    [Tooltip("Quitar")]
    [SerializeField] private int _actualSpawnPoint = 0;
    private float _timerCountdown = 0;
    [Tooltip("Quitar")]
    [SerializeField] private float _spawnRate = 0;
    [SerializeField] private int _enemyNumber = 0;
    [SerializeField] private int _waveNumber = 0;
    [Space]
    [Header("Spawn Points Parameters")]
    public List<SpawnPoints> _spawnPoints = new List<SpawnPoints>();
    

    private void Start()
    {
        _timerCountdown = _spawnCooldown;
        _spawnRate = _spawnPoints[_actualSpawnPoint].SpawnRate;
    }

    private void Update()
    {
        if (_timerCountdown > 0)
        {
            _timerCountdown -= Time.deltaTime;
        }

        if (_timerCountdown <= 0)
        {
            if (_waveNumber < _spawnPoints.Count)
            {
                if (_enemyNumber == _spawnPoints[_actualSpawnPoint].NumberOfEnemies)
                {
                    _timerCountdown = _spawnCooldown;
                    _actualSpawnPoint++;
                    _enemyNumber = 0;
                    _waveNumber++;
                }
                else
                {
                    SpawnEnemy();
                }
            }
        }
    }

    private void SpawnEnemy()
    {
        if (_enemyNumber < _spawnPoints[_actualSpawnPoint].NumberOfEnemies)
        {
            _spawnRate -= Time.deltaTime;

            for (int i = 0; i < _spawnPoints[_actualSpawnPoint].NumberOfEnemies; i++)
            {
                if (_spawnRate <= 0)
                {
                    GameObject enemy = ObjectPooler.SharedInstance.GetPooledObject(enemyTag);
                    if (enemy != null)
                    {
                        enemy.transform.position = _spawnPoints[_actualSpawnPoint].SpawnPoint.position;
                        enemy.transform.rotation = _spawnPoints[_actualSpawnPoint].SpawnPoint.rotation;
                        enemy.SetActive(true);
                    }
                    _spawnRate = _spawnPoints[_actualSpawnPoint].SpawnRate;
                    _enemyNumber++;
                }
            }
        }
    }
    
}

[System.Serializable]
public class SpawnPoints
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _spawnRate = 0;
    [SerializeField] private int _numberOfEnemies = 0;

    /// <summary>
    /// Return SpawnPoints Class transform
    /// </summary>
    public Transform SpawnPoint
    {
        get { return _spawnPoint; }
    }

    /// <summary>
    /// Return SpawnPoints Class spawn rate
    /// </summary>
    public float SpawnRate
    {
        get { return _spawnRate; }
    }

    /// <summary>
    /// Return SpawnPoints Class number of enemies to spawn
    /// </summary>
    public int NumberOfEnemies
    {
        get { return _numberOfEnemies; }
    }
}
