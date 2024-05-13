using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    private float searchRadius = 200;
    private Vector3 spawnPos;
    private int playTime;
    private int spawnTime;
    private const int SPAWN_TIME_PERIOD = 10;
    private NavMeshPath path;
    [SerializeField] private Transform pfVampire;
    private void Start() {
        path = new NavMeshPath();
        playTime = playTime = TimeSaveManager.Load();
    }

    void Update()
    {
        spawnTime += (int)Time.deltaTime;
        playTime += (int)Time.deltaTime;
        if (playTime / 60 > 30 && spawnTime / 60 > SPAWN_TIME_PERIOD) {
            spawnTime = 0;
            spawnPos = Vector3.zero;
            for (int i = 0; i < 20; i++) {
                spawnPos = RandomNavmeshLocation(searchRadius);
                if (spawnPos != Vector3.zero)
                    break;
            }
            Instantiate(pfVampire, spawnPos, Quaternion.identity);
        }
    }

    public Vector3 RandomNavmeshLocation(float radius) {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += PlayerInteract.instance.transform.position;
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius*1.1f,1)) {
            if (NavMesh.CalculatePath(hit.position,PlayerInteract.instance.transform.position,1, path)) {
                if (path.status == NavMeshPathStatus.PathComplete) {
                    return hit.position;
                }
            }
        }
        return Vector3.zero;
    }
}
