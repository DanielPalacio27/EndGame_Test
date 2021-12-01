using UnityEngine;
using UnityEngine.AI;
using Character;

public class AIController : MonoBehaviour
{
    [SerializeField] private AIData enemyData = null;
    private NavMeshAgent navMesh = null;
    private NavMeshPath navMeshPath = null;
    private CharacterAnimation characterAnimation = null;
    public bool canShoot { get; private set; }

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        navMeshPath = new NavMeshPath();
        characterAnimation = GetComponent<CharacterAnimation>();
        InitializeAI();
    }

    private void InitializeAI() => navMesh.speed = enemyData.speed;

    void Update() => AIBehaviour();

    public void AIBehaviour()
    {
        Transform nearestPlayer = FindNearestPlayer();

        if (nearestPlayer != null)
        {
            navMesh.destination = nearestPlayer.position;
            LookAtEnemy(nearestPlayer);
            //If the distance to player is close enough, proceed to shoot him
            if (DistanceToPlayer(nearestPlayer) <= enemyData.distanceToShoot)
            {
                characterAnimation.SetShoot(true);
                if (characterAnimation.CheckForCurrentState("Shooting"))
                {
                    navMesh.isStopped = true;
                    canShoot = true;
                }
            }
        }
        else
        {
            characterAnimation.SetShoot(false);
            canShoot = false;
            navMesh.isStopped = false;
            if (!navMesh.hasPath)
            {
                navMesh.destination = CalculateRandomPos();
            }
        }
        characterAnimation.SetState(navMesh.velocity.normalized.magnitude);
    }

    public void LookAtEnemy(Transform _enemy) => transform.LookAt(_enemy);
    float DistanceToPlayer(Transform _playerTransform) => (transform.position - _playerTransform.position).magnitude;

    Vector3 CalculateRandomPos()
    {
        Vector2 randomInCircle = Random.insideUnitCircle * Random.Range(-50f, 50f);
        Vector3 randomPos = Vector3.left * randomInCircle.x + Vector3.forward * randomInCircle.y;
        randomPos += transform.position;
        return IsValidThePath(randomPos) ? randomPos : CalculateRandomPos();
    }
    bool IsValidThePath(Vector3 endPos)
    {
        navMesh.CalculatePath(endPos, navMeshPath);
        return navMeshPath.status == NavMeshPathStatus.PathComplete ? true : false;
    }

    private Transform FindNearestPlayer()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, enemyData.distanceToDetectEnemy, transform.position, enemyData.distanceToDetectEnemy, enemyData.playerLayer);

        if (hits.Length == 0)
            return null;

        return hits[0].transform;
    }
}
