using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuButtons : MonoBehaviour
{
    public TMP_Text _victoryState;

    public Camera[] _cameras;
    public int _index;

    public void OnPlayButtonPress()
    {
        SceneManager.LoadScene("station");
    }

    public void OnQuitButtonPress()
    {
        Application.Quit();
    }

    public void SwitchViewCams()
    {

    }
}
