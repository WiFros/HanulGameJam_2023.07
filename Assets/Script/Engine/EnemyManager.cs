using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    public List<Enemy> enemies; // 적의 목록
    public int enemyCountPerRound = 100; // 각 라운드마다 몇 마리의 적이 출현하는지 결정
    public float enemyStrengthMultiplierPerRound = 1.2f; // 각 라운드마다 적의 강도가 얼마나 증가하는지를 결정하는 배수
    public bool bossSpawned; // 보스가 스폰되었는지 여부
    public GameObject bossHealthBar;
    public Transform[] spawnPoints;
    public Dictionary<int, Enemy> enemyPrefabsPerRound;
    
    public void Update()
    {
        if(enemyCountPerRound <= 0)
        {
            GameManager.Instance.NextRound();
            StrengthenEnemies();
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            enemies = new List<Enemy>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void AddEnemy(Enemy enemy)
    {
        // 새로운 적을 목록에 추가합니다.
        enemies.Add(enemy);
    }
    public void AddBoss(Enemy boss)
    {
        enemies.Add(boss);
        bossSpawned = true;
    }
    public void RemoveEnemy(Enemy enemy)
    {
        // 적을 목록에서 제거합니다.
        enemies.Remove(enemy);

        // 적이 제거되었을 때 경험치 증가
        Player.Instance.experience += enemy.expValue;

        // 모든 적이 제거되었으면 다음 라운드로 이동
        if (enemies.Count == 0)
        {
            Debug.Log(enemies.Count);
            GameManager.Instance.NextRound();
        }
    }

    public void StrengthenEnemies()
    { 
        enemyCountPerRound += 100;
        // 각 라운드마다 적들이 강해집니다. 이 메서드는 GameManager에서 라운드가 증가할 때 호출되어야 합니다.
        foreach (Enemy enemy in enemies)
        {
            enemy.maxHealth *= enemyStrengthMultiplierPerRound;
            enemy.attackPower *= enemyStrengthMultiplierPerRound;
            enemy.expValue *= enemyStrengthMultiplierPerRound;
        }
    }

}
