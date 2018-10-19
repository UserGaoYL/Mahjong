using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEditor;

public class AnchorMenu
{
	public const string mAutoAnchorMenuName = "Auto Anchor/";
	public const string mPaddingAnchorMenuName = "Padding Anchor/";
	public const string mScaleAnchorMenuName = "Scale Anchor/";
	[MenuItem(mAutoAnchorMenuName + mPaddingAnchorMenuName + "AddAnchor")]
	public static void addPaddingAnchor()
	{
		if (Selection.gameObjects.Length <= 0)
		{
			return;
		}
		// ��ѡ������������ͬһ�����ڵ���
		Transform parent = Selection.transforms[0].parent;
		int count = Selection.gameObjects.Length;
		for (int i = 1; i < count; ++i)
		{
			if (parent != Selection.transforms[i].parent)
			{
				UnityUtility.logError("objects must have the same parent!");
				return;
			}
		}
		for (int i = 0; i < count; ++i)
		{
			addPaddingAnchor(Selection.gameObjects[i]);
		}
	}
	[MenuItem(mAutoAnchorMenuName + mPaddingAnchorMenuName + "RemoveAnchor")]
	public static void removePaddingAnchor()
	{
		if (Selection.gameObjects.Length <= 0)
		{
			return;
		}
		// ��ѡ������������ͬһ�����ڵ���
		Transform parent = Selection.transforms[0].parent;
		int count = Selection.gameObjects.Length;
		for (int i = 1; i < count; ++i)
		{
			if (parent != Selection.transforms[i].parent)
			{
				UnityUtility.logError("objects must have the same parent!");
				return;
			}
		}
		for (int i = 0; i < count; ++i)
		{
			removePaddingAnchor(Selection.gameObjects[i]);
		}
	}
	[MenuItem(mAutoAnchorMenuName + mScaleAnchorMenuName + "AddAnchor")]
	public static void addScaleAnchor()
	{
		if (Selection.gameObjects.Length <= 0)
		{
			return;
		}
		// ��ѡ������������ͬһ�����ڵ���
		Transform parent = Selection.transforms[0].parent;
		int count = Selection.gameObjects.Length;
		for(int i = 1; i < count; ++i)
		{
			if(parent != Selection.transforms[i].parent)
			{
				UnityUtility.logError("objects must have the same parent!");
				return;
			}
		}
		for (int i = 0; i < count; ++i)
		{
			addScaleAnchor(Selection.gameObjects[i]);
		}
	}
	[MenuItem(mAutoAnchorMenuName + mScaleAnchorMenuName + "RemoveAnchor")]
	public static void removeScaleAnchor()
	{
		if (Selection.gameObjects.Length <= 0)
		{
			return;
		}
		// ��ѡ������������ͬһ�����ڵ���
		Transform parent = Selection.transforms[0].parent;
		int count = Selection.gameObjects.Length;
		for (int i = 1; i < count; ++i)
		{
			if (parent != Selection.transforms[i].parent)
			{
				UnityUtility.logError("objects must have the same parent!");
				return;
			}
		}
		for (int i = 0; i < count; ++i)
		{
			removeScaleAnchor(Selection.gameObjects[i]);
		}
	}
	[MenuItem(mAutoAnchorMenuName + mScaleAnchorMenuName + "AutoRelativePos")]
	static void Calculation()
	{
		if (Selection.activeGameObject == null)
		{
			return;
		}
		ScaleAnchor anchor = Selection.activeGameObject.GetComponent<ScaleAnchor>();
		if(anchor == null)
		{
			return;
		}
		var obj = Selection.activeGameObject;
		Vector2 thisPos = obj.transform.localPosition;
		// ��ȡ�����������
		UIRect parentRect = WidgetUtility.findParentRect(obj);
		Vector2 parentSize = WidgetUtility.getRectSize(parentRect);
		// ����
		anchor.mHorizontalRelativePos = thisPos.x / parentSize.x * 2;
		anchor.mVerticalRelativePos = thisPos.y / parentSize.y * 2;
		// ���ó��Զ��巽ʽ
		anchor.mPadding = PADDING_STYLE.PS_CUSTOM_VALUE;
	}
	// ��� PADDING_STYLE ����ֵ
	[MenuItem(mAutoAnchorMenuName + mScaleAnchorMenuName + "DefaultRelativePos")]
	static void ClaerCalculation()
	{
		if(Selection.activeGameObject == null)
		{
			return;
		}
		ScaleAnchor Anchor = Selection.activeGameObject.GetComponent<ScaleAnchor>();
		Anchor.mHorizontalRelativePos = 0.0f;
		Anchor.mVerticalRelativePos = 0.0f;
		Anchor.mPadding = PADDING_STYLE.PS_NONE;
	}
	//-------------------------------------------------------------------------------------------------------------------
	public static void addPaddingAnchor(GameObject obj)
	{
		// �������Լ���Anchor
		if (obj.GetComponent<PaddingAnchor>() == null)
		{
			// ֻҪ��Rect�Ϳ�����Ӹ����,panelҲ�������
			UIRect rect = WidgetUtility.getGameObjectRect(obj);
			if(rect != null)
			{
				PaddingAnchor anchor = obj.AddComponent<PaddingAnchor>();
				anchor.setAnchorMode(ANCHOR_MODE.AM_NEAR_PARENT_SIDE);
			}
		}
		// �������ӽڵ��Anchor
		int childCount = obj.transform.childCount;
		for(int i = 0; i < childCount; ++i)
		{
			addPaddingAnchor(obj.transform.GetChild(i).gameObject);
		}
	}
	public static void removePaddingAnchor(GameObject obj)
	{
		// �������Լ���Anchor
		if (obj.GetComponent<PaddingAnchor>() != null)
		{
			UnityUtility.destroyGameObject(obj.GetComponent<PaddingAnchor>(), true);
		}
		// �������ӽڵ��Anchor
		int childCount = obj.transform.childCount;
		for (int i = 0; i < childCount; ++i)
		{
			removePaddingAnchor(obj.transform.GetChild(i).gameObject);
		}
	}
	public static void addScaleAnchor(GameObject obj)
	{
		// �������Լ���Anchor
		if (obj.GetComponent<ScaleAnchor>() == null)
		{
			obj.AddComponent<ScaleAnchor>();
		}
		// �������ӽڵ��Anchor
		int childCount = obj.transform.childCount;
		for (int i = 0; i < childCount; ++i)
		{
			addScaleAnchor(obj.transform.GetChild(i).gameObject);
		}
	}
	public static void removeScaleAnchor(GameObject obj)
	{
		// �������Լ���Anchor
		if (obj.GetComponent<ScaleAnchor>() != null)
		{
			UnityUtility.destroyGameObject(obj.GetComponent<ScaleAnchor>(), true);
		}
		// �������ӽڵ��Anchor
		int childCount = obj.transform.childCount;
		for (int i = 0; i < childCount; ++i)
		{
			removeScaleAnchor(obj.transform.GetChild(i).gameObject);
		}
	}
}