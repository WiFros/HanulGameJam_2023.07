using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Enemy target; // 화살의 타겟
    private float speed; // 화살의 속도
    private float damage; // 화살이 주는 피해

    // 화살의 속도, 피해, 타겟을 초기화
    public void Initialize(float speed, float damage, Enemy target)
    {
        this.speed = speed;
        this.damage = damage;
        this.target = target;
    }

    void Update()
    {
        if (target == null) // 타겟이 사라졌다면 화살도 사라짐
        {
            Destroy(gameObject);
            return;
        }

        // 타겟을 향해 이동
        Vector3 direction = target.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        
        // 화살이 타겟을 바라보도록 회전
        //transform.rotation = Quaternion.LookRotation(direction);
        
        if (direction.magnitude <= distanceThisFrame)
        {
            // 타겟에 닿으면 피해를 주고 사라짐
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        target.TakeDamage(damage);
        Destroy(gameObject);
    }
}