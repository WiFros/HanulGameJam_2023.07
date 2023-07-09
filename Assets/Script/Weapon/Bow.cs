using System.Collections.Generic;
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
            Enemy closestEnemy = FindRandomEnemyInRange();

            // 가장 가까운 적이 공격 범위 내에 있다면 공격
            if (closestEnemy != null && Vector3.Distance(transform.position, closestEnemy.transform.position) <= attackDistance)
            {
                Attack(closestEnemy);
                lastAttackTime = Time.time;
            }
        }
    }
    
    protected Enemy FindRandomEnemyInRange(float closestEnemyChance = 0.5f)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<Enemy> enemiesInRange = new List<Enemy>();
        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemyObj in enemies)
        {
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            // 공격 범위 내의 적들을 리스트에 추가합니다.
            if (distance <= attackDistance)
            {
                enemiesInRange.Add(enemy);

                // 가장 가까운 적을 찾습니다.
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }

        // 범위 내에 적이 없다면 null을 반환합니다.
        if (enemiesInRange.Count == 0)
        {
            return null;
        }

        // 일정 확률로 가장 가까운 적을 선택하고, 그 외의 경우에는 무작위 적을 선택합니다.
        if (UnityEngine.Random.value < closestEnemyChance)
        {
            return closestEnemy;
        }
        else
        {
            int randomIndex = UnityEngine.Random.Range(0, enemiesInRange.Count);
            return enemiesInRange[randomIndex];
        }
    }



    public override void Attack(Enemy enemy) // 파라미터를 가진 Attack 메서드 구현
    {
        var arrowInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Arrow arrow = arrowInstance.GetComponent<Arrow>();
        arrow.Initialize(projectileSpeed, damage, enemy);
    }
}