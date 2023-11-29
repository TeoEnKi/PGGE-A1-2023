using PGGE;
using PGGE.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePlayer : MonoBehaviour
{
    [HideInInspector]
    public FSM mFsm = new FSM();
    public Animator mAnimator;
    public PlayerMovement mPlayerMovement;
    public LayerMask mPlayerMask;

    // Start is called before the first frame update
    void Start()
    {
        PlayerConstants.PlayerMask = mPlayerMask;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            mAnimator.SetBool("Attack",true);
        }
        else
        {
            mAnimator.SetBool("Attack", false);
        }
    }
    public void Move()
    {
        mPlayerMovement.HandleInputs();
        mPlayerMovement.Move();
    }

}
