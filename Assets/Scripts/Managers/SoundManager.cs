using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgmVolum;
    private AudioSource bgmPlayer;
	private AudioHighPassFilter bgmEffect;
	
	[Header("SFX")]
	public AudioClip[] sfxClips;
	public float sfxVolum;
	public int channels;
	private AudioSource[] sfxPlayers;
	private int channelIndex;

	[Header("Volum Slider")]
	[SerializeField] Slider volumSlider;

	public enum Sfx { Dead, Hit, LevelUp=3, Lose, Melee, Range=7, Select, Win}

	private void tart()
	{
		if (!PlayerPrefs.HasKey("musicVolume"))
		{
			PlayerPrefs.SetFloat("musicVolume", 1);
			Load();
		}
		else
		{
			Load();
		}
	}

	private void Awake()
	{
		//SoundInit();
	}

	//private void SoundInit()
	//{
	//	// 배경음 플레이어 초기화
	//	GameObject bgmObject = new GameObject("BgmPlayer");
	//	bgmObject.transform.parent = transform;
	//	bgmPlayer = bgmObject.AddComponent<AudioSource>();
	//	bgmPlayer.playOnAwake = false;
	//	bgmPlayer.volume = bgmVolum;
	//	bgmPlayer.clip = bgmClip;
	//	bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

	//	// 효과음 플레이어 초기화
	//	GameObject sfxObject = new GameObject("SfxPlayer");
	//	sfxObject.transform.parent = transform;
	//	sfxPlayers = new AudioSource[channels];

	//	for (int index = 0; index < sfxPlayers.Length; index++)
	//	{ 
	//		sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
	//		sfxPlayers[index].playOnAwake = false;
	//		sfxPlayers[index].bypassListenerEffects = true;
	//		sfxPlayers[index].volume = sfxVolum;
	//	}
	//}

	//public void PlayBgm(bool isPlay)
	//{
	//	if (isPlay) 
	//	{
	//		bgmPlayer.Play();
	//	}
	//	else 
	//	{
	//		bgmPlayer.Stop();
	//	}
	//}

	//// 레벨업 구간에서 사용
	//public void EffectBgm(bool isPlay)
	//{
	//	bgmEffect.enabled = isPlay;
	//}

	//public void PlaySfx(Sfx sfx)
	//{
	//	// 채널 수 만큼 순회하도록 채널 인덱스 변수 활용
	//	for (int index = 0; index < sfxPlayers.Length; index++)
	//	{
	//		int loopIndex = (index + channelIndex) % sfxPlayers.Length;

	//		if (sfxPlayers[loopIndex].isPlaying)
	//		{
	//			continue;
	//		}

	//		// 3개면 Switch문으로
	//		//int randIndex = 0;
	//		//if (sfx == Sfx.Hit || sfx == Sfx.Melee)
	//		//{
	//		//	randIndex = Random.Range(0, 2);
	//		//}

	//		channelIndex = loopIndex;
	//		sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
	//		sfxPlayers[loopIndex].Play();
	//		break;
	//	}
	//}

	public void ChangeVolume()
	{ 
		AudioListener.volume = volumSlider.value;
		Save();
	}

	private void Load()
	{
		volumSlider.value = PlayerPrefs.GetFloat("musicVolume");
	}

	private void Save()
	{
		PlayerPrefs.SetFloat("musicVolume", volumSlider.value);
	}
}
