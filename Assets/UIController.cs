using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class UIController : MonoBehaviour {
	public bool loadStart = false;
	public Image bgProgress;
	public Image progress;
	public Text touchStart;

	void Start () {
		this.UpdateAsObservable ()
			.Where(_ => Input.GetMouseButtonDown(0) && loadStart == false)
			.Subscribe(_ => {
				loadStart = true;
			});

		this.ObserveEveryValueChanged (x => loadStart)
			.Subscribe (x => {
				bgProgress.enabled = loadStart;
				progress.enabled = loadStart;
				touchStart.enabled = !loadStart;
			});
	}



	void Update () {
		
	}
}
