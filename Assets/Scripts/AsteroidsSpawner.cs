using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private List<GameObject> asteroidsPrefabs;
    [SerializeField] private Camera mainCamera;
    //x is used for movement speed when asteroids are spawned and y is used for torque
    [SerializeField] private Vector2 minVelocity;
    [SerializeField] private Vector2 maxVelocity;
    [SerializeField] private int initialWaveSize;
    private Vector3 maxLimitPosition;
    private Vector3 minLimitPosition;

    [SerializeField] private List<Asteroid> asteroids;
    public List<Asteroid> Asteroids => asteroids;

    // Start is called before the first frame update
    void Awake()
    {
        maxLimitPosition = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        minLimitPosition = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        SpawnAsteroidsWave(initialWaveSize);
    }

    void SpawnAsteroidsWave(int waveSize)
    {
        while (waveSize > 0) 
        {
            AsteroidSize randomSize = (AsteroidSize)Mathf.Min(Random.Range(1, 5), waveSize);
            SpawnAsteroid(randomSize, null);
            waveSize -= (int)randomSize;
        }
    }

    private void SpawnAsteroid(AsteroidSize size, Vector3? position)
    {
        GameObject asteroidObject = Instantiate(asteroidsPrefabs[((int)size) - 1],
            position.HasValue ? position.Value : RandomInsideScreen(),
            Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
        Rigidbody2D rigidbody = asteroidObject.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(asteroidObject.transform.right * Random.Range(minVelocity.x, maxVelocity.x), 
            ForceMode2D.Impulse);
        rigidbody.AddTorque(Random.Range(minVelocity.y, maxVelocity.y));
        
        Asteroid asteroid = asteroidObject.GetComponent<Asteroid>();
        asteroid.AsteroidDestroyed += CreateAsteroidsChunks;
        asteroids.Add(asteroid);
        scoreManager.RegisterAsteroid(asteroid);

        asteroidObject.GetComponent<WrapPosition>().SetGameManager(gameManager);
    }

    private void CreateAsteroidsChunks(Asteroid asteroid, Vector3 position)
    {
        asteroids.Remove(asteroid);
        asteroid.AsteroidDestroyed -= CreateAsteroidsChunks;

        if (asteroid.Size == AsteroidSize.Small) return;
        int groupSize = (int)asteroid.Size;
        while (groupSize > 0)
        {
            AsteroidSize randomSize = (AsteroidSize)Mathf.Min(Random.Range(1, groupSize), groupSize);
            SpawnAsteroid(randomSize, position);
            groupSize -= (int)randomSize;
        }
    }

    private Vector3 RandomInsideScreen()
    {
        return new Vector3(
            Random.Range(minLimitPosition.x, maxLimitPosition.x),
            Random.Range(minLimitPosition.y, maxLimitPosition.y),
            0f
        );
    }
}
