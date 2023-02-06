using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCooldown : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameData.Data.ItemCooldown = true;
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameData.Data.ItemCooldown = false;
    }
}
