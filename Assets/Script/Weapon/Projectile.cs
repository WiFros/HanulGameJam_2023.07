using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private Enemy target;
    private float damage;
    public Enemy Target { get; private set; }

    public void SetTarget(Enemy target)
    {
        this.Target = target;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void Update()
    {
        if (target == null)
        {
            // 타겟이 없으면 투사체를 삭제합니다.
            Destroy(gameObject);
        }
        else
        {
            // 타겟이 있으면 타겟의 방향으로 이동합니다.
            var direction = (target.transform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target.gameObject)
        {
            // 타겟에 도달하면 데미지를 입히고 투사체를 삭제합니다.
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
