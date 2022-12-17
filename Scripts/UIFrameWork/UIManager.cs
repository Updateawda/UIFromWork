using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

	//库  用来缓存所有加载出来的UI
	public Dictionary<string,UIBase> uiDic = new Dictionary<string, UIBase>();
	// + - g c
	//库  存所有正在打开普通UI
	public Dictionary<string, UIBase> uiCacheDic = new Dictionary<string, UIBase>();
	//反向切换库
	public Stack<UIBase> uiStack = new Stack<UIBase>();

	//初始化获取父节点
	//root节点
	public Transform rootCav;
	public Transform mainCav;
	public Transform normalCav;
	public Transform tipsCav;
	public UIManager() {
		rootCav = GameObject.Find("RootCanvas").transform;
		mainCav = rootCav.Find("MainCanvas").transform;
		normalCav = rootCav.Find("NormalCanvas").transform;
		tipsCav = rootCav.Find("TipsCanvas").transform;
	}

	//对外提供的接口
	//打开方法
	public void OpenUI(string panelName)
	{
		UIBase ui = LoadUI(panelName);

		switch (ui.uiType)
		{
			case UIType.Fixed:
				ui.Show();
				break;
			case UIType.Normal:
				if(!uiCacheDic.ContainsKey(panelName))
					uiCacheDic.Add(panelName, ui);

				ui.Show();
				break;
			case UIType.Reverse:

				if (uiStack.Count != 0)
					uiStack.Peek().Hide();

				uiStack.Push(ui);
				ui.Show();

				break;
			case UIType.CloseAll:
                foreach (var item in uiCacheDic.Values)
                {
					item.Hide();
                }

				if (uiStack.Count != 0)
					uiStack.Peek().Hide();

				break;
			case UIType.Tips:
				
				break;
		}

	}


	private UIBase LoadUI(string panelName)
	{
		if (!uiDic.ContainsKey(panelName))
		{
			GameObject temp = Resources.Load<GameObject>(panelName);
			GameObject go = GameObject.Instantiate(temp,rootCav,false);
			UIBase uiBase = go.GetComponent<UIBase>();
			uiBase.Init();
			uiDic.Add(panelName, uiBase);
			switch (uiBase.uiType)
			{
				case UIType.Fixed:
					uiBase.gameObject.transform.parent = mainCav;
					break;
				case UIType.Normal:
					uiBase.gameObject.transform.parent = normalCav;
					break;
				case UIType.Tips:
					uiBase.gameObject.transform.parent = tipsCav;
					break;

			}
		}

		return uiDic[panelName];

	}

	//关闭方法
	public void CloseUI(string panelName)
	{
		if (uiDic.ContainsKey(panelName))
		{
			UIBase ui = uiDic[panelName];
			switch (ui.uiType)
			{
				case UIType.Fixed:
					
					break;
				case UIType.Normal:
					if(uiCacheDic.ContainsKey(panelName))
						uiCacheDic.Remove(panelName);

					uiDic[panelName].Hide();
					break;
				case UIType.Reverse:
					if (uiStack.Count != 0)
					{
						uiStack.Peek().Hide();
						uiStack.Pop();
						if (uiStack.Count != 0)
							uiStack.Peek().Show();
					}
					break;
				case UIType.CloseAll:
                    foreach (var item in uiCacheDic.Values)
                    {
						item.Show();
                    }

					if (uiStack.Count != 0)
						uiStack.Peek().Show();

					break;
				case UIType.Tips:
					
					break;

			}

			
		}
	}
}
