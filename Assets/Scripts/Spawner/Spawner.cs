using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public event Action<int> EnemyDied;
    public event Action WaveCleared;

    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private int _startZombieCount;
    [SerializeField] private int _numberOfAddZombiePerRound;
    [SerializeField] private int _numberOfHardZombie;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private Transform _container;


    private List<Enemy> _enemiesPool = new();

    public int NumberOfAliveZombie { get; private set; }

    public void Init()
    {
        for (int i = 0; i < _startZombieCount;  i++)
        {
            var enemy = _enemyFactory.Get(EnemyType.SlowZombie, _spawnPoints[Random.Range(0, _spawnPoints.Count)]);
            _enemiesPool.Add(enemy);
            enemy.DyingEnded += OnEnemyDied;
        }

        NumberOfAliveZombie = _enemiesPool.Count();
    }

    private void OnDisable()
    {
        foreach (Enemy enemy in _enemiesPool)
        {
            enemy.DyingEnded -= OnEnemyDied;
        }
    }

    public void Spawn()
    {
        for (int i = 0; i < _numberOfAddZombiePerRound; i++)
        {
            Enemy enemy;

            if (i < _numberOfHardZombie)
                enemy = _enemyFactory.Get(EnemyType.FastZombie, _spawnPoints[Random.Range(0, _spawnPoints.Count)]);
            else
                enemy = _enemyFactory.Get(EnemyType.SlowZombie, _spawnPoints[Random.Range(0, _spawnPoints.Count)]);

            _enemiesPool.Add(enemy);
            enemy.DyingEnded += OnEnemyDied;
        }

        foreach (Enemy enemy in _enemiesPool)
        {
            enemy.gameObject.SetActive(true);
            enemy.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
        }

        if (_numberOfHardZombie != _numberOfAddZombiePerRound)
        {
            _numberOfHardZombie++;
        }

        NumberOfAliveZombie = _enemiesPool.Count();
    }

    private void OnEnemyDied()
    {

        EnemyDied?.Invoke(--NumberOfAliveZombie);
        
        if (NumberOfAliveZombie == 0)
        {
            WaveCleared?.Invoke();
        }
            
    }
}
