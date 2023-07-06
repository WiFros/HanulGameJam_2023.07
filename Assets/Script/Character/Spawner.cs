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

    private void Update()
    {
        // 타이머를 증가시킵니다.
        timer += Time.deltaTime;

        // 지정된 시간 간격이 경과하면 적을 생성합니다.
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f; // 타이머를 초기화합니다.
        }
    }

    private void SpawnEnemy()
    {
        // 적을 생성하고, 스포너의 위치에서 시작하도록 설정합니다.
        var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        // 필요한 경우 여기에서 적의 초기화 로직을 추가할 수 있습니다.
        // 예를 들어, 적이 점차 강해지게 하려면 이 시점에서 적의 속성을 증가시킬 수 있습니다.
    }
}
