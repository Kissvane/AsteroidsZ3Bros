using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AsteroidSize { Enormous = 4, Big = 3, Normal = 2, Small = 1 }

public class Asteroid : MonoBehaviour
{
    public Action<Asteroid, Vector3> AsteroidDestroyed;
    [SerializeField] private AsteroidSize size;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private Collider2D myCollider;
    [SerializeField] private SpriteRenderer myRenderer;
    public AsteroidSize Size => size;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }

    private void Explode()
    {
        AsteroidDestroyed?.Invoke(this, transform.position);
        StartCoroutine(PlaySoundAndDestroy());
    }

    IEnumerator PlaySoundAndDestroy()
    {
        myCollider.isTrigger = true;
        myRenderer.enabled = false;
        AudioClip randomClip = audioClips[UnityEngine.Random.Range(0, audioClips.Count)];
        audioSource.PlayOneShot(randomClip);
        yield return new WaitForSeconds(randomClip.length);
        Destroy(gameObject);
    }
}
