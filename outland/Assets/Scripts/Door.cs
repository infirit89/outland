using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 CameraChange;
    public Vector3 PlayerChange;

    private Camera _MainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        _MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player")) 
        {
            Debug.Log("I came");
            _MainCamera.transform.position += CameraChange;
            other.transform.position += PlayerChange;
        }
    }
}
