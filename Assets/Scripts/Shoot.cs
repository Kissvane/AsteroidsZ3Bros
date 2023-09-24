using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private float missileSpeed;
    [SerializeField] private PlayerLife playerLife;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private AudioSource audioSource;
    // Update is called once per frame
    void Update()
    {
        if (!playerLife.IsInvincible && Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnTransform.position, spawnTransform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(spawnTransform.right * missileSpeed);
            bullet.GetComponent<WrapPosition>().SetGameManager(gameManager);
            audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Count)]);
        }
    }
}
