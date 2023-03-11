using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject BulletPrefab;
    public float Speed = 20.0f;
    public Weapon ShotWith;
    public float TimeSinceLastShot = 0.0f;

    // Update is called once per frame
    void Update()
    {
        TimeSinceLastShot += Time.deltaTime;
    }

    public void Shoot()    
    {
        if(TimeSinceLastShot * 10 < ShotWith.FireRate)
            return;
        
        TimeSinceLastShot = 0;
        GameObject _Bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        _Bullet.GetComponent<Bullet>().Damage = ShotWith.Damage;
        Rigidbody2D _RigitBody = _Bullet.GetComponent<Rigidbody2D>();
        _RigitBody.AddForce(FirePoint.up * Speed, ForceMode2D.Impulse);
    }
    
    
}
