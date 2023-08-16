using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private PlayerFlare _playerFlare;
    private Health _health;
    private float _currentFlareCooldown;

    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _cooldownText;

    private void Start()
    {
        _playerFlare = _player.GetComponentInChildren<PlayerFlare>(true);
        _health = _player.GetComponent<Health>();

        _currentFlareCooldown = 0f;

        UpdateUI();
    }

    public void GiveFlareCooldownToUI(float cooldown)
    {
        _currentFlareCooldown = cooldown;

        UpdateUI();
        InvokeRepeating("LowerFlareCooldown", 1f, 1f);
    }

    private void LowerFlareCooldown()
    {
        _currentFlareCooldown -= 1;

        UpdateUI();

        if (_currentFlareCooldown <= 0f) 
        { 
            CancelInvoke("LowerFlareCooldown");
        }
    }

    public void UpdateUI()
    {
        _healthSlider.value = HealthToSliderValue();

        _healthText.text = $"{_health.CurrentHealth} / {_health.MaxHealth} HP";

        if (_currentFlareCooldown > 0)
        {
            _cooldownText.text = $"Flare: {_currentFlareCooldown}";
        }

        else
        {
            _cooldownText.text = "Flare: Available";
        }
    }


    private float HealthToSliderValue()
    {
        if (_healthSlider != null)
        {
            return (float)_health.CurrentHealth / (float)_health.MaxHealth;
        }

        else
        {
            return 0f;
        }
    }
}
