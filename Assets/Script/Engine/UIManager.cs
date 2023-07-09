using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance{ get; private set; } // 싱글톤

    [SerializeField]
    public Slider healthBar;
    public Slider expBar;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;
    public GameObject gameOverUI;
    public List<GameObject> otherUIs = new List<GameObject>(); // 게임 오버가 되면 꺼져야하는 나머지 UI
    public GameObject levelUpUI; // 레벨업 선택지 UI
    public List<Transform> roundUIElementsPositions; // UI 요소의 위치 리스트
    public GameObject filledFrame; // 색칠된 UI 프레임
    
    
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
    }
    public void OnLevelUpOptionSelected(int option)
    {
        Time.timeScale = 1; // 게임 재개
        levelUpUI.SetActive(false); // 레벨업 선택지 UI 비활성화

        switch (option)
        {
            case 0: 
                // 첫 번째 선택지: 무기 공격력 높이기
                Player.Instance.IncreaseWeaponPower();
                break;
            case 1: 
                // 두 번째 선택지: 공격 속도 높이기
                Player.Instance.IncreaseAttackSpeed();
                break;
            case 2: 
                // 세 번째 선택지: 동일한 무기의 수 늘리기
                Player.Instance.IncreaseSameWeaponCount();
                break;
        }
    }
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        Debug.Log("UpdateHealthBar");
        healthBar.value = currentHealth / maxHealth;
    }
    public void UpdateExpBar(float currentExp, float maxExp)
    {
        Debug.Log("UpdateExpBar");
        expBar.value = currentExp / maxExp;
        expText.text = currentExp + " / " + maxExp;
    }
    public void UpdateLevelText(int level)
    {
        Debug.Log("UpdateLevelText");
        levelText.text = level.ToString();
        Time.timeScale = 0; // 게임 일시 정지
        levelUpUI.SetActive(true); // 레벨업 선택지 UI 활성화
    }
    public void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
        foreach (GameObject ui in otherUIs)
        {
            ui.SetActive(false);
        }
    }
    public void Restart()
    {
        gameOverUI.SetActive(false);
        GameManager.Instance.RestartGame();
    }
    public void UpdateRoundUI(int currentRound)
    {
        // 현재 라운드에 따라 색칠된 UI를 적절한 위치로 이동
        int index = (currentRound - 1) % roundUIElementsPositions.Count;
        filledFrame.transform.position = roundUIElementsPositions[index].position;
    }
}
