using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using static WakeUp.Constants.PlayerInput;

public class PlayerController : MonoBehaviour
{
    private Vector3 _Position;
    private float _Horizontal;
    private float _Vertical;
    public float Speed = 5;
    public float _JumpForce = 5;
    private bool _Jump;
    private Rigidbody2D _RigidBody;
    private SpriteRenderer _SpriteRenderer;
    private BoxCollider2D _Collider;
    private Animator _Animator;
    [SerializeField] private LayerMask layerMask;

    public FloatVariable PlayerHp;

    private int MAX_SANITY = 100;
    public int SanityReduceRate = 1;
    public int SanityReduceInterval = 5;
    private int CurrentInterval = 1;
    private int _CurrentSanity;
    private float _StartTime;
    private float _ElapsedTime;
    public EventHandler<int> SanityReduced { get; set; }

    void Start()
    {
        //Set position of transform
        _Position = transform.position;
        _RigidBody = GetComponent<Rigidbody2D>();
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _Collider = GetComponent<BoxCollider2D>();
        _Animator = GetComponent<Animator>();

        _CurrentSanity = MAX_SANITY;
        _StartTime = Time.fixedTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Setting bool values with conditionals to check if layer is moving left or right
        // Setting bool values with conditionals to check if layer is moving up or down
        _Horizontal = IsPressingLeft ? -1 : (IsPressingRight ? 1 : 0);
        _Vertical = IsPressingDown ? -1 : ((IsPressingUp || IsPressingSpace) ? 1 : 0);

        _Animator.SetBool("Running", _Horizontal != 0);
        if (_Horizontal != 0)
            _SpriteRenderer.flipX = _Horizontal < 0;
            
        if (_Animator.GetBool("Jumping") && _RigidBody.velocity.y == 0)
            _Animator.SetBool("Jumping", false);

        if (_Vertical > 0 && IsGrounded())
        {
            _Jump = true;
            _Animator.SetBool("Jumping", true);
        }

        if (_Horizontal != 0){
            _StartTime = _ElapsedTime;
        }

        //Amount of time passed since Player creation
        var time = _ElapsedTime - _StartTime;
        if (time > SanityReduceInterval) {
            _StartTime = _ElapsedTime;
            ReduceSanity(SanityReduceRate);
        }

        if (PlayerHp != null)
            PlayerHp.Value = _CurrentSanity;
        // if (IsPressingEnter)
        //     ReduceSanity(1);
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     Debug.Log(collision);
    //     if (collision.contacts.Any(x => x.normal.x == 0 && x.normal.y == 1))
    //     {
    //         Debug.Log("Hello");
    //         _IsGrounded = true;
    //     }
    // }

    private void FixedUpdate()
    {
        _ElapsedTime = Time.fixedTime;
        if (_Jump)
        {
            _Jump = false;
            // _RigidBody.velocity = transform.up * _JumpForce; /* jump is more realstic with this one, but doesn't work perfectly with boxcast */
            _RigidBody.velocity = new Vector2(0, 0);
            _RigidBody.AddForce(transform.up * _JumpForce, ForceMode2D.Impulse);
        }

        _RigidBody.velocity = new Vector2(_Horizontal * Speed, _RigidBody.velocity.y);
    }

    private bool IsGrounded()
    {
        var extraLength = .1f; //can be adjusted if the box's y extent doesn't seem perfect
        RaycastHit2D hit = Physics2D.BoxCast(_Collider.bounds.center, _Collider.bounds.size, 0f, Vector2.down, extraLength, layerMask);
        
        /* uncomment to show the BoxCast (green means it is not grounded, red means it is) */
        // Color rayColor;
        // if (hit.collider != null) {
        //     rayColor = Color.green;
        // }
        // else {
        //     rayColor = Color.red;
        // }
        // Debug.DrawRay(_Collider.bounds.center + new Vector3(_Collider.bounds.extents.x, 0), Vector2.down * (_Collider.bounds.extents.y + extraLength), rayColor);
        // Debug.DrawRay(_Collider.bounds.center - new Vector3(_Collider.bounds.extents.x, 0), Vector2.down * (_Collider.bounds.extents.y + extraLength), rayColor);
        // Debug.DrawRay(_Collider.bounds.center - new Vector3(_Collider.bounds.extents.x, _Collider.bounds.extents.y + extraLength), Vector2.right * (_Collider.bounds.extents.x), rayColor);
        return hit.collider != null;
    }

    private void ReduceSanity(int amount){
        _CurrentSanity -= amount;
        _CurrentSanity = _CurrentSanity < 0 ? 0 : _CurrentSanity;
        
        SanityReduced?.Invoke(this, _CurrentSanity);
    }
}