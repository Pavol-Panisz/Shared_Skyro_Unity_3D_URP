using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    protected Rigidbody _rigidobdy;

    [SerializeField] protected float _speed = 3f;
    [HideInInspector] public float curSpeed;

    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _distanceFromPivotToTheGround = 0f;
    [SerializeField] private float _maxSlopeAngle = 40f;
    protected RaycastHit _groundHit;
    protected bool _isGrounded;
    protected bool _onSlope;
    protected bool _limitOnSlopeSpeed;
    [SerializeField] private float _groundFriction = 10f;
    [HideInInspector] public bool canChangeLinearDamping = true;
    public float linearDamping { get { return _rigidobdy.linearDamping; } set { if (canChangeLinearDamping) { _rigidobdy.linearDamping = value; } } }
    [SerializeField] private float _airMultiplier = 0.8f;
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private float _minJumpTime = 0.1f;
    private float _curJumpTime;

    [SerializeField, Tooltip("Time to rotate on 90 degree angle")] private float _rotSmoothTime = 0.1f;
    private float _curTimeToRotate;
    private float _curRotationTime;
    private Vector3 _startRotatingForwardVector;

    protected Vector3 _beforeMoveDirection;
    protected Vector2 _curHorizontalMoveDirection;

    protected event Action onHittedTheGround;

    protected virtual void Awake()
    {
        _rigidobdy = GetComponent<Rigidbody>();

        curSpeed = _speed;

        onHittedTheGround += OnHittedTheGround;
    }

    protected virtual void Update()
    {
        bool isGruonded = IsGrounded(out _groundHit);
        if(_isGrounded != isGruonded)
        {
            _isGrounded = isGruonded;
            if (isGruonded)
            {
                if (onHittedTheGround != null) onHittedTheGround();
            }
        }
        _onSlope = OnSlope(_groundHit);

        JumpCooldown();

        SpeedControll();
    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void Move(Vector3 horizontalMoveDirection)
    {
        if (horizontalMoveDirection.magnitude < 0.1f) return;

        Vector3 moveDir = new Vector3(horizontalMoveDirection.x, 0f, horizontalMoveDirection.z);
        RotateObjectToTheMoveDirection(moveDir);

        if (_onSlope)
        {
            moveDir = GetSlopeMoveDirection(moveDir, _groundHit.normal);
        }
        _rigidobdy.AddForce(moveDir * curSpeed * 80f * (_isGrounded ? 1f : _airMultiplier), ForceMode.Force);
    }

    protected virtual void RotateObjectToTheMoveDirection(Vector3 moveDirection)
    {
        if (moveDirection.magnitude <= 0.1f) return;
        if (_beforeMoveDirection != moveDirection)
        {
            _curTimeToRotate = Vector3.Angle(transform.forward, moveDirection) / 90f * _rotSmoothTime;
            _curRotationTime = 0f;
            _startRotatingForwardVector = transform.forward;
            _beforeMoveDirection = moveDirection;
        }

        _curRotationTime += Time.fixedDeltaTime;

        RotateObjectToTheMoveDirection(moveDirection, _startRotatingForwardVector, _curTimeToRotate, _curRotationTime);
    }

    protected virtual void RotateObjectToTheMoveDirection(Vector3 moveDirection, Vector3 startForwardVector, float timeToRotate, float curRotateTime)
    {
        transform.forward = Vector3.Slerp(startForwardVector, moveDirection, curRotateTime / timeToRotate);
    }

    protected bool IsGrounded()
    {
        return IsGrounded(out RaycastHit raycastHit);
    }

    protected virtual bool IsGrounded(out RaycastHit raycastHit)
    {
        Vector3 raycastStartPos = transform.position + Vector3.up * (_distanceFromPivotToTheGround > 0f ? 0f : 1f);
        float raycastDistance = _distanceFromPivotToTheGround > 0f ? _distanceFromPivotToTheGround + 0.1f : 1.1f;
        bool isGrounded = Physics.Raycast(raycastStartPos, Vector3.down, out raycastHit, raycastDistance, _groundLayer);

        if(_isGrounded != isGrounded)
        {
            linearDamping = isGrounded ? _groundFriction : 0f;
        }

        return isGrounded;
    }

    protected bool OnSlope(RaycastHit raycastHit)
    {
        if (!_isGrounded) return false;
        float angle = Vector3.Angle(Vector3.up, raycastHit.normal);
        return angle > 0f && angle <= _maxSlopeAngle;
    }

    protected Vector3 GetSlopeMoveDirection(Vector3 moveDir, Vector3 normal)
    {
        return Vector3.ProjectOnPlane(moveDir, normal);
    }

    protected virtual void SpeedControll()
    {
        if (_onSlope)
        {
            if (_limitOnSlopeSpeed)
            {
                if (_rigidobdy.linearVelocity.magnitude > curSpeed)
                {
                    _rigidobdy.linearVelocity = _rigidobdy.linearVelocity.normalized * curSpeed;
                }
            }
        }
        else
        {
            Vector2 horizontalVeclocity = new Vector2(_rigidobdy.linearVelocity.x, _rigidobdy.linearVelocity.z);
            if (horizontalVeclocity.magnitude > curSpeed)
            {
                horizontalVeclocity = horizontalVeclocity.normalized * curSpeed;
                _rigidobdy.linearVelocity = new Vector3(horizontalVeclocity.x, _rigidobdy.linearVelocity.y, horizontalVeclocity.y);
            }
        }
    }

    protected void Jump()
    {
        if(CanJump())
        {
            _curJumpTime = 0f;
            _limitOnSlopeSpeed = false;
            linearDamping = 0f;
            canChangeLinearDamping = false;
            _rigidobdy.linearVelocity = new Vector3(_rigidobdy.linearVelocity.x, 0f, _rigidobdy.linearVelocity.z);
            _rigidobdy.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private bool CanJump()
    {
        return _isGrounded && _minJumpTime <= _curJumpTime;
    }

    private void JumpCooldown()
    {
        if(_curJumpTime < _minJumpTime)
        {
            _curJumpTime += Time.deltaTime;
            if(_curJumpTime >= _minJumpTime)
            {
                OnJumpCooldown();
            }
        }
    }

    protected virtual void OnJumpCooldown()
    {
        _limitOnSlopeSpeed = true;
        canChangeLinearDamping = true;
    }

    protected virtual void OnHittedTheGround()
    {

    }



#if UNITY_EDITOR

    protected virtual void OnValidate()
    {
        curSpeed = _speed;
    }

#endif
}