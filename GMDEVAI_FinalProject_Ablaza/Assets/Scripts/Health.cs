using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private UIPlayer _uiPlayer;

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

        _uiPlayer.UpdateUI();

        if (_currentHealth <= 0)
        {
            DoDeath();
            //Destroy(gameObject);
        }
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;

        _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
    }

    private void DoDeath()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
