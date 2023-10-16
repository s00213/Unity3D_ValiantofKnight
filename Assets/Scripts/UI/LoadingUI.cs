using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : BaseScene
{
	[SerializeField] private Slider slider;

	private Animator anim;

	private void Awake()
	{
		anim = GetComponent<Animator>();
	}

	public void FadeIn()
	{
		anim.SetBool("Active", true);
		Debug.Log("FadeIn");
	}

	public void FadeOut()
	{
		anim.SetBool("Active", false);
		Debug.Log("FadeOut");
	}

	public void SetProgress(float progress)
	{
		slider.value = progress;
	}
}