using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public Rigidbody2D Rigidbody;
    private Vector2 _Movement;
    private Vector2 _MousePos;
    private Camera _MainCamera;

    private void Start() 
    {
        _MainCamera = Camera.main;
    }

    private void Update()
    {
        _Movement.x = Input.GetAxisRaw("Horizontal");
        _Movement.y = Input.GetAxisRaw("Vertical");

        _MousePos = _MainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Rigidbody.MovePosition(Rigidbody.position + _Movement * MoveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = _MousePos - Rigidbody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        Rigidbody.rotation = angle;
    }
}
