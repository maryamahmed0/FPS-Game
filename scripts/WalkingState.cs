using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingState : StateMachineBehaviour
{
    float timer;
    List<Transform> wayPoints = new List<Transform>();
    public NavMeshAgent agent;
    float ChasingRange = 8f;
    float attackingRange = 2f;
    GameObject Player;
    Enemy enemy;
    // nStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        agent = animator.GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        agent.speed = 1.5f;
        timer = 0;
        GameObject WayPoint = GameObject.FindWithTag("WayPoints");
        foreach (Transform t in WayPoint.transform)
        {
            wayPoints.Add(t);
        }
        agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
        Player = GameObject.FindWithTag("Player");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);

        timer += Time.deltaTime;
        if (timer > 10)
        {
            animator.SetBool("walking",false);
        }
        float distance = Vector3.Distance(Player.transform.position, animator.transform.position);
        if (distance < ChasingRange || enemy.Hit == true)
        {
            agent.speed = 4f;
            agent.SetDestination(Player.transform.position);
            if (distance < attackingRange)
                animator.SetBool("attacking", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.isStopped = true;
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
