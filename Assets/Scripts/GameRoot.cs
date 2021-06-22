using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour {

	public Transform[] Zombie_SpawnerTr;
	public GameObject[] Zombies; // 0 zombie, 1 police_zombie;
	public Transform Player_SpawnerTr;
	public GameObject Player;
	GameObject[] zombiecnt;

	// Use this for initialization
	void Start () {
		//Instantiate (Player, Player_SpawnerTr.position, Player_SpawnerTr.rotation);
		InvokeRepeating ("Zombie_Spawn", 0, 2f);
	}

	// Update is called once per frame
	void Update () {
		zombiecnt = GameObject.FindGameObjectsWithTag("ENEMY"); 
	}

	void Zombie_Spawn(){
		if (zombiecnt.Length < 15f) {  // 좀비 개수 제한

			Instantiate (Zombies [Random.Range (0, Zombies.Length)],
				Zombie_SpawnerTr [Random.Range (0, Zombie_SpawnerTr.Length)].position, 
				Zombie_SpawnerTr [Random.Range (0, Zombie_SpawnerTr.Length)].rotation);
		}
	}
}
