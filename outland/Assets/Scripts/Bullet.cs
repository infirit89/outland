using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public GameObject HitEffect;
    private float _TimeAlive = 0.0f;
    public Weapon ShotBy;
    public float Speed = 20.0f;

    [HideInInspector]
    public int Damage = 0;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        // TODO: make entity base class
        if(other.CompareTag("Player") || other.CompareTag("Enemy"))
            other.GetComponent<Stats>().TakeDamage(Damage);
        
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
