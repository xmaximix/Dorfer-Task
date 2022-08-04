using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnimations 
{
    [SerializeField] Animator animator;

    public void AnimateRun(Movement movement)
    {
        if (movement.GetMagnitude() >= 0.1f)
        {
            EnableRunning();
        }
        else
        {
            DisableRunning();
        }
    }

    private void EnableRunning()
    {
        animator.SetBool("Running", true);
    }

    private void DisableRunning()
    {
        animator.SetBool("Running", false);
    }
}
