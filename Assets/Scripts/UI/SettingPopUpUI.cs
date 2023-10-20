using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["CloseButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
		buttons["ContinueButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}
}
