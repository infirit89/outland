using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject BulletPrefab;
    public float Speed = 20.0f;
    public Weapon ShotWith;
    public float _TimeSinceLastShot = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && _TimeSinceLastShot * 10 >= ShotWith.FireRate)
        {
            Shoot();
            _TimeSinceLastShot = 0;
        }
        else
            _TimeSinceLastShot += Time.deltaTime;
        
    }

    void Shoot()    
    {
        GameObject _Bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        Rigidbody2D _RigitBody = _Bullet.GetComponent<Rigidbody2D>();
        _RigitBody.AddForce(FirePoint.up * Speed, ForceMode2D.Impulse);
    }
    
    
}
