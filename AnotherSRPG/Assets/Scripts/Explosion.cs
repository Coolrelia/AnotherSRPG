using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifetime;

    private void Start()
    {
        Invoke("Destruction", lifetime);
    }

    void Destruction()
    {
        Destroy(gameObject);
    }
}
