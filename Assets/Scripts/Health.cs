using System;
using UnityEngine;

[Serializable]
public class Health 
{
    public event Action<float> HealthChanged;
    public event Action Died;

    [SerializeField] private float _count;
    private float _amount;

    public bool IsAlive => _amount > 0;
    public float Amount => _amount;

    public void Init()
    {
        _amount = _count;
    }

    public void ReduceHealth(float value)
    {
        if (_amount <= 0 || value <= 0)
            return;

        _amount -= value;
        HealthChanged?.Invoke(_amount);

        if (_amount <= 0)
            Died?.Invoke();
    }
}
