using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float currentHealth = 1000; // 성채의 체력
    public float maxHealth = 1000; // 성채의 최대 체력
    public int level = 1; // 성채의 레벨
    public int experience = 0; // 현재 경험치
    public int experienceToLevelUp = 100; // 레벨업에 필요한 경험치
    public List<MonoBehaviour> weapons;
    public static Player Instance { get; private set; }
    public event Action<float, float> OnHealthChanged; // 체력 변화 이벤트
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        currentHealth = maxHealth;
        weapons = new List<MonoBehaviour>();
        
        // 체력 변화 이벤트에 메서드를 연결
        OnHealthChanged += UIManager.Instance.UpdateHealthBar;
    }
    // 무기를 추가하는 함수
    public void AddWeapon(MonoBehaviour newWeapon)
    {
        if (newWeapon is Weapon)
        {
            weapons.Add(newWeapon);
            Debug.Log("Added weapon: " + newWeapon.name + "!");
        }
        else
        {
            Debug.Log("Failed to add weapon: " + newWeapon.name + " is not a Weapon!");
        }
    }

    // 피해를 입었을 때 호출하는 함수
    public void TakeDamage(float damage)
    {
        // 플레이어가 데미지를 입음
        currentHealth -= damage;

        // 체력이 변했음을 알림
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    // 경험치를 획득하는 함수
    public void GainExperience(int amount)
    {
        experience += amount;
        Debug.Log("Gained " + amount + " experience!");

        // 충분한 경험치를 획득하면 레벨업
        if (experience >= experienceToLevelUp)
        {
            LevelUp();
        }
    }

    // 레벨업을 하면 체력이 증가하고, 무기의 공격력이 증가하는 함수
    private void LevelUp()
    {
        level++;
        maxHealth += 50; // 레벨업 할 때마다 체력이 50 증가
        experience -= experienceToLevelUp; // 레벨업에 필요한 경험치를 소모
        experienceToLevelUp += 100; // 다음 레벨업에 필요한 경험치를 증가
        

        Debug.Log("Leveled up to level " + level + "!");
    }
}
