using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float damage;
    public float range;
    public float attackDistance;
    public float attackSpeed;
    public float projectileSpeed;
    protected Enemy target; // 공격 대상
    public AttackPriority priority;
    public GameObject projectilePrefab;
    protected float lastAttackTime; // 마지막 공격 시간
    protected virtual void Start()
    {
        lastAttackTime = -attackSpeed; // 처음에는 곧바로 공격할 수 있도록 설정
    }

    public abstract void Attack(Enemy enemy); // 파라미터가 없는 Attack 메서드
}

