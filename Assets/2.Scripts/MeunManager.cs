using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))] // 이 스크립트가 붙은 GameObject에 AudioSource가 없으면 자동으로 추가되도록 강제하는 특성(Attribute)
public class MeunManager : MonoBehaviour
{
   public GameObject Meun; //메뉴창
    public AudioClip[] soundFile; //Sound를 담아둘 배열
    public float soundVolume = 0.2f;       // 배경음 볼륨(0.0f ~ 1.0f). Slider값으로부터 갱신됨.
    public bool isSoundMute = false; //배경음 뮤트 설정.

    public float sfxVolume = 0.2f; //효과음 볼륨
    public bool isSfxMute = false; //효과음 뮤트 설정

    public Slider sl; //배경음 슬라이더 UI
    public Toggle tg;//배경음 토글 UI

    public Slider sfxSl; //효과음 볼륨 슬라이더 UI
    public Toggle sfxTg; //효과음 뮤트 토글 UI



    private AudioSource audio; //같은 오브젝트에 붙은 AudioSource 컴포넌트를 캐싱하여 매 프레임 GetComponent 호출을 피함.

    public GameObject Sound; //사운드 설정 창(패널) GameObject. 열고/닫기용으로 SetActive 사용.
    public GameObject PlaySoundBtn; //사운드 패널을 여는 버튼(예: 스피커 아이콘).
    public GameObject MainMeun;  //메뉴창

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
     
    }
    // Start is called before the first frame update
    void Start()
    {
        Meun.SetActive(false); //메뉴창 비활성화
      
        soundVolume = 0.2f;    // 예: 0.0 ~ 1.0
        isSoundMute = tg.isOn;     // 예: true면 음소거, false면 소리 켬

        sfxVolume = 0.2f;    // 예: 0.0 ~ 1.0
        isSfxMute = sfxTg.isOn;     // 예: true면 음소거, false면 소리 켬

        PlaySoundBtn.SetActive(true); //사운드 버튼 활성화

        AudioSet(); //현재 설정값을 실제 AudioSource에 반영(볼륨/뮤트 적용)
    }
    public void SetSound()
    {
        soundVolume = sl.value;//슬라이더 값으로 볼륨 갱신
        isSoundMute = tg.isOn; //토글 값으로 뮤트 갱신
        AudioSet(); //현재 설정값을 실제 AudioSource에 반영(볼륨/뮤트 적용)
    }
    public void AudioSet()
    {
        audio.volume = soundVolume; //볼륨 적용
        audio.mute = isSoundMute; //뮤트 적용
    }
    public void SfxAudio()
    {
        sfxVolume = sfxSl.value; //슬라이더 값으로 볼륨 갱신
        isSfxMute = sfxTg.isOn; //토글 값으로 뮤트 갱신
    }
  
    public void SoundUiopen()
    {
        Sound.SetActive(true); //사운드 설정 창 활성화
        MainMeun.SetActive(false);//메뉴창 비활성화


    }
    public void SoundUiSaveclose()
    {
        Sound.SetActive(false); //사운드 설정 창 비활성화
        AudioSet(); //현재 설정값을 실제 AudioSource에 반영(볼륨/뮤트 적용)
        MainMeun.SetActive(true);//메뉴창 비활성화
    }
    public void SoundUiclose()
    {
        Sound.SetActive(false); //사운드 설정 창 비활성화
        MainMeun.SetActive(true);//메뉴창 비활성화
    }
    public void PlayBackground(int type)
    {
        audio.clip = soundFile[type - 1]; //배열에서 type-1 인덱스의 오디오 클립을 가져옴
        audio.loop = true; //배경음은 반복 재생
        audio.spatialBlend = 1.0f; //3D 사운드로 설정
        AudioSet(); //현재 설정값을 실제 AudioSource에 반영(볼륨/뮤트 적용)
        
        audio.Play(); //배경음 재생 시작
    }
    public void PlayEffct(Vector3 pos,AudioClip sfx)
    {
       if(isSfxMute || sfx ==null) //뮤트 상태이거나 재생할 클립이 없으면 함수 종료
            return;

        GameObject _soundObj =new GameObject("sfx"); //효과음 재생용 임시 오브젝트 생성
        
        _soundObj.transform.position = pos; //오브젝트 위치 설정
        
        AudioSource _audio = _soundObj.AddComponent<AudioSource>(); //오디오 소스 컴포넌트 추가

        _audio.clip = sfx; //재생할 효과음 클립 설정

        _audio.volume = sfxVolume; //현재 볼륨 설정
        _audio.spatialBlend= 1.0f; //3D 사운드로 설정
        _audio.minDistance= 15.0f; //최소 거리 설정
        _audio.maxDistance= 50.0f; //최대 거리 설정

        _audio.Play(); //효과음 재생 시작

        Destroy(_soundObj, sfx.length); //효과음 길이 후에 임시 오브젝트 제거
    }
    //게임으로 돌아가기 버튼
    public void ResumeBtn()
    {
        MainMeun.SetActive(false); //재시작 버튼 비활성화
    }
    //게임 종료 버튼
    public void ExitGame()
    {
     UnityEditor.EditorApplication.isPlaying = false; //에디터 모드에서는 재생 중지
     Application.Quit(); //빌드된 게임에서는 애플리케이션 종료
    }
}
