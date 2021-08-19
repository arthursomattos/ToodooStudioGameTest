using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : StateMachineBehaviour
{
    public GameObject NPC;
    public float runRotSpeed = 3.0f;
    public float accuracy = 3.0f;
    public NavMeshAgent agent;
    public GameObject[] waypoints;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        agent = NPC.GetComponent<NavMeshAgent>();
        waypoints = NPC.GetComponent<BaseEnemy>().GetWaypoints();
    }
}
