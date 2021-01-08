using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static WakeUp.Constants.PlayerInput;

public class PlayerController : MonoBehaviour
{
    private Vector3 _Position;
    private float _Horizontal;
    private float _Vertical;
    public float Speed = 5;
    public float _JumpForce = 5;
    private bool _IsGrounded;
    private bool _Jump;
    private Rigidbody2D _RigidBody;
    void Start()
    {
        //Set position of transform
        _Position = transform.position;
        _RigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Setting bool values with conditionals to check if layer is moving left or right
        // Setting bool values with conditionals to check if layer is moving up or down
        _Horizontal = IsPressingLeft ? -1 : (IsPressingRight ? 1 : 0);
        _Vertical = IsPressingDown ? -1 : (IsPressingUp ? 1 : 0);


        if (_Vertical > 0 && !_Jump)
        {
            _Jump = true;
            _IsGrounded = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts.Any(x => x.normal.x == 0 && x.normal.y == 1))
            _IsGrounded = true;
    }

    private void FixedUpdate()
    {
        if (_Jump)
        {
            _Jump = false;
            _RigidBody.AddForce(transform.up * _JumpForce, ForceMode2D.Impulse);
        }

        _RigidBody.velocity = new Vector2(_Horizontal * Speed, _RigidBody.velocity.y);
    }
}