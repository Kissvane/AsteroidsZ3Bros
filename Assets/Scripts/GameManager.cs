using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private AsteroidsSpawner spawner;
    [SerializeField] private PlayerLife playerLife;
    [SerializeField] private GameObject gameOverPopup;
    [SerializeField] private TMP_Text gameOverScoreLabel;
    [SerializeField] private TMP_Text gameOverState;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;

    private Vector2[] corners = new Vector2[4];
    public Vector2[] Corners => corners;

    public Rect ScreenRect => screenRect;
    private Rect screenRect = new Rect();
    
    private void Awake()
    {
        playerLife.PlayerIsDead += GameOver;
        scoreManager.ScoreChanged += GameOver;

        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1,1,0));
        Vector2 topLeft = mainCamera.ViewportToWorldPoint(new Vector3(0,1,0));
        Vector2 btmLeft = mainCamera.ViewportToWorldPoint(new Vector3(0,0,0));
        Vector2 btmRight = mainCamera.ViewportToWorldPoint(new Vector3(1,0,0));
        corners = new Vector2[]{ topRight, btmRight, btmLeft, topLeft};
        screenRect = new Rect(btmLeft, topRight-btmLeft);
    }

    private void GameOver()
    {
        if (spawner.Asteroids.Count > 0) return;
        gameOverPopup.SetActive(true);
        gameOverState.text = "YOU WIN";
        gameOverScoreLabel.text = $"Score : {scoreManager.Points+playerLife.Life*1000}";
        audioSource.PlayOneShot(audioClips[0]);
    }

    private void GameOver(int playerLife)
    {
        if (playerLife >= 0) return;
        gameOverPopup.SetActive(true);
        gameOverState.text = "YOU LOSE";
        gameOverScoreLabel.text = $"Score : {scoreManager.Points}";
        audioSource.PlayOneShot(audioClips[1]);
    }
}
