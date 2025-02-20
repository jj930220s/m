using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    private EnemyManager enemyManager;
    private Transform target;

    private bool isCoroutineRunning;

    [SerializeField] private float followRange = 15f;

    public void Init(EnemyManager enemyManager,Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target;
        isCoroutineRunning = false;
        isAttacking = false;
    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }

    protected override void Attack()
    {
        base.Attack();

    }

    protected override void HandleAction()
    {
        base.HandleAction();

        if(weaponHandler==null || target==null)
        {
            if (!movementDirection.Equals(Vector2.zero))
                movementDirection = Vector2.zero;
            return;
        }

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        if(distance<=followRange)
        {
            lookDirection = direction;

            if(distance<weaponHandler.AttackRange)
            {
                int layerMaskTarget = weaponHandler.target;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction,
                    weaponHandler.AttackRange*1.5f,
                    (1<<LayerMask.NameToLayer("Level"))|layerMaskTarget);

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    if (!isCoroutineRunning)
                    {
                        StartCoroutine(SetAttackDelay(weaponHandler.Delay));
                    }
                }
                movementDirection = Vector2.zero;
                return;
            }
            else
            {
                StopCoroutine(SetAttackDelay(weaponHandler.Delay));
            }

            movementDirection = direction;
        }
    }

    private IEnumerator SetAttackDelay(float attackDelay)
    {
        while(true)
        {
            isAttacking = true;
            isCoroutineRunning = true;
            yield return new WaitForSeconds(attackDelay);
            isCoroutineRunning = false;
        }
    }

    public override void Death()
    {
        base.Death();
        enemyManager.RemoveEnemyOnDeath(this);
    }


}
