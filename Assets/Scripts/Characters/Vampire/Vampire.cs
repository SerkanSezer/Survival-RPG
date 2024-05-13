using UnityEngine;
using UnityEngine.AI;

public class Vampire : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private NavMeshPath path;
    private VampireAnimation vampireAnimation;
    private State state;
    private const int ATTACK_DISTANCE = 2;
    private float attackTimer = 3;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        vampireAnimation = GetComponent<VampireAnimation>();
        state = State.Chase;
    }

    void Update() {
        switch (state) {
            case State.Chase:
                vampireAnimation.AnimateRun();
                
                if (NavMesh.CalculatePath(transform.position, PlayerInteract.instance.transform.position,NavMesh.AllAreas, path)) {
                    if (path.status == NavMeshPathStatus.PathComplete) {
                        navMeshAgent.SetDestination(PlayerInteract.instance.transform.position);
                    }else {
                        state = State.Idle;
                        navMeshAgent.isStopped = true;
                        vampireAnimation.AnimateIdle();
                    }
                }else {
                    state = State.Idle;
                    navMeshAgent.isStopped = true;
                    vampireAnimation.AnimateIdle();
                }
                
                if (Vector3.Distance(transform.position,PlayerInteract.instance.transform.position) < ATTACK_DISTANCE) {
                    state = State.Attack;
                    navMeshAgent.isStopped = true;
                    vampireAnimation.AnimateIdle();
                }
                break;
            case State.Attack:
                transform.forward = (PlayerInteract.instance.transform.position - transform.position).normalized;
                Attack();
                if (Vector3.Distance(transform.position, PlayerInteract.instance.transform.position) > ATTACK_DISTANCE) {
                    state = State.Chase;
                    navMeshAgent.isStopped = false;
                    vampireAnimation.AnimateIdle();
                }
                break;
            case State.Idle:
                if (NavMesh.CalculatePath(transform.position, PlayerInteract.instance.transform.position, NavMesh.AllAreas, path)) {
                    if (path.status == NavMeshPathStatus.PathComplete) {
                        if (Vector3.Distance(transform.position, PlayerInteract.instance.transform.position) > ATTACK_DISTANCE) {
                            state = State.Chase;
                            navMeshAgent.isStopped = false;
                            navMeshAgent.SetDestination(PlayerInteract.instance.transform.position);
                        }else {
                            vampireAnimation.AnimateIdle();
                            state = State.Attack;
                        }
                    }
                }
                break;
            case State.Dead:
                navMeshAgent.isStopped = true;
                break;
        }
    }

    public void Attack() {
        attackTimer += Time.deltaTime;
        if (attackTimer > 3) {
            vampireAnimation.AnimateAttack();
            attackTimer = 0;
        }
    }
    public void Dead() {
        state = State.Dead;
    }
}
public enum State {
    Idle,
    Chase,
    Attack,
    Dead
}
