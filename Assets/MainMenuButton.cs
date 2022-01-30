using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{

    Animator animator;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();

    }
    public void ResetButtonState()
    {
        if (gameObject.active)
        {
            StartCoroutine(ResetAnimator());
        }
    }

    IEnumerator ResetAnimator()
    {
        yield return new WaitForSeconds(1f);
        animator.Play("Normal");
    }
}
