using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts.Common
{

	public static class RectTransformExtensions
	{
		public static void SetAnchoredPosition (this RectTransform rectTf, Vector3 pos)
		{
			Canvas canvas = rectTf.root.GetComponent<Canvas> ();
			pos.x *= 1 / canvas.transform.localScale.x;
			pos.y *= 1 / canvas.transform.localScale.y;
			pos.z *= 1 / canvas.transform.localScale.z;
			rectTf.anchoredPosition = pos;
		}
	}
}