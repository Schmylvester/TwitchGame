using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationController : MonoBehaviour
{
    [SerializeField] Animator m_animator;
    void Update()
    {
        for (KeyCode i = KeyCode.Alpha1; i < KeyCode.Alpha6; ++i)
        {
            if (Input.GetKeyDown(i))
            {
                m_animator.SetInteger("animState", (int)i - 49);
            }
        }
    }
}
