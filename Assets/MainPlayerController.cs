using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MainPlayerController : MonoBehaviour {

	private Animator anim;
	public float speed = 1.5f;
	public float jumpPower  = 6.0f;

	private CharacterController controller;
	private Vector3 direction;

	void Start () {
		controller = GetComponent<CharacterController>();
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
}
