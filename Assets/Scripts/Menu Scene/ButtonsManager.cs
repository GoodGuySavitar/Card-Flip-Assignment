using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Android.Types;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] private GameObject gridSizeSettings; 
    [SerializeField] private TMP_Dropdown dropdown; 

    public int gridSize;

    // private void Awake()
    // {
    //     DontDestroyOnLoad(gameObject);
    // }

    public void StartGame()
    {   
        SceneManager.LoadScene(1);
    }

    public void ChangeSettings()
    {
        gridSizeSettings.SetActive(true);
    }

    // private void Update()
    // {
    //     SetGridSize();
    // }
    // public void SetGridSize()
    // {
    //     if(dropdown.value == 0)
    //     {
    //         gridSize = 3;
    //     }
    //     else if(dropdown.value == 1)
    //     {
    //         gridSize = 4;
    //     }
    // }
}
