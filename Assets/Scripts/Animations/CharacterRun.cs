using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRun : StateMachineBehaviour
{
    [SerializeField]
    private float _jumpHeight = 0.3f;

    [SerializeField]
    private float _jumpSpeed = 6f;

    private float _multiplier = 0f;
    private Transform _transform = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;
        _multiplier = _jumpSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform.localPosition = new Vector3(
            _transform.localPosition.x, 
            _transform.localPosition.y, 
            _transform.localPosition.z + (_multiplier * Time.deltaTime)
        );

        if (_transform.localPosition.z > _jumpHeight)
            _multiplier = -_jumpSpeed;
        else if (_transform.localPosition.z < 0f)
            _multiplier = _jumpSpeed;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       _transform.localPosition = new Vector3(
           _transform.localPosition.x, 
           _transform.localPosition.y, 
           0f
       );
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
