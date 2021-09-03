using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using static WakeUp.Constants.PlayerInput;

public class PlayerController : MonoBehaviour
{

    private Coroutine _CameraTiltCoroutine;
    private float _TiltDirection;
    private float TiltDirection
    {
        get { return _TiltDirection; }
        set
        {
            if (_TiltDirection != value)
            {
                _TiltDirection = value;
                if (_CameraTiltCoroutine != null)
                {
                    StopCoroutine(_CameraTiltCoroutine);
                    OnTiltCamera.Raise(0f);
                }
                _CameraTiltCoroutine = this.StartTimedAction(null,
                    () =>
                    {
                        OnTiltCamera.Raise(_TiltDirection);
                    }, 0.5f);
            }
        }
    }
    private bool _IsTiltingCamera;

    private float _Horizontal;
    private float _Vertical;
    public float Speed = 5;
    public float _JumpForce = 5;
    private bool _Jump;
    private bool _Jumping;
    private bool _Falling;
    private Rigidbody2D _RigidBody;
    private SpriteRenderer _SpriteRenderer;
    private Collider2D _Collider;
    private Animator _Animator;
    [SerializeField] private LayerMask layerMask;

    public FloatVariable PlayerSanity;
    //public FloatVariable ItemPickUp;

    private Coroutine _ReduceSanityCoroutine;
    private int MAX_SANITY = 100;
    public int SanityReduceRate = 1;
    public int SanityReduceInterval = 5;
    public float RespawnDelay = 1f;
    private float _CurrentSanity;

    public bool IsGrounded => _IsGrounded = CheckGrounded();
    private bool _IsGrounded;
    public GameObject CurrentGround { get; private set; }

    private bool _IsInteracting;
    private bool _IsInCutscene;
    private bool _IsReseting;
    public bool UpdateDisabled { get => _IsInteracting || _IsReseting || _IsInCutscene;  }

    private Vector3 _SoftCheckpoint;
    private Vector3 _HardCheckpoint;

    public GameEvent OnPlayerDeath;
    public GameEvent OnPlayerDamage;
    public GameEvent OnTiltCamera;
    public GameEvent OnPlayerJump;
    public GameEvent OnPlayerSpawn;
    // private Vector3 _OriginalPosition;

    private void Awake()
    {
        //Set position of transform
        _RigidBody = GetComponent<Rigidbody2D>();
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _Collider = GetComponent<Collider2D>();
        _Animator = GetComponent<Animator>();
    }

    void Start()
    {
        _CurrentSanity = MAX_SANITY;

        //if (ItemPickUp != null)
        //    ItemPickUp.PropertyChanged += ItemPickUp_PropertyChanged;

        _HardCheckpoint = transform.position + new Vector3(0, 1, 0);
        UpdateSoftCheckpoint(_HardCheckpoint);
        StartReduceSanity();
    }

    //private void ItemPickUp_PropertyChanged(object sender, EventArgs args) {
    //    if (ItemPickUp.RuntimeValue > 0)
    //        _CurrentSanity += ItemPickUp.RuntimeValue;
    //    ItemPickUp.RuntimeValue = 0;
    //}

    // Update is called once per frame
    void Update()
    {
        _Horizontal = _Vertical = 0;
        if (UpdateDisabled)
            return;

        // Setting bool values with conditionals to check if layer is moving left or right
        // Setting bool values with conditionals to check if layer is moving up or down
        _Horizontal = IsPressingLeft ? -1 : (IsPressingRight ? 1 : 0);

        _Vertical = IsPressingSpace ? 1 : 0;

        var tiltDirection = IsPressingDown ? -1 : (IsPressingUp ? 1 : 0);
        TiltDirection = (_Horizontal == 0 && _IsGrounded) ? tiltDirection : 0;


        _Animator.SetBool("Running", _Horizontal != 0);
        if (_Horizontal != 0)
            _SpriteRenderer.flipX = _Horizontal < 0;


        if (_Vertical > 0 && _IsGrounded)
            _Jumping = _Jump = true;

        //else if (_IsGrounded && _Jumping)
        //{
        //    _Jumping = false;
        //    _Animator.SetBool("Jumping", false);
        //}

        _Animator.SetBool("Jumping", !_IsGrounded && _Jumping);
        _Animator.SetBool("Falling", !_IsGrounded && _Falling);

        if (_Horizontal != 0 || _Vertical != 0)
            //TODO: Add way to prevent from stopping midway
            StopReduceSanity();

        if (_ReduceSanityCoroutine == null)
            StartReduceSanity();

        if (PlayerSanity != null)
            PlayerSanity.RuntimeValue = _CurrentSanity;
    }

    private void StartReduceSanity()
    {
        _ReduceSanityCoroutine = this.StartTimedAction(null,
            () =>
            {
                ReduceSanity(SanityReduceRate);
                StartReduceSanity();
            }, SanityReduceInterval);
    }

    private void StopReduceSanity()
    {
        if (_ReduceSanityCoroutine == null)
            return;

        StopCoroutine(_ReduceSanityCoroutine);
        _ReduceSanityCoroutine = null;
    }

    private void FixedUpdate()
    {
        var wasGrounded = _IsGrounded;

        _IsGrounded = CheckGrounded();

        if (!wasGrounded && _IsGrounded)
            _Jumping = _Falling = false;

        Move();
    }

    private void Move()
    {
        if (_Jump)
        {
            OnPlayerJump?.Raise();
            _Jump = false;
            _IsGrounded = false;
            // _RigidBody.velocity = transform.up * _JumpForce; /* jump is more realstic with this one, but doesn't work perfectly with boxcast */
            //_RigidBody.velocity = new Vector2(0, _JumpForce);
            _RigidBody.velocity = Vector2.zero;
            _RigidBody.AddForce(transform.up * _JumpForce, ForceMode2D.Impulse);
        }

        var yVelocity = _IsGrounded ? 0 : _RigidBody.velocity.y;
        _RigidBody.velocity = new Vector2(_Horizontal * Speed, yVelocity);

        if (!_IsGrounded)
        {
            _Jumping = yVelocity > 0;
            _Falling = yVelocity < 0;
        }
    }

    private bool CheckGrounded()
    {
        var extraLength = .001f; //can be adjusted if the box's y extent doesn't seem perfect
        var size = _Collider.bounds.size;
        var newSize = new Vector3(size.x / 2, extraLength, size.z);
        var origin = _Collider.bounds.center - new Vector3(0, size.y / 2, 0);
        RaycastHit2D hit = Physics2D.BoxCast(origin, newSize, 0f, Vector2.down, extraLength, layerMask);

        /* uncomment to show the BoxCast (green means it is not grounded, red means it is) */
        Color rayColor;
        if (hit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        var xOffset = newSize.x / 2;
        Debug.DrawRay(origin + new Vector3(xOffset, 0), Vector2.down * extraLength, rayColor);
        Debug.DrawRay(origin - new Vector3(xOffset, 0), Vector2.down * extraLength, rayColor);
        Debug.DrawRay(origin - new Vector3(xOffset, extraLength), Vector2.right * xOffset, rayColor);
        Debug.DrawRay(origin + new Vector3(xOffset, -extraLength), Vector2.left * xOffset, rayColor);

        CurrentGround = null;
        if (hit.collider != null)
            CurrentGround = hit.collider.gameObject;

        return CurrentGround != null;
    }

    public void OnKillzoneEnter(object value) {
        if (_IsReseting)
            return;

        float damage = value is float ? (float)value : 25f;

        ReduceSanity(damage, true);
        if (!IsPlayerDead())
        {
            _IsReseting = true;
            CrossFadeController.Instance.RunCrossFade(() => ResetPlayer(_SoftCheckpoint));
        }
    }


    private void ReduceSanity(float amount, bool killzoneTriggered = false){
        _CurrentSanity -= amount;
        _CurrentSanity = _CurrentSanity <= 0f ? 0f : _CurrentSanity;
        if (IsPlayerDead()) {
            //if (!killzoneTriggered) //TODO: If only for sound, cool, if not then think better
                OnPlayerDeath?.Raise(); //TODO: Find some better solution to this
            _IsReseting = true;
            CrossFadeController.Instance.RunCrossFade(() =>
            {
                ResetPlayer(_HardCheckpoint);
                ResetSanity();
            });
        } else {
            //Will be used by other systems later
            OnPlayerDamage?.Raise();
        }
    }
    //private void ResetPlayerOnDeath() {
    //    yield return new WaitForSeconds(RespawnDelay);

    //    ResetPlayer(_HardCheckpoint);
    //    ResetSanity(100f);
    //}

    private bool IsPlayerDead(){
        return _CurrentSanity <= 0f;
    }

    public void ResetSanity(){
        _CurrentSanity = MAX_SANITY;
    }

    public void UpdateHardCheckpoint(object newPos) {
        _HardCheckpoint = (Vector3) newPos;

        if (_SoftCheckpoint == Vector3.zero)
            _SoftCheckpoint = _HardCheckpoint;
    }

    public void UpdateSoftCheckpoint(object newPos) {
        _SoftCheckpoint = (Vector3) newPos;
    }

    private Coroutine _ResetDamageCoroutine;
    public void ResetPlayer(Vector3 resetPos, bool triggersBlinking = true) {
        //CrossFadeController.Instance.RunCrossFadeWithAction(1f, 0.75f, () => transform.position = resetPos);

        

        var size = _Collider.bounds.size;
        var origin = resetPos - new Vector3(0, size.y / 2, 0);

        var hit = Physics2D.Raycast(origin, Vector2.down, 20f, layerMask);

        if (hit.collider != null)
        {
            var yOffset = origin.y - hit.point.y;

            resetPos += new Vector3(0, -yOffset, 0);
        }
        transform.position = resetPos;
        _IsReseting = false;

        if (_ResetDamageCoroutine != null)
            StopCoroutine(_ResetDamageCoroutine);

        if (triggersBlinking)
        {
            var loopCount = 12;
            _ResetDamageCoroutine = this.StartLoopingAction(
                () =>
                {
                    _SpriteRenderer.enabled = !_SpriteRenderer.enabled;
                    loopCount--;
                },
                () => loopCount > 0,
                0.25f,
                () => _SpriteRenderer.enabled = true
                );
        }

        OnPlayerSpawn?.Raise();
    }

    //private void ResetPlayerToGround(Vector3 resetPos)
    //{
    //}

    public void SetInteract(object value) {
        _IsInteracting = (bool)value;
        if (_IsInteracting)
            StopReduceSanity();
    }

    public void SetIsInCutscene(object value)
    {
        _IsInCutscene = (bool)value;
        if (_IsInCutscene)
            StopReduceSanity();
    }

    public void CouragePickedUp(object value)
    {
        _CurrentSanity += (float)value;
        _CurrentSanity = _CurrentSanity > MAX_SANITY ? MAX_SANITY : _CurrentSanity;
    }
}