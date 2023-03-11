using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [SerializeField] private Transform _Muzzle;
    
    private float _NextTimeToFire = 0f;

    [SerializeField] protected Weapon p_Weapon;
    
    public void Start()
    {
    }

    private bool CheckFireRate()
    {
        if (Time.time >= _NextTimeToFire)
        {
            _NextTimeToFire = Time.time + (1f / p_Weapon.FireRate);
            return true;
        }
        return false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckFireRate())
            {
                Shoot(_Muzzle);
            }
        }
    }
    

    public void Shoot(Transform origin)
    {
        GameObject bullet = Instantiate(
            p_Weapon.BulletPrefab,
            origin.position,
            origin.rotation,
            null
        );
    }
}
