using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MainPlayerController : MonoBehaviour {

	private Animator anim;

	void Start () {
		anim = GetComponent<Animator>();

		IObservable<Unit> clickStream = this.UpdateAsObservable ()
			.Where(_ => Input.GetMouseButtonDown(0));
		//.Select(_ => new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

		clickStream
			.Subscribe (input => Move(), error => Debug.Log("error"), () => Debug.Log("completed"));
	}

	void Move(){
		Debug.Log ("main player add");
	}

	void Update ()
	{
		if (Input.GetKey ("up")) {
			anim.SetBool ("walk", true);
		} else {
			anim.SetBool ("walk", false);
		}
	}
}
