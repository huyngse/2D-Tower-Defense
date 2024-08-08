using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    public void OpenPortal() {
        animator.SetBool("open", true);
    }
    public void ClosePortal() {
        animator.SetBool("open", false);
    }
}
