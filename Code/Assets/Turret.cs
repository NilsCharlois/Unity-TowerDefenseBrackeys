using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Turret : MonoBehaviour
{
    private Transform CurrentTarget;
    public float range = 15f;
    public string enemyTag = "enemy";
    public Transform PartToRotate;
    public float rotationSpeed = 10f;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); // to call UpdateTarget every half second
    }

    // To find the nearest enemy
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // enemy in reach
        if(nearestEnemy != null && shortestDistance <= range)
        {
            CurrentTarget = nearestEnemy.transform;
        }
        else
        {
            CurrentTarget = null;
        }
    }

    void Update()
    {
        if(CurrentTarget == null) return;

        // target lock on
        Vector3 direction = CurrentTarget.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;

        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
