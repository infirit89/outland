using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public Sprite Playercopidlepistolsheet;
    // Start is called before the first frame update
    public int Id;
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            Destroy(gameObject);
            if(Id==1){
                Debug.Log(1);
                this.gameObject.GetComponent<SpriteRenderer>.sprite = Playercopidlepistolsheet;
            }else if(Id==2){
                Debug.Log(2);
            }
        }
    }
}
