using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour {

	public AudioSource audio;
	public AudioClip AS_reload; // 재장전 소리
	public AudioClip Shot; // 총 쏘는 소리

	public GameObject Gun; // 총
	float Gun_cnt= 0f;
	float Gun_power = 0.01f; // 총을 쏠 때 총의 움직임 

	public Text text_bulletcnt;


	bool CEK_bulletcnt = true;
	int bulletcnt;
	int bulletMAXcnt = 20; // 총알 최대 개수
	public Transform fire;

	float bullet_damage = 20f;

	float reloadtime = 2.5f; // 총알 재장전 시간
	float delaytimer = 0f; // 총알 연사할 시 중간의 공백 시간 체크
	float ShotDelaytime = 0.13f; // 총알 연사할 시 중간의 공백 시간

	public GameObject muzzleFlash;
	public Transform fireTr;

	// Use this for initialization
	void Start () {
		Cursor.visible = false; // 마우스 커서 안보이게 하기
		bulletcnt = bulletMAXcnt;
	}
	
	// Update is called once per frame
	void Update () {



		text_bulletcnt.text = bulletcnt + " / oo ";

		RayShot ();

	}

	void RayShot(){
		Debug.DrawRay (fire.position, fire.forward * 30f, Color.green);

		delaytimer += Time.deltaTime;

		if ((bulletcnt <= 0) || Input.GetKeyDown(KeyCode.R)) {
			if (CEK_bulletcnt == true) {
				CEK_bulletcnt = false;
				StartCoroutine ("reload");
			}
		}

		if (Input.GetMouseButton (0) && delaytimer > ShotDelaytime &&  CEK_bulletcnt == true) {
			audio.PlayOneShot (Shot);
			EffectCreate ();
			GunMove ();
			bulletcnt--;
			SendMessage ("bound", SendMessageOptions.RequireReceiver);
			RaycastHit hit;
			if (Physics.Raycast (fire.position, fire.forward, out hit, Mathf.Infinity)) {
				if (hit.collider.tag == "ENEMY") {
					hit.collider.gameObject.GetComponent<MonsterCtrl> ().hit (bullet_damage);
				}
			}
			delaytimer = 0f;
		}
	}

	void GunMove(){
		
	}

	void EffectCreate(){
		GameObject muzzle = (GameObject )Instantiate (muzzleFlash, fireTr.position, fireTr.rotation * Quaternion.Euler (new Vector3 (1,1,Random.Range(0,90))));
		muzzle.transform.parent = fireTr.transform;
		Destroy (muzzle, 0.2f);
	}

	IEnumerator reload(){
		audio.PlayOneShot (AS_reload);
		yield return new WaitForSeconds (reloadtime);
		bulletcnt = bulletMAXcnt;
		CEK_bulletcnt = true;
	}
}
