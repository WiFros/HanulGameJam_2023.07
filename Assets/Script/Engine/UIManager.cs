using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance{ get; private set; } // 싱글톤

    [SerializeField]
    public Slider healthBar; 
    
    private void Awake()
    {
        // 싱글톤 패턴
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // 플레이어의 체력 변화 이벤트를 구독합니다.
        Player.Instance.OnHealthChanged += UpdateHealthBar;
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        Debug.Log("UpdateHealthBar");
        healthBar.value = currentHealth / maxHealth;
    }
}
