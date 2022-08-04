using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] PlayerAnimations playerAnimations;
    [SerializeField] public Backpack backpack;
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterController.detectCollisions = false;
    }

    private void Update()
    {
        movement.Move(characterController);
        playerAnimations.AnimateRun(movement);
    }
}
