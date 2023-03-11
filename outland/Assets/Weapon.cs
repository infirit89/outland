using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon",  menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public byte Range;
    public byte Damage;
    public byte FireRate;
    public Sprite WeaponSprite;
    
    [SerializeField] 
    public GameObject BulletPrefab;
    
}
