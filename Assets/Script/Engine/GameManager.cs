using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public enum GameState { Playing, GameOver }
    public GameState state = GameState.Playing;
    public int round = 1;

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
    public void GameOver()
    {
        state = GameState.GameOver;
        Time.timeScale = 0;
        UIManager.Instance.ShowGameOverUI();
        Debug.Log("Game Over! Press 'R' to restart.");
    }

    public void RestartGame()
    {
        round = 1;
        Time.timeScale = 1;
        Player.Instance.currentHealth = 1000;
        Player.Instance.maxHealth = 1000;
        Player.Instance.experience = 0;
        
        state = GameState.Playing;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬을 다시 로드하여 게임을 재시작
    }

    public void Lobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
    
    public void NextRound() // 다음 라운드로 이동하는 메서드
    {
        round++;
        UIManager.Instance.UpdateRoundUI(round);
        Debug.Log("Round: " + round);
    }
}