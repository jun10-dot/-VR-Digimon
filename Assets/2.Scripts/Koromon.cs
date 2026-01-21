using UnityEngine;

/// <summary>
/// Pet 추상 클래스를 상속받는 코로몬 구체 클래스
/// 코로몬 머티리얼 변경 및 수면 자세를 정의합니다.
/// </summary>

public class Koromon : Pet
{
    // 코로몬의 외형(눈) 변경을 담당하는 컴포넌트
    private KoromonChangeMat koromonMat; // 코로몬 전용 머티리얼 클래스

    public override void Awake()
    {
        base.Awake();
        // 코로몬 전용 컴포넌트 캐싱
        koromonMat = GetComponent<KoromonChangeMat>();
    }
    // Pet부모 클래스의 Sleep() 로직 호출 전, 코로몬에게 적합한 수면 각도를 설정
    public override void SleepPosition()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    // Pet부모 클래스의 WakeUp 코루틴에서 호출
    // 머티리얼 변경 함수 호출
    public override void GetUp()
    {
        koromonMat.IdleMatChanged();
    }
}
