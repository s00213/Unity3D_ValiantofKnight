using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
	private EventSystem eventSystem;

	private Canvas popUpCanvas;
	private Stack<PopUpUI> popUpStack; // 편리하게 UI 관리를 위힌 Stack 구조 사용

	private Canvas windowCanvas;

	private Canvas inGameCanvas;

	private void Awake()
	{
		eventSystem = GameManager.Resource.Instantiate<EventSystem>("UI/EventSystem");
		eventSystem.transform.parent = transform;

		popUpCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
		popUpCanvas.gameObject.name = "PopUpCanvas";
		popUpCanvas.sortingOrder = 100;					
		popUpStack = new Stack<PopUpUI>();

		windowCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
		windowCanvas.gameObject.name = "WindowCanvas";
		windowCanvas.sortingOrder = 10;

		//gameSceneCanvas.sortingOrder = 1;

		inGameCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
		inGameCanvas.gameObject.name = "InGameCanvas";
		inGameCanvas.sortingOrder = 0;
	}

	public void ShowPopUpUI(PopUpUI popUpUI)
	{
		PopUpUI ui = GameManager.Pool.GetUI(popUpUI);
		ui.transform.SetParent(popUpCanvas.transform, false);

		// UI 관리를 위힌 Stack 구조 사용
		popUpStack.Push(ui);

		// 팝업이 있을 때   시간 멈추게 함
		Time.timeScale = 0;
	}

	public void ShowPopUpUI(string path)
	{
		PopUpUI ui = GameManager.Resource.Load<PopUpUI>(path);
		ShowPopUpUI(ui);
	}

	public void ClosePopUpUI()
	{
		PopUpUI ui = popUpStack.Pop();
		// 풀 매니저를 통해서 UI 반납함
		GameManager.Pool.ReleaseUI(ui.gameObject);

		// 팝업이 1개도 없으면 시간이 다시 흐르게 함
		if (popUpStack.Count > 0)
		{
			PopUpUI curUI = popUpStack.Peek();
			curUI.gameObject.SetActive(true);
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	public void ClearPopUpUI()
	{
		while (popUpStack.Count > 0)
		{
			ClosePopUpUI();
		}
	}

	//public T ShowWindowUI<T>(T windowUI) where T : WindowUI
	//{
	//	T ui = GameManager.Pool.GetUI(windowUI);
	//	ui.transform.SetParent(windowCanvas.transform, false);
	//	return ui;
	//}

	//public T ShowWindowUI<T>(string path) where T : WindowUI
	//{
	//	T ui = GameManager.Resource.Load<T>(path);
	//	return ShowWindowUI(ui);
	//}

	//public void SelectWindowUI<T>(T windowUI) where T : WindowUI
	//{
	//	windowUI.transform.SetAsLastSibling();
	//}

	//public void CloseWindowUI<T>(T windowUI) where T : WindowUI
	//{
	//	GameManager.Pool.ReleaseUI(windowUI.gameObject);
	//}

	//public void ClearWindowUI()
	//{
	//	WindowUI[] windows = windowCanvas.GetComponentsInChildren<WindowUI>();

	//	foreach (WindowUI windowUI in windows)
	//	{
	//		GameManager.Pool.ReleaseUI(windowUI.gameObject);
	//	}
	//}

	//public T ShowInGameUI<T>(T gameUi) where T : InGameUI
	//{
	//	T ui = GameManager.Pool.GetUI(gameUi);
	//	ui.transform.SetParent(inGameCanvas.transform, false);

	//	return ui;
	//}

	//public T ShowInGameUI<T>(string path) where T : InGameUI
	//{
	//	T ui = GameManager.Resource.Load<T>(path);
	//	return ShowInGameUI(ui);
	//}

	//public void CloseInGameUI<T>(T inGameUI) where T : InGameUI
	//{
	//	GameManager.Pool.ReleaseUI(inGameUI.gameObject);
	//}

	//public void ClearInGameUI()
	//{
	//	InGameUI[] inGames = inGameCanvas.GetComponentsInChildren<InGameUI>();

	//	foreach (InGameUI inGameUI in inGames)
	//	{
	//		GameManager.Pool.ReleaseUI(inGameUI.gameObject);
	//	}
	//}
}
