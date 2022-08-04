using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Movement
{
    [SerializeField] float speed;
    private const float gravityValue = -9.81f;
    private bool grounded;
    private Vector3 velocity;
    private Vector3 moveDir;

    public void Move(CharacterController characterController)
    {
        grounded = characterController.isGrounded;
        if (grounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        moveDir = new Vector3(PlayerInput.GetJoystickHorizontal(), 0, PlayerInput.GetJoystickVertical());
        characterController.Move(speed * Time.deltaTime * moveDir);

        if (moveDir != Vector3.zero)
        {
            characterController.gameObject.transform.forward = moveDir;
        }

        velocity.y += gravityValue * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public float GetMagnitude()
    {
        return moveDir.magnitude;
    }
}
