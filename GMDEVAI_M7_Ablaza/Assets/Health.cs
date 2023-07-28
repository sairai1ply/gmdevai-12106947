using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int CurrentHealth
    {
        get => _currentHealth;
    }

    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;

    public void Initialize()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        _currentHealth = Mathf.Max(_currentHealth, 0);

        if (this.gameObject.GetComponent<TankAI>() != null )
        {
            float hpPercent = (float)_currentHealth / (float)_maxHealth;
            Debug.Log($"HP%: {hpPercent}");
            if (hpPercent <= 0.2f)
            {
                Debug.Log($"Trying to flee");
                this.GetComponent<Animator>().SetBool("IsFleeing", true);
            }
        }

        if (_currentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} took {damage} dmg and died");
            Destroy(gameObject);
        }
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;

        _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
    }
}
