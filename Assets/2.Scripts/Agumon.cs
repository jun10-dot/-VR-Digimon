using UnityEngine;

/// <summary>
/// Pet 추상 클래스를 상속받는 아구몬 구체 클래스
/// 아구몬 전용 애니메이션, 머티리얼 변경 및 수면 자세를 정의합니다.
/// </summary>
public class Agumon : Pet
{
    // 아구몬의 외형(눈) 변경을 담당하는 컴포넌트
    private AgumonChangeMat agumonChangeMat; // 아구몬 전용 머티리얼 클래스
    public override void Awake()
    {
        base.Awake();
        // 아구몬 전용 컴포넌트 캐싱
        agumonChangeMat = GetComponent<AgumonChangeMat>();
    }

    // Pet부모 클래스의 Sleep() 로직 호출 전, 아구몬에게 적합한 수면 각도를 설정
    public override void SleepPosition()
    {
        transform.rotation = Quaternion.Euler(new Vector3(20f, 0f, 0f));
    }

    // Pet부모 클래스의 WakeUp 코루틴에서 호출
    // 아구몬 전용 애니메이션, 머티리얼 변경 함수 호출
    public override void GetUp()
    {
        anim.SetTrigger("Getup");
        agumonChangeMat.IdleMatChanged();
    }
}