using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AsteroidSize { Enormous = 4, Big = 3, Normal = 2, Small = 1 }

public class Asteroid : MonoBehaviour
{
    public Action<Asteroid, Vector3> AsteroidDestroyed;
    [SerializeField] private AsteroidSize size;
    public AsteroidSize Size => size;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }

    private void Explode()
    {
        AsteroidDestroyed?.Invoke(this, transform.position);
        Destroy(gameObject);
    }
}
