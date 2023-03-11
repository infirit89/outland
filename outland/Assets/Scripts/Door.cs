using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 CameraChange;
    public Vector3 PlayerChange;
    public DoorDirection DoorDirection;

    private Camera _MainCamera;
    private GameManager _GameManager;
    
    private void Start()
    {
        _MainCamera = Camera.main;
        _GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player")) 
        {
            GameObject currentRoom = GameObject.Find($"Room {_GameManager.PlayerCellPosition.x}, {_GameManager.PlayerCellPosition.y}");
            switch (DoorDirection)
            {
                case DoorDirection.Top: _GameManager.PlayerCellPosition.y++; break;
                case DoorDirection.Bottom: _GameManager.PlayerCellPosition.y--; break;
                case DoorDirection.Left: _GameManager.PlayerCellPosition.x--; break;
                case DoorDirection.Right: _GameManager.PlayerCellPosition.x++; break;
            }
            GameObject nextRoom = GameObject.Find($"Room {_GameManager.PlayerCellPosition.x}, {_GameManager.PlayerCellPosition.y}");
            
            currentRoom.transform.GetChild(6).gameObject.SetActive(false);
            nextRoom.transform.GetChild(6).gameObject.SetActive(true);
            
            _MainCamera.transform.position += CameraChange;
            other.transform.position += PlayerChange;
        }
    }
}
