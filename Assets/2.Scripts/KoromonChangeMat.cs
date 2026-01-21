using System.Collections;
using UnityEngine;

/// <summary>
/// 코로몬의 머티리얼(수면, 식사) 변경을 관리하는 클래스
/// 특정 상태(Idle, Sleep)에 따라 메쉬의 머티리얼을 교체합니다.
/// </summary>
public class KoromonChangeMat : MonoBehaviour
{
    public Material eatMat; // 식사 시 사용할 머티리얼
    public GameObject sPhere2; // 코로몬의 외형을 교체하기위한 오브젝트
    private MeshRenderer KoromonMesh;
    private Material koromonMat; // 기본 머티리얼 저장용
    void Awake()
    {
        // 지정된 오브젝트의 MeshRenderer를 찾아서 캐싱
        KoromonMesh = sPhere2.GetComponent<MeshRenderer>();
    }

    // 게임 시작 시 코로몬의 기본 머티리얼 저장
    void Start()
    {
        koromonMat = KoromonMesh.material;   
    }

    // 애니메이션 이벤트 호출
    // 특정 상태(Idle, Move)에서 짧은 순간 동안 외형을 변경(눈 깜빡임)
    public void BlinkEye()
    {
        // 현재 머티리얼을 배열 요소[1]로 교체
        KoromonMesh.material = KoromonMesh.materials[1];
        // 0.2초 후 다시 기본 상태로 복구
        StartCoroutine(QuickyChanged(0.2f));
    }

    IEnumerator QuickyChanged(float time)
    {
        yield return new WaitForSeconds(time);
        KoromonMesh.material = koromonMat;
    }

    // 애니메이션 이벤트 호출
    // 수면 상태 진입 시 지속적인 수면 머티리얼 적용
    public void Sleeping()
    {
        KoromonMesh.material = KoromonMesh.materials[1];
    }

    // 기상 시 기본 머티리얼로 복구
    public void IdleMatChanged()
    {
        KoromonMesh.material = koromonMat;
    }

    // 애니메이션 이벤트 호출
    // 음식을 먹는 순간 외형(입) 변화 연출
    public void EatMatChanged()
    {
        KoromonMesh.material = eatMat;
        StartCoroutine(QuickyChanged(0.2f));
    }
}
