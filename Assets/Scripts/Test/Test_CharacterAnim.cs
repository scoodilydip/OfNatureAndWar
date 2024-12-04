using System.Collections;
using System;
using UnityEngine;

public class Test_CharacterAnim : MonoBehaviour {

    public Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void PlayAttackAnim(Vector3 pos, Action onHit, Action onComplete) {
        if (onHit != null) onHit();
        if (onComplete != null) onComplete();
    }

    public void PlayIdle() {
        animator.SetBool("isMoving", false);
    }

    public void PlayIdle(Vector3 pos) {
        animator.SetFloat("moveX", pos.x);
        animator.SetFloat("moveY", pos.y);
        animator.SetBool("isMoving", false);
    }

    public void PlayMoveAnim(Vector2 move) {
        animator.SetFloat("moveX", move.x);
        animator.SetFloat("moveY", move.y);
        animator.SetBool("isMoving", true);
    }
}