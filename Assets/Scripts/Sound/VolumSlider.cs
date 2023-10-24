using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VolumSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

	private void Start()
	{
		//slider.onValueChanged.AddListener(OnSliderValueChanged);
	}

	private void ChangedMasterVoulum(float newValue)
	{
		// 슬라이더 값이 변경될 때 호출됩니다.
		// newValue는 슬라이더의 현재 값입니다.
		// 이 값을 SoundManager에 전달하여 볼륨을 조절할 수 있습니다.

		// 예를 들어, SoundManager의 SetMasterVolume 함수를 호출하여 볼륨을 조절할 수 있습니다.
		// 여기에서는 볼륨 값을 0에서 1 사이로 정규화한 다음, SetMasterVolume 함수에 전달합니다.
		float normalizedVolume = newValue; // 슬라이더 값은 이미 0에서 1 사이로 정규화되어 있을 것입니다.
		//GameManager.Sound.PlaySound(normalizedVolume);
	}
}
