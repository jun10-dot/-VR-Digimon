using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
///  펫 캐릭터의 공통 로직을 담고 있는 추상 클래스
///  
///  상속을 통해 중복 코드 방지 및 확장성을 고려하여 설계되었습니다.
/// </summary>

public abstract class Pet : MonoBehaviour
{
    // 컴포넌트 캐싱
    protected Animator anim;
    private NavMeshAgent agent;
    private Rigidbody rb;

    private Transform target; // 추적할 대상
    private Transform sunBed; // 잠을 잘 침대 위치

    private RigidbodyConstraints rbConstraint; // 수면중 외부충격 제어용
    private Vector3 pos; // 수면 직전 위치 저장용

    private float hp; // 체력
    private float hungry; // 배고픔
    private float traceDist = 100; // 추적 유효 거리
    private int wakeUpDelay = 8; // 시간 간격 
    private bool isStop; // 침대와 상호작용중이라면 NavMeshAgent 행동 중단

#region Property
    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }
    public float TraceDist
    {
        get { return traceDist; }
        private set { traceDist = value; }
    }
    public float Hungry
    {
        get { return hungry; }
        set { hungry = value; }
    }
    public float Hp
    {
        get { return hp; }
        set { hp = value; }
    }
#endregion

    public virtual void Awake()
    {
        // 컴포넌트 캐싱
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }
    public virtual void Start()
    {
        rbConstraint = rb.constraints;
    }

    public virtual void Update()
    {
        Move();
    }

    // 대기(Idle), 이동(Move)을 포함한 함수
    public void Move()
    {
        // 타겟이 없으면 대기(Idle) 상태
        if (target == null) 
        {
            anim.SetTrigger("Idle");
            agent.isStopped = true;
            return;
        }
       // 타겟과의 거리 계산 후 이동(Move) 판단
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist <= TraceDist)
        {
            if (isStop) // 잠을 자는중이라면 중단 
                return;
            
            // 추적 범위 내에 있으면 타겟으로 이동
            agent.isStopped = false;
            agent.SetDestination(target.position);
            anim.SetTrigger("Move");
        }
        else
        {
            // 추적 범위를 벗어나면 정지 (Idle)
            anim.SetTrigger("Idle");
            agent.isStopped = true;
        }
    }

    // 침대 충돌 시 수면 로직 실행
    public void OnTriggerEnter(Collider coll)
    {
        // 침대 태그 및 PetCtrl 수면 상태 체크 
        // petCtrl 컴포넌트는 팀원이 담당한 스크립트 (협업)
        if (coll.gameObject.CompareTag("Bed") && GetComponent<petCtrl>().sleep)
        {
            isStop = true;
            sunBed = coll.gameObject.transform.GetChild(1); // 침대 특정 위치(자식 오브젝트) 가져오기
            SleepPosition();
            Sleep();
            // wakeUpDelay 후 기상 처리
            StartCoroutine(WakeUp(wakeUpDelay));
        }
    }

    // 수면 상태로의 전환을 처리하는 메서드
    public virtual void Sleep()
    {
        anim.SetTrigger("Sleep"); // 수면 애니메이션 실행
        rb.constraints = RigidbodyConstraints.FreezeAll; // 수면 중 외부충격 혹은 중력에 의해 펫이 밀려나는 현상을 방지
        agent.enabled = false; // 수면 중 agent가 다시 경로 계산하며 위치를 보정하지 않도록 완전히 비활성화
        pos = transform.position; // 현재 위치 백업
        transform.position = sunBed.position; // 침대 위 특정 좌표로 전환
    }

    // 일정 시간 후 펫을 다시 활성화 상태로 전환
    public IEnumerator WakeUp(int delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<petCtrl>().sleep = false; // petCtrl 컴포넌트는 팀원이 담당한 스크립트 (협업)
        GetUp();
        anim.SetTrigger("Idle");
        agent.enabled = true; // 내비게이션 재활성화
        transform.position = pos; // 잠들기전 위치로 복귀
        rb.constraints = rbConstraint;
        isStop = false;
        target = null; // 타겟 초기화 (새로운 명령 대기)
    }

    // 펫마다 수면 위치 미세 조정
    public abstract void SleepPosition();
    
    // 기상 시 펫마다 개별 동작 
    public abstract void GetUp();
    
}
