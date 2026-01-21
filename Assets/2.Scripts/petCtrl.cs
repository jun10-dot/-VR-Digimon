using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class petCtrl : MonoBehaviour
{
    [SerializeField]
    private float hp;
    [SerializeField]
    private float hunger;
    [SerializeField]
    private float affinity;

    private Scrollbar hpScroll;
    private Scrollbar hungerScroll;
    private Scrollbar affinityScroll;

    private bool isCool;
    private float coolTime;
    private float perSec;

    private float timer;
    private float delay;
    [HideInInspector]
    public bool sleep;

    public GameObject agumon;
    public GameObject koromon;
    public GameObject currentState;
    private Transform pos;
    private Animator anim;
    private Coroutine coroutine;
    public GameObject icon;

    public void Awake()
    {
        hpScroll = GameObject.FindGameObjectWithTag("hp").GetComponent<Scrollbar>();
        hungerScroll = GameObject.FindGameObjectWithTag("hunger").GetComponent<Scrollbar>();
        affinityScroll = GameObject.FindGameObjectWithTag("affinity").GetComponent<Scrollbar>();

        anim = currentState.GetComponent<Animator>();
        pos = currentState.GetComponent<Transform>();
        icon = GameObject.FindGameObjectWithTag("icon");
        hp = 0.3f;
        hunger = 0.3f;
        affinity = 0.0f;
    }

    void Start()
    {
        coolTime = 5.0f;
        perSec = 5.0f;
        coroutine = null;

        timer = 0.0f;
        delay = 60.0f;
        sleep = false;
    }

    void Update()
    {
        StartCoroutine("GettingHunger");
        timer += Time.deltaTime;
        if (timer > delay)
        {
            if (hunger <= 0)
            {
                hp -= 0.1f;
            }
            else
            {
                hunger -= 0.1f;
            }
            timer = 0.0f;
        }
        hpScroll.size = hp;
        hungerScroll.size = hunger;
        affinityScroll.size = affinity;

        if (affinity >= 1.0f && currentState == koromon)
        {
            // 진화
            agumon.SetActive(true);
            agumon.GetComponent<Transform>().position = pos.position;
            currentState = agumon;
            agumon.GetComponent<petCtrl>().hp = hp;
            agumon.GetComponent<petCtrl>().hunger = hunger;
            agumon.GetComponent<petCtrl>().affinity = affinity;
            agumon.GetComponent<Animator>().SetTrigger("Evolution");

            icon.SendMessage("ChangeIcon");

            koromon.SetActive(false);
        }
    }

    public void PlayCo()
    {
        StartCoroutine("Play");
    }

    public void SleepCo()
    {
        sleep = true;
        StartCoroutine("Sleep");
    }

    public IEnumerator Sleep()
    {
        hp = 1.0f;
        hunger -= 0.2f;
        GetComponent<Pet>().Target = GameObject.FindGameObjectWithTag("Bed").transform;

        yield return new WaitForSeconds(8.0f);
        icon.GetComponent<IconCtrl>().quads[3].SetActive(true);
        StopCoroutine("Sleep");
    }

    public void PatPat()
    {
        affinity += 0.3f;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "food")
        {
            if (currentState == koromon)
            {
                icon.GetComponent<IconCtrl>().quads[0].SetActive(true);
            }
            else if (currentState == agumon)
            {
                icon.GetComponent<IconCtrl>().quads[1].SetActive(true);
            }
            Destroy(col.gameObject);
            StartCoroutine("Eat");
        }
    }

    public IEnumerator Eat()
    {
        anim.SetTrigger("Eat");
        hp += 0.3f;
        hunger += 0.3f;
        affinity += 0.2f;

        yield return new WaitForSeconds(5.0f);
        icon.GetComponent<IconCtrl>().quads[0].SetActive(false);
        icon.GetComponent<IconCtrl>().quads[1].SetActive(false);
        anim.SetTrigger("Idle");
        StopCoroutine("Eat");
    }

    public IEnumerator Play()
    {
        hp -= 0.1f;
        hunger -= 0.1f;
        affinity += 0.1f;

        GetComponent<Pet>().Target = GameObject.FindGameObjectWithTag("Soccer").transform;

        yield return new WaitForSeconds(10.0f);
        anim.SetTrigger("Idle");
        StopCoroutine("Play");
    }
    IEnumerator GettingHunger()
    {
        yield return new WaitForSeconds(60.0f);
        if (hunger <= 0.1f)
        {
            hp -= 0.1f;
            StopCoroutine("GettingHunger");
        }
        else
        {
            hunger -= 0.1f;
            StopCoroutine("GettingHunger");
        }
    }

    public IEnumerator PetHold()
    {


        isCool = true;
        affinity += perSec * Time.deltaTime;


        if (affinity > 1.0f)
            affinity = 1.0f;
        else if (affinity < 0.0f)
            affinity = 0.0f;

        yield return new WaitForSeconds(coolTime);

        isCool = false;
        coroutine = null;
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Hand")
        {

            if (isCool) return;

            if (coroutine == null)
            StartCoroutine(PetHold());
           

        }
    }
}

// 이벤트 상호작용시 프로필 뜨도록 -> 머리 위에 상태 띄우기 (프로필 띄우기)
// 메뉴 scene에서 ui가려지는거 ui camera 적용해보고 안되면 다른 방법 찾기
// 쓰다듬기 -> 코로몬의 경우 들어서 direct로 쓰다듬어 보자 (보류)

// idle?? -> 잠자는거랑 산책하는거에 시간주고 시간 지나면 자동으로 idle 돌아오도록
