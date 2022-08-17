using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodState : StateMachineBehaviour
{    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var spawner = animator.GetComponent<FloodController>();
        spawner.StartFlooding();
    }
}
