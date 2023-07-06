using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public float explosionRange; // 폭발 범위

    private float speed;
    private float damage;
    private Enemy target;
    private Vector3? explosionPoint = null;
    public GameObject hitEffectPrefab; // 이펙트 프리팹
    public void Initialize(float speed, float damage, Enemy target)
    {
        this.speed = speed;
        this.damage = damage;
        this.target = target;
    }

    void Update()
    {
        Vector3 direction;

        if (target == null)
        {
            if (explosionPoint == null)
            {
                Destroy(gameObject);
                return;
            }

            direction = explosionPoint.Value - transform.position;
        }
        else
        {
            direction = target.transform.position - transform.position;

            // 폭발 지점 업데이트
            explosionPoint = target.transform.position;
        }

        float distanceThisFrame = speed * Time.deltaTime;

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);

        if (direction.magnitude <= distanceThisFrame)
        {
            Explode();
        }
    }

    void Explode()
    {
        // 주변의 적 찾기
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, explosionRange);

        GameObject hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        // 각 적에게 데미지 주기
        foreach (var collider in enemiesInRange)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        Destroy(hitEffect, 2f); // 2초 후에 이펙트 파괴
        Destroy(gameObject); // 폭발 후 투사체 파괴
    }
}