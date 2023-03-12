using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public float StoppingDistance;
    public float RetreatDistance;
    public float AggroDistance;
    public float StartTimeBetweenShots;
    public int Health = 0;
    private Stats _Stats;

    private Transform _Player;
    private float _TimeBetweenShots;
    private Rigidbody2D _Rigidbody;
    private Shooting _Shooting;
    
    private void Start()
    {
        _Stats = GetComponent<Stats>();
        _Stats.OnDied = () => Destroy(gameObject);
        
        _Player = GameObject.FindGameObjectWithTag("Player").transform;
        _TimeBetweenShots = StartTimeBetweenShots;
        _Rigidbody = GetComponent<Rigidbody2D>();
        _Shooting = GetComponent<Shooting>();
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, _Player.position) > AggroDistance) return;
        Vector2 directionToPlayer = (_Player.position - transform.position);
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90.0f;
        _Rigidbody.rotation = angle;

        if(Vector2.Distance(transform.position, _Player.position) > StoppingDistance)
            transform.position = Vector2.MoveTowards(transform.position, _Player.position, Speed * Time.deltaTime);
        else if(Vector2.Distance(transform.position, _Player.position) < StoppingDistance &&
                Vector2.Distance(transform.position, _Player.position) > RetreatDistance)
            transform.position = transform.position;
        else if(Vector2.Distance(transform.position, _Player.position) < RetreatDistance) 
            transform.position = Vector2.MoveTowards(transform.position, _Player.position, -Speed * Time.deltaTime);

        _Shooting.Shoot();
    }
}
