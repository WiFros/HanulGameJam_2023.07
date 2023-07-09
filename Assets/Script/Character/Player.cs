using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float currentHealth = 1000; // 성채의 체력
    public float maxHealth = 1000; // 성채의 최대 체력
    public int level = 1; // 성채의 레벨
    public float experience = 0; // 현재 경험치
    public int experienceToLevelUp = 100; // 레벨업에 필요한 경험치
    public MonoBehaviour bowPrefab;
    public MonoBehaviour cannonPrefab;
    
    public List<MonoBehaviour> weapons;
    public static Player Instance { get; private set; }
    public event Action<float, float> OnHealthChanged; // 체력 변화 이벤트
    public event Action<float, float> OnExpChanged; // 경험치 변화 이벤트
    public event Action<int> OnLevelChanged; // 레벨 변화 이벤트
    
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
        
        // 체력 변화 이벤트에 메서드를 연결
        OnHealthChanged += UIManager.Instance.UpdateHealthBar;
        OnExpChanged += UIManager.Instance.UpdateExpBar;
        OnLevelChanged += UIManager.Instance.UpdateLevelText;
    }
    // 무기를 추가하는 함수
    public void AddWeapon(MonoBehaviour newWeapon)
    {
        Debug.Log("Trying to add weapon: " + newWeapon.name);

        if (newWeapon is Weapon)
        {
            weapons.Add((Weapon)newWeapon);
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
            GameManager.Instance.GameOver();
        }
    }

    // 경험치를 획득하는 함수
    public void GainExperience(float amount)
    {
        OnExpChanged?.Invoke(experience, experienceToLevelUp);
        experience += amount;
        Debug.Log("Gained " + amount + " experience!");

        // 충분한 경험치를 획득하면 레벨업
        if (experience >= experienceToLevelUp)
        {
            experience -= experienceToLevelUp;
            LevelUp();
        }
    }

    // 레벨업을 하면 체력이 증가하고, 무기의 공격력이 증가하는 함수
    private void LevelUp()
    {
        level++;
        OnLevelChanged?.Invoke(level);
        maxHealth += 100; // 레벨업 할 때마다 체력이 50 증가
        experienceToLevelUp += 100*GameManager.Instance.round; // 다음 레벨업에 필요한 경험치를 증가
        

        Debug.Log("Leveled up to level " + level + "!");
    }

    public void IncreaseWeaponPower()
    {
        Debug.Log("IncreaseWeaponPower");
        foreach (Weapon weapon in weapons)
        {
            weapon.IncreasePower();
            Debug.Log("IncreaseWeaponPower : "+ weapon.name);
        }
    }

    public void IncreaseAttackSpeed()
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.IncreaseAttackSpeed();
        }
    }

    public void IncreaseSameWeaponCount()
    {
        if (weapons.Count > 0)
        {
            MonoBehaviour newWeaponPrefab = UnityEngine.Random.value < 0.5f ? bowPrefab : cannonPrefab;
            MonoBehaviour newWeapon = Instantiate(newWeaponPrefab);
            AddWeapon(newWeapon);
        }
    }
    
}
