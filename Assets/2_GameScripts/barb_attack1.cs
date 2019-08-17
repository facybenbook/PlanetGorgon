using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barb_attack1 : MonoBehaviour
{
  public Animator anim;

  void Start()
  {
  anim = GetComponent<Animator>();
  }

  void Update()
  {
    if (Input.GetKeyDown (KeyCode.Mouse0))
      {
        		if (!Input.GetKey(KeyCode.LeftAlt)){anim.Play("standing_melee_attack_downward");}
      }
    }
}
