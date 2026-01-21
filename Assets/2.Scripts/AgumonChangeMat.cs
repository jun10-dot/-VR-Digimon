using UnityEngine;

/// <summary>
///  아구몬의 머티리얼(수면) 변경을 관리하는 클래스
///  특정 상태(Idle, Sleep)에 따라 메쉬의 머티리얼을 교체합니다.
/// </summary>

public class AgumonChangeMat : MonoBehaviour
{
    private SkinnedMeshRenderer AgumonMesh; // 펫의 메쉬와 머티리얼 정보를 담을 변수
    private Material mat; // 기본 머티리얼(눈 뜬 텍스쳐) 저장용
    void Awake()
    {
        // 자식에 붙은 SkinnedMeshRenderer 컴포넌트를 찾아서 캐싱
        AgumonMesh = GetComponentInChildren<SkinnedMeshRenderer>();
    }
    
    // 게임 시작 시 아구몬의 기본 머티리얼 저장
    void Start()
    {
        mat = AgumonMesh.material; 
    }

    // 애니메이션 이벤트 호출
    // 미리 할당된 머티리얼 배열 [1]로 교체 (잠자는 눈)
    public void SkinnedMatChanged()
    {
        AgumonMesh.material = AgumonMesh.materials[1];
    }

    // 기상 시 기본 머티리얼로 복구
    public void IdleMatChanged()
    {
        AgumonMesh.material = mat;
    }
}
