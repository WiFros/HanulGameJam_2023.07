using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackType
{
    void ExecuteAttack(Weapon weapon, Transform target);
}

public class SingleTargetAttack : IAttackType
{
    public void ExecuteAttack(Weapon weapon, Transform target)
    {
        // 여기에 단일 목표 공격 코드를 작성합니다.
        // 예: 투사체를 생성하고, target으로 발사합니다.
    }
}

public class AreaAttack : IAttackType
{
    public void ExecuteAttack(Weapon weapon, Transform target)
    {
        // 여기에 영역 공격 코드를 작성합니다.
        // 예: 대포 공을 생성하고, target으로 발사합니다. 이후 대포 공이 폭발하여 주변 적들에게 데미지를 입힙니다.
    }
}
