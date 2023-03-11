using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;
    private Vector2 _Movement;
    private Vector2 _MousePos;
    private Camera _MainCamera;
    private Shooting _Shooting;
    private Rigidbody2D _Rigidbody;

    private void Start() 
    {
        _MainCamera = Camera.main;
        _Shooting = GetComponent<Shooting>();
        _Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _Movement.x = Input.GetAxisRaw("Horizontal");
        _Movement.y = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("Fire1"))
            _Shooting.Shoot();

        _MousePos = _MainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        _Rigidbody.MovePosition(_Rigidbody.position + _Movement * MoveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = _MousePos - _Rigidbody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        _Rigidbody.rotation = angle;
    }
}
