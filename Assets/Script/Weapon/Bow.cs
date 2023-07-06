using UnityEngine;

public class Bow : Weapon
{
    public LayerMask enemyLayer; // 적들이 속해 있는 레이어

    private GameObject arrowPrefab; // 발사할 화살의 프리팹
    protected override void Start()
    {
        base.Start();
        lastAttackTime = -attackSpeed; // 처음에는 곧바로 공격할 수 있도록 설정
    }
    
    protected void Update()
    {

        if (Time.time >= lastAttackTime + attackSpeed)
        {
            // 가장 가까운 적 찾기
            Enemy closestEnemy = FindClosestEnemy();

            // 가장 가까운 적이 공격 범위 내에 있다면 공격
            if (closestEnemy != null && Vector3.Distance(transform.position, closestEnemy.transform.position) <= attackDistance)
            {
                Attack(closestEnemy);
                lastAttackTime = Time.time;
            }
        }
    }
    
    protected Enemy FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemyObj in enemies)
        {
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    public override void Attack(Enemy enemy) // 파라미터를 가진 Attack 메서드 구현
    {
        var arrowInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Arrow arrow = arrowInstance.GetComponent<Arrow>();
        arrow.Initialize(projectileSpeed, damage, enemy);
    }
}