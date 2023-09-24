using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] Rigidbody2D myRigidbody;
    [SerializeField] Collider2D myCollider;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private int life = 5;
    [SerializeField] private float deathTransparency = 100f;
    [SerializeField] private float invincibilityTime = 3f;
    [SerializeField] private float flickerPeriod = 0.25f;
    [SerializeField] private TMP_Text lifeLabel;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;
    public Action<int> PlayerIsDead;

    private bool isInvincible = false;
    public bool IsInvincible => isInvincible;
    public int Life => life;
    private Color normalColor;
    private Color deathColor;

    private void Awake()
    {
        lifeLabel.text = $"Life : {life}"; 
        normalColor = spriteRenderer.color;
        deathColor = new Color(
            spriteRenderer.color.r,
            spriteRenderer.color.g,
            spriteRenderer.color.b,
            deathTransparency);

        StartCoroutine(Spawn());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            KillPlayer();
        }
    }

    void KillPlayer()
    {
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.angularVelocity = 0f;
        life--;
        if (life < 0)
        {
            PlayerIsDead?.Invoke(life);
            gameObject.SetActive(false);
            return;
        }
        audioSource.PlayOneShot(audioClips[UnityEngine.Random.Range(0, audioClips.Count)]);
        lifeLabel.text = $"Life : {life}";
        transform.position = Vector3.zero;

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        isInvincible = true;
        myCollider.isTrigger = true;
        StartCoroutine(DeathTwinkle());

        yield return new WaitForSeconds(invincibilityTime);

        spriteRenderer.color = normalColor;
        myCollider.isTrigger = false;
        isInvincible = false;
    }

    private IEnumerator DeathTwinkle()
    {
        float startTime = Time.time;
        float elapsedTime = Time.time - startTime;
        while (elapsedTime < invincibilityTime)
        {
            if(Mathf.FloorToInt(elapsedTime/ flickerPeriod) %2 == 0)
                spriteRenderer.color = deathColor;
            else
                spriteRenderer.color = normalColor;
            yield return new WaitForSeconds(flickerPeriod);
            elapsedTime = Time.time - startTime;
        }
    }


}
