using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _playerGO;
    [SerializeField] private GameObject _scientistModelGO;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private List<Camera> _monsterCameras;

    private FPS_Controller _controller;
    private int _cameraIndex = 0;
    private bool _isViewingMonsterCam;

    private void Start()
    {
        _scientistModelGO.SetActive(false);
        _isViewingMonsterCam = false;

        _controller = _playerGO.GetComponent<FPS_Controller>();

        Camera[] temp  = FindObjectsOfType<Camera>(true);

        foreach(Camera c in temp)
        {
            if (c != _playerCamera)
            {
                c.gameObject.SetActive(false);
                _monsterCameras.Add(c);
            }
        }
    }

    private void Update()
    {
        if (_isViewingMonsterCam && Input.GetKeyDown(KeyCode.Q))
        {
            _cameraIndex--;

            if (_cameraIndex < 0)
            {
                _cameraIndex = _monsterCameras.Count - 1;
            }

            SwitchToMonsterCamera(_cameraIndex);
        }

        if (_isViewingMonsterCam && Input.GetKeyDown(KeyCode.E)) 
        {
            _cameraIndex++;

            if (_cameraIndex >= _monsterCameras.Count)
            {
                _cameraIndex = 0;
            }

            SwitchToMonsterCamera(_cameraIndex);
        }

        //toggle camera viewing mode from Player to Monster Cam array and vice versa
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_isViewingMonsterCam)
            {
                _isViewingMonsterCam = false;
                SwitchToPlayerCamera();
            }

            else
            {
                _isViewingMonsterCam = true;
                SwitchToMonsterCamera(_cameraIndex);
            }
        }
    }
    private void SwitchToMonsterCamera(int index)
    {
        _scientistModelGO.SetActive(true);
        _playerCamera.gameObject.SetActive(false);
        DisableAllMonsterCameras();

        _monsterCameras[index].gameObject.SetActive(true);
    }

    private void SwitchToPlayerCamera()
    {
        DisableAllMonsterCameras();
        _scientistModelGO.SetActive(false);

        _playerCamera.gameObject.SetActive(true);
    }

    private void DisableAllMonsterCameras()
    {
        foreach (Camera c in _monsterCameras)
        {
            c.gameObject.SetActive(false);
        }
    }
}
