using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;  // 이 슬라이더는 체력을 표시하는 UI 요소입니다.
    // Slider 컴포넌트는 Unity의 Inspector에서 설정하고 연결해야 합니다.

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
