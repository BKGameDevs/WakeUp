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
    private Collider2D _Collider;
    private Animator _Animator;
    [SerializeField] private LayerMask layerMask;

    public FloatVariable PlayerSanity;
    //public FloatVariable ItemPickUp;

    private int MAX_SANITY = 100;
    public int SanityReduceRate = 1;
    public int SanityReduceInterval = 5;
    private float _CurrentSanity;
    private float _StartTime;
    private float _ElapsedTime;

    private bool _IsGrounded;
    private bool _IsInteracting;

    private Vector3 _SoftCheckpoint;
    private Vector3 _HardCheckpoint;

    public GameEvent OnPlayerDeath;
    public GameEvent OnPlayerDamage;
    // private Vector3 _OriginalPosition;
    void Start()
    {
        //Set position of transform
        // _OriginalPosition = _Position = transform.position;
        _RigidBody = GetComponent<Rigidbody2D>();
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _Collider = GetComponent<Collider2D>();
        _Animator = GetComponent<Animator>();

        _CurrentSanity = MAX_SANITY;
        _StartTime = Time.fixedTime;

        //if (ItemPickUp != null)
        //    ItemPickUp.PropertyChanged += ItemPickUp_PropertyChanged;

        _HardCheckpoint = transform.position;

    }

    //private void ItemPickUp_PropertyChanged(object sender, EventArgs args) {
    //    if (ItemPickUp.RuntimeValue > 0)
    //        _CurrentSanity += ItemPickUp.RuntimeValue;
    //    ItemPickUp.RuntimeValue = 0;
    //}

    // Update is called once per frame
    void Update()
    {
        if (_IsInteracting)
            return;

        // Setting bool values with conditionals to check if layer is moving left or right
        // Setting bool values with conditionals to check if layer is moving up or down
        _Horizontal = IsPressingLeft ? -1 : (IsPressingRight ? 1 : 0);
        if(_Horizontal != 0) {
            // Debug.Log(_Horizontal);
        }
        
        _Vertical = IsPressingDown ? -1 : ((IsPressingUp || IsPressingSpace) ? 1 : 0);

        _Animator.SetBool("Running", _Horizontal != 0);
        if (_Horizontal != 0)
            _SpriteRenderer.flipX = _Horizontal < 0;
            
        if (_Animator.GetBool("Jumping") && _IsGrounded)
            _Animator.SetBool("Jumping", false);

        if (_Vertical > 0 && _IsGrounded)
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

        if (PlayerSanity != null)
            PlayerSanity.RuntimeValue = _CurrentSanity;
    }


    private void FixedUpdate()
    {
        _IsGrounded = IsGrounded();
        _ElapsedTime = Time.fixedTime;
        if (_Jump)
        {
            _Jump = false;
            _IsGrounded = false;
            // _RigidBody.velocity = transform.up * _JumpForce; /* jump is more realstic with this one, but doesn't work perfectly with boxcast */
            _RigidBody.velocity = new Vector2(0, 0);
            _RigidBody.AddForce(transform.up * _JumpForce, ForceMode2D.Impulse);
        }

        var yVelocity = _IsGrounded ? 0 : _RigidBody.velocity.y;
        _RigidBody.velocity = new Vector2(_Horizontal * Speed, yVelocity);
    }

    private bool IsGrounded()
    {
        var extraLength = .1f; //can be adjusted if the box's y extent doesn't seem perfect
        var size = _Collider.bounds.size;
        var newSize = new Vector3(size.x / 2, size.y, size.z);
        RaycastHit2D hit = Physics2D.BoxCast(_Collider.bounds.center, newSize, 0f, Vector2.down, extraLength, layerMask);
        
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

    public void OnKillzoneEnter(){ 
        ResetPlayer(_SoftCheckpoint);
        ReduceSanity(25f);
    }


    private void ReduceSanity(float amount){
        _CurrentSanity -= amount;
        _CurrentSanity = _CurrentSanity <= 0f ? 0f : _CurrentSanity;
        if (IsPlayerDead()) {
            OnPlayerDeath?.Raise();
            ResetPlayerOnDeath();
        } else {
            //Will be used by other systems later
            OnPlayerDamage?.Raise();
        }
    }
    private void ResetPlayerOnDeath() {
        ResetPlayer(_HardCheckpoint);
        ResetSanity(100f);
    }

    private bool IsPlayerDead(){
        return _CurrentSanity <= 0f;
    }

    private void ResetSanity(float amount){
        _CurrentSanity = amount;
    }

    public void UpdateHardCheckpoint(object newPos) {
        _HardCheckpoint = (Vector3) newPos;
    }

    public void UpdateSoftCheckpoint(object newPos) {
        _SoftCheckpoint = (Vector3) newPos;
    }

    public void ResetPlayer(Vector3 resetPos) {
        transform.position = resetPos;
    }

    public void SetInteract(object value) {
        _IsInteracting = (bool)value;
        if (!_IsInteracting)
            _StartTime = _ElapsedTime;
    }

    public void CouragePickedUp(object value)
    {
        _CurrentSanity += (float)value;
        _CurrentSanity = _CurrentSanity > MAX_SANITY ? MAX_SANITY : _CurrentSanity;
    }

}