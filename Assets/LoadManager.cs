using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;

public class LoadManager : MonoBehaviour {

	ReactiveProperty<float> progress = new ReactiveProperty<float>(0.0f); //初期値を指定可能
	public Image progressBar;
	public UIController uiController;

	// Use this for initialization
	void Start () {
		progressBar.fillAmount = progress.Value;

		progress.Subscribe (_ => {
			Debug.Log("change progress");
			progressBar.fillAmount = progress.Value;
		});

		this.ObserveEveryValueChanged (x => uiController.loadStart)
			.Where(x => x)
			.Subscribe(x => loadData());
	}

	void loadData(){
		Observable.Return(Unit.Default)
			.Delay(TimeSpan.FromMilliseconds(1000))
			.Subscribe(_ => {
				Debug.Log("change progress1");
				progress.Value = 0.1f;
			});
		Observable.Return(Unit.Default)
			.Delay(TimeSpan.FromMilliseconds(2000))
			.Subscribe(_ => {
				Debug.Log("change progress2");
				progress.Value = 0.2f;
			});
		Observable.Return(Unit.Default)
			.Delay(TimeSpan.FromMilliseconds(3000))
			.Subscribe(_ => {
				Debug.Log("change progress3");
				progress.Value = 0.3f;
			});
		Observable.Return(Unit.Default)
			.Delay(TimeSpan.FromMilliseconds(4000))
			.Subscribe(_ => {
				Debug.Log("change progress4");
				progress.Value = 0.4f;
			});
	}

	// Update is called once per frame
	void Update () {
		
	}
}
