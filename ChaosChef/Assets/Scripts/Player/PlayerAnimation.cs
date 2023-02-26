using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimation : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;

    private void Awake() {
        playerController = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();

        animator.SetBool("IsWalking", playerController.IsWalking);
    }
    private void Update() {
        animator.SetBool("IsWalking", playerController.IsWalking);
    }
}
