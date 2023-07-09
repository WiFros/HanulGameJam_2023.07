using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float attackPower;
    public float defensePower;
    public float attackSpeed;
    public float moveSpeed;
    public float expValue;
    public bool isBoss = false;
    public float currentHealth;
    public GameObject healthBarPrefab;
    public GameObject bossHealthBarPrefab;
    
    private GameObject healthBarUI;
    

    private void Start()
    {
        EnemyManager.Instance.enemyCountPerRound--;
        EnemyManager.Instance.AddEnemy(this);
        currentHealth = maxHealth;
        healthBarUI = Instantiate(healthBarPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
        UIManager.Instance.otherUIs.Add(healthBarUI);
        healthBarUI.GetComponent<HealthBar>().SetMaxHealth(maxHealth);
    }
    public void changeHealthBar()
    {
        healthBarUI = Instantiate(bossHealthBarPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
        UIManager.Instance.otherUIs.Add(healthBarUI);
        healthBarUI.GetComponent<HealthBar>().SetMaxHealth(maxHealth);
    }
    private void Update()
    {
        // 성채를 향해 이동
        MoveTowardsCastle();
        healthBarUI.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);
        healthBarUI.GetComponent<HealthBar>().SetHealth(currentHealth);
    }

    public void TakeDamage(float damage)
    {
        // 방어력을 계산하여 실제로 입는 데미지를 조절
        float actualDamage = damage - defensePower;
        actualDamage = Mathf.Max(actualDamage, 0); // 데미지가 0보다 작아지지 않도록

        currentHealth -= actualDamage;
        healthBarUI.GetComponent<HealthBar>().SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            // 적을 제거하고 경험치를 플레이어에게 부여
            Destroy(gameObject);
            Player.Instance.GainExperience(expValue);
        }
    }

    private void MoveTowardsCastle()
    {
        // 성채의 위치를 찾아 이동
        GameObject castle = GameObject.FindWithTag("Castle");

        // 성채를 못 찾으면 이동하지 않음
        if (castle == null)
            return;

        // 성채를 향한 방향 벡터를 계산
        Vector3 direction = (castle.transform.position - transform.position).normalized;

        // 이 방향으로 이동
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered!");
        if (other.gameObject.CompareTag("Castle"))
        {
            // 성채에 도착하면 공격
            StartCoroutine(AttackCastle());
        }
    }

    private IEnumerator AttackCastle()
    {
        while (true)
        {
            // 공격 속도만큼 대기
            yield return new WaitForSeconds(1 / attackSpeed);
        
            // 성채 공격
            Player.Instance.TakeDamage(attackPower);
        }
    }
    private void OnDestroy()
    {
        // 적이 파괴될 때 체력바 UI도 파괴
        EnemyManager.Instance.RemoveEnemy(this);
        UIManager.Instance.otherUIs.Remove(healthBarUI);
        Destroy(healthBarUI);
    }

}
