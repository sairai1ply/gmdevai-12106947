using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField] protected int _damage;

    [SerializeField] protected float _fleeDuration;
    protected bool _isFleeing;
    protected bool _isFleeingFromThrowableFlare;

    public float _fleeRadius;
    public float _detectionRadius;

    public Vector3 _tempThrowableFlareLocation;

    [HeaderAttribute("References")]
    [SerializeField] protected GameObject _playerGO;
    protected Animator _anim;
    public NavMeshAgent _agent;

    public GameObject GetPlayer()
    {
        return _playerGO;
    }

    virtual protected void Awake()
    {
        _playerGO = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();

        _isFleeing = false;
        _isFleeingFromThrowableFlare = false;
    }

    private void Update()
    {
        if (_playerGO == null) return;
        _anim.SetFloat("distance", Vector3.Distance(transform.position, _playerGO.transform.position));
    }

    private IEnumerator CO_StartFleeDuration(float fleeDuration)
    {
        _isFleeing = true;
        _anim.SetBool("IsFleeing", true);
        yield return new WaitForSeconds(fleeDuration);
        _isFleeing = false;
        _anim.SetBool("IsFleeing", false);
    }

    public void StartFlee()
    {
        if (_isFleeing) return;

        Debug.Log("Flee start");
        StartCoroutine(CO_StartFleeDuration(_fleeDuration));
    }

    public void StartFlee(float fleeDuration)
    {
        if (_isFleeing) return;

        Debug.Log("Flee start");
        StartCoroutine(CO_StartFleeDuration(fleeDuration));
    }


    public void DetectThrowableFlare(Vector3 location)
    {
        if (_isFleeingFromThrowableFlare) return;

        _tempThrowableFlareLocation = location;
        StartCoroutine(CO_StartFleeFromThrowableFlare());

        Debug.Log("Throwable Flare detected");
    }

    private IEnumerator CO_StartFleeFromThrowableFlare()
    {
        _isFleeingFromThrowableFlare = true;
        _anim.SetBool("IsFleeingFromThrowableFlare", true);

        yield return new WaitForSeconds(10f);

        _isFleeingFromThrowableFlare = false;
        _anim.SetBool("IsFleeingFromThrowableFlare", false);

        _tempThrowableFlareLocation = _playerGO.transform.position;
    }

    virtual public void Attack()
    {
        _playerGO.GetComponent<Health>().TakeDamage(_damage);
    }
}
