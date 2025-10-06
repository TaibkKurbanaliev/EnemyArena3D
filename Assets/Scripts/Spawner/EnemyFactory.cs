using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFactory", menuName = "Factory/Enemy")]
public class EnemyFactory : ScriptableObject
{
    [SerializeField] private Enemy _slowZombiePrefab;
    [SerializeField] private Enemy _fastZombiePrefab;

    public Enemy Get(EnemyType type, Transform spawnPoint)
    {
        switch (type)
        {
            case EnemyType.SlowZombie:
                return Instantiate(_slowZombiePrefab, spawnPoint.position, Quaternion.identity);
            case EnemyType.FastZombie:
                return Instantiate(_fastZombiePrefab, spawnPoint.position, Quaternion.identity);
            default:
                throw new NotImplementedException();
        }
    }
}
