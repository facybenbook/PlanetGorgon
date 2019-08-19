using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barb_animation : MonoBehaviour
{
    Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        //Get the Animator attached to the GameObject you are intending to animate.
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.W))
        //{
        //}
        //else
        //{
        //    m_Animator.Play("Better_Idle");
        //}

    }
}
