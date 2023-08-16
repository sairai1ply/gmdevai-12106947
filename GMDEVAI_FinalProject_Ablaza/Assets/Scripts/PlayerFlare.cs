using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerFlare : MonoBehaviour
{
    [SerializeField] private List<Monster> _monstersInRange;

    [SerializeField] private float _duration;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _thrownCooldown = 20;

    [SerializeField] private Light _flareLight;
    [SerializeField] private float _flareBaseIntensity;

    private bool _isAvailable;
    private bool _isActive;
    private bool _isThrown;

    [Header("References")]
    [SerializeField] private GameObject _flareFirstPersonModel;
    [SerializeField] private GameObject _flareParticlesGO;
    [SerializeField] private GameObject _flareProjectilePrefab;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private UIPlayer _uiPlayer;


    private void Start()
    {
        _isAvailable = true;
        _isActive = false;
        _isThrown = false;

        _flareLight.intensity = _flareBaseIntensity;
        _flareLight.gameObject.SetActive(false); 

        _flareParticlesGO.SetActive(false);
    }

    private void Update()
    {
        if (_isAvailable && Input.GetMouseButtonDown(0)) 
        {
            ActivateFlare();
        }

        if (_isAvailable && Input.GetMouseButtonDown(1))
        {
            ThrowFlare();
        }

            if (_isActive)
        {
            FlareLightFade();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Monster>() != null)
        {
            _monstersInRange.Add(other.GetComponent<Monster>());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isActive)
        {
            ScareMonsters();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Monster>() != null)
        {
            _monstersInRange.Remove(other.GetComponent<Monster>());
        }
    }

    private void ActivateFlare()
    {

        _flareLight.intensity = _flareBaseIntensity;
        _flareLight.gameObject.SetActive(true);

        StartCoroutine(CO_StartFlareActiveDuration());
        StartCoroutine(CO_StartFlareCooldown(_cooldown));
    }

    private void FlareLightFade()
    {
        if (_flareLight.intensity > 0)
        {
            _flareLight.intensity -= Time.deltaTime * 0.5f;
        }
    }

    private void ScareMonsters()
    {
        foreach (Monster monster in _monstersInRange)
        {
            monster.StartFlee();
        }
    }

    private IEnumerator CO_StartFlareCooldown(float cooldown)
    {
        _uiPlayer.GiveFlareCooldownToUI(cooldown);

        _isAvailable = false;

        if (_isThrown)
        {
            _flareFirstPersonModel.SetActive(false);
        }

        yield return new WaitForSeconds(cooldown);

        _isAvailable = true;

        if (_isThrown)
        {
            _flareFirstPersonModel.SetActive(true);
            _isThrown = false;
        }

        
    }

    private IEnumerator CO_StartFlareActiveDuration()
    {
        _isActive = true;
        _flareParticlesGO.SetActive(true);

        yield return new WaitForSeconds(_duration);

        _isActive = false;
        _flareParticlesGO.SetActive(false);
    }

    private void ThrowFlare()
    {
        GameObject flareProjectile = Instantiate(_flareProjectilePrefab, _muzzle.transform.position, _muzzle.transform.rotation);

        flareProjectile.GetComponent<Rigidbody>().AddForce(_muzzle.transform.forward * 500);

        _isThrown = true;
        StartCoroutine(CO_StartFlareCooldown(_thrownCooldown));
    }
}
