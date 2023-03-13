using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int Health;
    
    public Action OnDied;
    public Action<int> OnDamageTaken;

    public void TakeDamage(int damage) 
    {
        if(Health <= 0) 
        {
            if(OnDied != null)
                OnDied();
            return;
        }
        
        Health -= damage;

        if(OnDamageTaken != null)
            OnDamageTaken(damage);
    }
}
