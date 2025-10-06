using System;
using System.Collections;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Slider _health;
    [SerializeField] private TMP_Text _countEnemy;
    [SerializeField] private TMP_Text _wave;
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private Button _restartButton;
    [SerializeField] private int _secondsBetweenRounds;

    private int _currentWave = 1;
    

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _spawner.Init();
        _wave.text = _currentWave.ToString();
        _countEnemy.text = _spawner.NumberOfAliveZombie.ToString();
    }

    private void OnEnable()
    {
        _health.maxValue = _player.Health.Amount;
        _player.Health.HealthChanged += OnHealthChanged;
        _spawner.EnemyDied += OnEnemyCountChanged;
        _spawner.WaveCleared += OnWaveCleared;
        _player.Health.Died += OnPlayerDied;
    }


    private void OnDisable()
    {
        _player.Health.HealthChanged -= OnHealthChanged;
        _spawner.EnemyDied -= OnEnemyCountChanged;
        _spawner.WaveCleared -= OnWaveCleared;
        _player.Health.Died -= OnPlayerDied;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnWaveCleared()
    {
        _wave.text = (_currentWave++).ToString();
        StartCoroutine("StartNewWave");
    }

    private void OnEnemyCountChanged(int count)
    {
        _countEnemy.text = count.ToString();
    }

    private void OnHealthChanged(float value)
    {
        _health.value = value;
    }

    private IEnumerator StartNewWave()
    {
        Debug.Log("Kek");
        _timer.gameObject.SetActive(true);

        for (int i = _secondsBetweenRounds; i > 0; i--)
        {
            _timer.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        _timer.gameObject.SetActive(false);
        _spawner.Spawn();
        _countEnemy.text = _spawner.NumberOfAliveZombie.ToString();
    }
    private void OnPlayerDied()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _restartButton.gameObject.SetActive(true);
    }
}
