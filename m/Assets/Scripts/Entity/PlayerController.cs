using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : BaseController
{
    private Camera camera;

    private GameManager gameManager;

    private float basicAttackDelay;
    private float flipDelay;
    private float flipTimeCount;

    [SerializeField] private RangeWeaponHandler unEquipWeapon;

    private RangeWeaponHandler unEquipWeapons;

    private EnemyManager enemyManager;
    [SerializeField] private EnemyController target;
    private float unEquipWeaponDelay = 1f;

    private bool isFlip;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        camera=Camera.main;
        basicAttackDelay = 1.5f;
        flipDelay = 2f;
        flipTimeCount = 0f;
        isFlip = false;
        if(unEquipWeapon != null)
        {
            unEquipWeapons = Instantiate(unEquipWeapon, transform);
        }
        enemyManager = GameManager.Instance.GetEnemyManager();
        knockbackDuration = 0;
        target = null;
        StartCoroutine(StartAutoAttack(basicAttackDelay));
        StartCoroutine(SetTarget());
        StartCoroutine(StartUnEquipAttack());
    }

    protected override void Update()
    {
        base.Update();
        flipTimeCount += Time.deltaTime;
    }

    private EnemyController RefreshTarget()
    {
        if(enemyManager.ActiveEnemies.Count==0)
        {
            return null;
        }

        EnemyController nearEnemy = enemyManager.ActiveEnemies.OrderBy(x =>
        Vector2.Distance(transform.position, x.transform.position)).First();
        return nearEnemy;
    }

    protected override void HandleAction()
    {
        float horiaontal = Input.GetAxisRaw("Horizontal");
        float vertical= 0;
        movementDirection=new Vector2(horiaontal, vertical).normalized;

        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        if(lookDirection.magnitude<.9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            Flip();
    }

    protected override void Attack()
    {
        base.Attack();
    }

    private IEnumerator StartUnEquipAttack()
    {
        while(true)
        {
            yield return new WaitWhile( () => TargetCheck());
            Vector2 direction = (Vector2)target.transform.position - (Vector2)transform.position;

            float minAngle = -(unEquipWeapons.NumberofProjectilePerShot / 2f) * unEquipWeapons.MultipleProjectileAngle;

            unEquipWeapons?.CreateProjectile(direction, minAngle + unEquipWeapons.MultipleProjectileAngle);
            yield return new WaitForSeconds(unEquipWeaponDelay);
        }
    }

    private IEnumerator StartAutoAttack(float delay)
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator SetTarget()
    {
        while (true)
        {
            target = RefreshTarget();
            yield return TargetCheck()? null : new WaitUntil(() => TargetCheck() == true);
        }
    }

    private bool TargetCheck()
    {
        if(target==null || target.GetAlive()==false)
        {
            return true;
        }    
        
        return false;
    }

    public override void Death()
    {
        base.Death();
        gameManager.GameOver();
        StopCoroutine(StartAutoAttack(basicAttackDelay));
        StopCoroutine(StartUnEquipAttack());
    }

    private void Flip()
    {
        if(flipTimeCount<flipDelay)
        {
            return;
        }

        isFlip = !isFlip;
        transform.rotation = isFlip? Quaternion.Euler(0, 180, 180): Quaternion.Euler(0, 0, 0);

        _rigidbody.position = new Vector2(transform.position.x, transform.position.y * -1);
        flipTimeCount = 0;
    }


}
