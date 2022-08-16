using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeOfOzzy.Components;

public class BossNextStageState : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var spawner = animator.GetComponent<CircularProjectileSpawner>();
        spawner.Stage++;

        var changeLight = animator.GetComponent<ChangeLightsComponent>();
        if (changeLight != null) changeLight.SetColor();
    }
}
