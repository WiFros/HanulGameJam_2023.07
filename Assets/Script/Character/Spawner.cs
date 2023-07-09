using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Enemy enemyPrefab; // 생성할 적 프리팹

    [SerializeField]
    private float spawnInterval = 5f; // 적을 생성할 시간 간격

    private float timer; // 경과 시간을 기록할 타이머
    private int round; // 현재 라운드

    private void Update()
    {
        // 타이머를 증가시킵니다.
        timer += Time.deltaTime;
        round = GameManager.Instance.round;

        // 지정된 시간 간격이 경과하면 적을 생성합니다.
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f; // 타이머를 초기화합니다.
        }
    }

    private void SpawnEnemy()
    {
        // 5의 배수 라운드에서는 보스를 소환합니다.
        if (round % 5 == 0 && !EnemyManager.Instance.bossSpawned)
        {
            Enemy boss = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            EnemyManager.Instance.bossSpawned = true;
            boss.transform.localScale *= 10; // 보스의 크기를 5배로 키웁니다.
            boss.maxHealth *= 100; // 보스의 최대 체력을 5000배로 늘립니다.
            boss.currentHealth = boss.maxHealth; // 보스의 체력을 5000배로 늘립니다.
            boss.attackPower *= 2; // 보스의 공격력을 2배로 늘립니다.
            boss.moveSpeed *= 0.2f; // 보스의 이동 속도를 반으로 줄입니다.
            boss.attackSpeed *= 0.5f; // 보스의 공격 속도를 반으로 줄입니다.
            boss.changeHealthBar();
            boss.defensePower = 5; // 보스의 방어력을 5로 설정합니다.
            EnemyManager.Instance.AddEnemy(boss);
        }
        else if(round % 5 != 0)
        {
            // 일반적인 라운드에서는 여러 적들을 소환합니다.
            Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            EnemyManager.Instance.AddEnemy(enemy);
        }
    }
}
