public enum AttackPriority
{
    Closest, // 가장 가까운 적
    Farthest, // 가장 먼 적
    Middle, // 먼 적과 가까운 적의 중간
    HighPriority // 우선 순위가 높은 적 (보스 - 네임드 - 지원형 - 일반)
}