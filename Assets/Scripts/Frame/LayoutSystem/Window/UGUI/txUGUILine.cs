﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class txUGUILine : txUIObject
{
	public LineRenderer mLineRenderer;
	public txUGUILine()
	{
		;
	}
	public override void init(GameLayout layout, GameObject go, txUIObject parent)
	{
		base.init(layout, go, parent);
		mLineRenderer = mObject.GetComponent<LineRenderer>();
		if (mLineRenderer == null)
		{
			mLineRenderer = mObject.AddComponent<LineRenderer>();
		}
	}
	public override void destroy()
	{
		base.destroy();
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
	}
	public void setPointList(Vector3[] pointList)
	{
#if UNITY_2018
		if (pointList.Length > mLineRenderer.positionCount)
		{
			mLineRenderer.positionCount = pointList.Length;
		}
#endif
		mLineRenderer.SetPositions(pointList);
#if UNITY_2018
		if (pointList.Length < mLineRenderer.positionCount)
		{
			// 将未使用的点坐标设置为最后一个点
			int unuseCount = mLineRenderer.positionCount - pointList.Length;
			for (int i = 0; i < unuseCount; ++i)
			{
				mLineRenderer.SetPosition(i + pointList.Length, pointList[pointList.Length - 1]);
			}
		}
#endif
	}
	public void setPointListBezier(Vector3[] pointList, int bezierDetail = 10)
	{
#if UNITY_2018
		setPointList(getBezierPoints(pointList, mLineRenderer.loop, bezierDetail));
#else
		setPointList(getBezierPoints(pointList, false, bezierDetail));
#endif
	}
	public void setPointListSmooth(Vector3[] pointList, int bezierDetail = 10)
	{
#if UNITY_2018
		setPointList(getCurvePoints(pointList, mLineRenderer.loop, bezierDetail));
#else
		setPointList(getCurvePoints(pointList, false, bezierDetail));
#endif
	}
}

