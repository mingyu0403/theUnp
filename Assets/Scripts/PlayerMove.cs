using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	public float speed;   // 이동 속도
	public float jumpSpeed;   // 점프 속도
	public float gravity;   // 중력 힘
	private Vector3 moveDirection = Vector3.zero;

	private float boundPower = 0f;  // 총 발사 시 카메라 반동 힘

	private Animator animator;
	public GameObject soldier;

	private float verticalRotation = 0.0f;

	// Use this for initialization
	void Start () {
		animator = soldier.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		move ();
		animatorChange ();
		FBRotate ();
		if (boundPower < 0)
			boundPower = boundPower + 0.3f;
	}
		
	void FBRotate(){
		
		transform.Rotate (0f, Input.GetAxis ("Mouse X") * 2f, 0f);

		verticalRotation -= Input.GetAxis ("Mouse Y") * 2f;
		verticalRotation = Mathf.Clamp (verticalRotation, -90, 90);

		Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation + boundPower, 0f, 0f);
		Camera.main.cullingMask = ~(1 << 10); // 10번 레이어만 카메라에 보지 않게 한다.
	}

	void move () {
		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) { // 땅에 있으면
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection); // 축을 바꾼다. 월드 좌표로
			moveDirection *= speed;
		if (Input.GetButton("Jump")) 
			moveDirection.y = jumpSpeed;
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}

	void bound(){
		boundPower -= 3;
	}

	void animatorChange(){
		if (Input.GetKey (KeyCode.W)) {
			animator.SetBool ("forward", true);
		} else {
			animator.SetBool ("forward", false);
		}
		if (Input.GetKey (KeyCode.D)) {
			animator.SetBool ("right", true);
		} else {
			animator.SetBool ("right", false);
		}
		if (Input.GetKey (KeyCode.A)) {
			animator.SetBool ("left", true);
		} else {
			animator.SetBool ("left", false);
		}
		if (Input.GetKey (KeyCode.S)) {
			animator.SetBool ("down", true);
		} else {
			animator.SetBool ("down", false);
		}
	}
}
