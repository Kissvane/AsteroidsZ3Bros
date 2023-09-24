using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string sceneName; 
    // Start is called before the first frame update
    void Awake()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        SceneManager.LoadScene(sceneName);
    }
}
