using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    private Vector2 _moveInputs;
    private bool _jumping;

    protected override void Update()
    {
        base.Update();
        transform.rotation = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Move(transform.rotation * new Vector3(_moveInputs.x, 0f, _moveInputs.y));
    }

    protected override void RotateObjectToTheMoveDirection(Vector3 moveDir) { }

    protected override void OnHittedTheGround()
    {
        base.OnHittedTheGround();
        if (_jumping) Jump();
    }

    protected override void OnJumpCooldown()
    {
        base.OnJumpCooldown();
        if(_jumping) Jump();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInputs = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _jumping = !context.canceled;
        if(context.started)
        {
            Jump();
        }
    }
}
