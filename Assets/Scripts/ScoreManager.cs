using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Action ScoreChanged;
    private int points = 0;
    public int Points => points;
    //each asteroids size score is stored in the corresponding index in the list
    [SerializeField] private List<int> pointsByAsteroidSize;
    [SerializeField] private TMP_Text scoreLabel;

    private void Awake()
    {
        scoreLabel.text = $"Score : {points}";
    }

    public void RegisterAsteroid(Asteroid asteroid)
    {
        asteroid.AsteroidDestroyed += GainPoints;
    }

    private void GainPoints(Asteroid asteroid, Vector3 vector)
    {
        asteroid.AsteroidDestroyed -= GainPoints;
        points += pointsByAsteroidSize[(int)asteroid.Size - 1];
        scoreLabel.text = $"Score : {points}";
        ScoreChanged?.Invoke();
    }
}
