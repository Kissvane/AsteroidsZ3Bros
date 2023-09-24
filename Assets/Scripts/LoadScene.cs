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
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    void Awake()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        StartCoroutine(PlaySoundAndLoad());
    }

    IEnumerator PlaySoundAndLoad()
    {
        audioSource.PlayOneShot(audioClip);
        yield return new WaitForSeconds(audioClip.length);
        SceneManager.LoadScene(sceneName);
    }
}
