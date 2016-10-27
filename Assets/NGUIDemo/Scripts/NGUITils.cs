﻿using UnityEngine;
using System.Collections;

public class NGUITils {

	public static IEnumerator HideUiWithAlphaTween(GameObject goWidget){
		if(null != goWidget){
			TweenAlpha tween = goWidget.GetComponent<TweenAlpha>();
			if(null != tween){
				yield return new WaitForSeconds(tween.duration);
				if(null != goWidget){
					goWidget.SetActive(false);
				}
			}
		}
	}
}
