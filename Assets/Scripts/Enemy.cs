using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Range(0, 360)] public float ViewAngle = 90f;          //угол обзора
    public float ViewDistance = 15f;                       //как далеко видит
    public float DetectionDistance = 1.5f;                   //мин. дистанция замеиности
    public Transform EnemyEye;
    public Transform hero;

    [SerializeField] private float changePositionTime = 5f;           //время стояния на точке
    [SerializeField] private float moveDistance = 30f;                //переместить на расстояние

    private NavMeshAgent agent;
    private float rotationSpeed;
    private Transform agentTransform;



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        rotationSpeed = agent.angularSpeed;
        agentTransform = agent.transform;
        InvokeRepeating(nameof(MovePatr), changePositionTime, changePositionTime);
    }
    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(hero.transform.position, agent.transform.position);
        if (distanceToPlayer <= DetectionDistance || IsInView())
        {
            //RotateToTarget();
            MoveToTarget();
        }
    }

    private bool IsInView() // true если цель видна
    {
        float realAngle = Vector3.Angle(EnemyEye.forward, hero.position - EnemyEye.position);
        RaycastHit hit;
        if (Physics.Raycast(EnemyEye.transform.position, hero.position - EnemyEye.position, out hit, ViewDistance))
        {
            if (realAngle < ViewAngle / 2f && Vector3.Distance(EnemyEye.position, hero.position) <= ViewDistance && hit.transform == hero.transform)
            {
                return true;
            }
        }
        return false;
    }

    private Vector3 RandomNavSphere(float distance)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance; //создает точку

        randomDirection += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, -1);  // путь к точке

        return navHit.position;

    }

    private void MovePatr() // движения к точкам
    {
        agent.SetDestination(RandomNavSphere(moveDistance));
    }

    private void MoveToTarget() // движения к игроку
    {
        agent.SetDestination(hero.position);
    }

    private void RotateToTarget() // поворачивает в сторону цели со скоростью
    {
        Vector3 lookVector = hero.position - agentTransform.position;
        lookVector.y = 0;
        if (lookVector == Vector3.zero) return;
        agentTransform.rotation = Quaternion.RotateTowards(agentTransform.rotation, Quaternion.LookRotation(lookVector, Vector3.up), 
           rotationSpeed * Time.deltaTime);
    }

    

    

}
