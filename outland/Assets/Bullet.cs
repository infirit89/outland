using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 20f;
    public Rigidbody2D RigidBody;

    public Weapon ShotByWeapon; 
    private float _TimeAlive = 0f;
    void Start()
    {
        RigidBody.velocity = transform.right * Speed;

    }

    void Update()
    {
        _TimeAlive += Time.deltaTime;
        if (_TimeAlive * Speed >= ShotByWeapon.Range)
            Destroy(gameObject);

    }
    /*void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(ShotByWeapon.Damge);
        }
        Destroy(gameObject);
    }
    */

}