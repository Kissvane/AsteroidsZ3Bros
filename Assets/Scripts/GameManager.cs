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

    private void Awake()
    {
        playerLife.PlayerIsDead += GameOver;
        scoreManager.ScoreChanged += GameOver;
    }

    private void GameOver()
    {
        if (spawner.Asteroids.Count > 0) return;
        gameOverPopup.SetActive(true);
        gameOverState.text = "YOU WIN";
        gameOverScoreLabel.text = $"Score : {scoreManager.Points}";
    }

    private void GameOver(int playerLife)
    {
        if (playerLife >= 0) return;
        gameOverPopup.SetActive(true);
        gameOverState.text = "YOU LOSE";
        gameOverScoreLabel.text = $"Score : {scoreManager.Points}";
    }
}
