using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SkeltonController : MonoBehaviour {

	private Animator anim;

	public float speed = 3.0f;
	public float jumpPower  = 6.0f;

	private Vector3 direction;
	private CharacterController controller;

	void Start () {
		controller = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();

		this.gameObject.AddComponent<ObservableUpdateTrigger>();

		IObservable<Unit> clickStream = this.UpdateAsObservable ()
			.Where(_ => Input.GetMouseButtonDown(0));
			//.Select(_ => new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
		
		clickStream
			.Subscribe (input => move (), error => Debug.Log("error"), () => Debug.Log("completed"));



		//int型のReactiveProperty
		ReactiveProperty<int> hp = new ReactiveProperty<int>(10); //初期値を指定可能

		//普通に代入したり、値を読み取ることができる
		hp.Value = 500;
		int currentValue = hp.Value; 

		//Subscribeもできる(Subscribe時に現在の値も発行される）
		hp.Subscribe(x => {
			Debug.Log(x);
			currentValue = x;
		});

		//値を書き換えた時にOnNextが飛ぶ
		int damege = 100;
		hp.Value = currentValue - damege;

		Observable.Return(Unit.Default)
			.Delay(TimeSpan.FromMilliseconds(1000))
			.Subscribe(_ => {
				hp.Value = currentValue - damege;
			});

		//パラメータを渡すパターン
		//現在のプレイヤの座標を500ミリ秒後に表示する
		//複数パラメータ渡したい場合はUniRx.Tupleを使うといいかも。
		Observable.Return(transform.position)
			.Delay(TimeSpan.FromMilliseconds(3000))
			.Subscribe(p => {
				hp.Value = currentValue - damege;
			});

	}

	void move()
	{
		Debug.Log ("click click");

		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		Vector3 inputDirection = new Vector3(inputX,0,inputY);
		direction = Vector3.zero;

		if(inputDirection.magnitude > 0.1f){
			transform.LookAt(transform.position + inputDirection);
			direction += transform.forward * speed;
			anim.SetFloat("Speed",direction.magnitude);
		}else{
			anim.SetFloat("Speed",0);
		}
	}

	void Update ()
	{
		if(controller.isGrounded){
			float inputX = Input.GetAxis("Horizontal");
			float inputY = Input.GetAxis("Vertical");
			Vector3 inputDirection = new Vector3(inputX,0,inputY);
			direction = Vector3.zero;

			if(inputDirection.magnitude > 0.1f){
				transform.LookAt(transform.position + inputDirection);
				direction += transform.forward * speed;
				anim.SetFloat("Speed",direction.magnitude);
			}else{
				anim.SetFloat("Speed",0);
			}
		}

		if (Input.GetKey ("up") || Input.GetKey ("down")) {
			anim.SetBool ("walk", true);
		} else {
			anim.SetBool ("walk", false);
		}

		controller.Move(direction * Time.deltaTime);
		direction.y += Physics.gravity.y * Time.deltaTime;
	}

	void attak(){
		
	}
}
