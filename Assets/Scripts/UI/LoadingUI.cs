using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
	[SerializeField] private Slider slider;

	private Animator animater;

	private void Awake()
	{
		animater = GetComponent<Animator>();
	}

	public void FadeIn()
	{
		animater.SetBool("Active", true);
		Debug.Log("Fade In");
	}

	public void FadeOut()
	{
		animater.SetBool("Active", false);
		Debug.Log("Fade Out");
	}

	public void SetProgress(float progress)
	{
		slider.value = progress;
	}
}