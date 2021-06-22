using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCtrl : MonoBehaviour {

	private UnityEngine.AI.NavMeshAgent nvAgent;
	public Transform monsterTr;
	public float hp = 100.0f;
	private Transform playerTr;
	private float distTime = 0f;

	public Animator animator;

	public GameObject bloodEffect;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();

		monsterTr = this.gameObject.GetComponent<Transform> ();

		playerTr = GameObject.Find("Player").GetComponent<Transform> ();
		nvAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {

		float dist = Vector3.Distance (playerTr.position, monsterTr.position);
		if (dist < 5) {
			distTime += Time.deltaTime;
			animator.SetBool ("Attack", true);
			if (distTime > 1.0f) {
				GameObject.Find ("Player").GetComponent<PlayerCtrl> ().playerhit (20f);
				distTime = 0;
			}
		} 
		else {
			distTime = 0;
			animator.SetBool ("Attack", false);
		}
			
		nvAgent.destination = playerTr.position;

		//transform.LookAt (playerTr);

		if (hp <= 0) {
			animator.SetTrigger ("Die");
			nvAgent.Stop ();
			this.gameObject.GetComponent<CapsuleCollider> ().enabled = false;
			Destroy (this.gameObject, 5.0f);
		}
	}

	public void hit(float damage){
		hp = hp - damage;
		animator.SetTrigger ("Hit");
	}
}
