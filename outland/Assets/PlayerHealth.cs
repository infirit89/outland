using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 3;
    public int CurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(int amount)
    {
        CurrentHealth-=amount;
        Debug.Log(GameObject.Find("heart " + CurrentHealth));
        Destroy(GameObject.Find("heart " + CurrentHealth));
        //Debug.Log(GetComponent("heart" + CurrentHealth));
        if(CurrentHealth <= 0){
             SceneManager.LoadSceneAsync("EndScreen");
        }
    }
}
