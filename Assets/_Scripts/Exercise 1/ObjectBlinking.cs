using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBlinking : MonoBehaviour
{
    public Transform model;
    private Animator anim;

    private void Start()
    {
        anim = model.GetComponent<Animator>();
    }


    public void BlinkButton ()
    {
        anim.SetTrigger("Blink");
    }
}
