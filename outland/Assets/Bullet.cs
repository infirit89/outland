using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public GameObject HitEffect;
    private float _TimeAlive = 0.0f;
    public Weapon ShotBy;
    public float Speed = 20.0f;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    void Update()
    {
        _TimeAlive += Time.deltaTime;
        if (_TimeAlive * Speed >= ShotBy.Range)
        {
            Destroy(gameObject);
            _TimeAlive = 0f;
        }
    }
}
