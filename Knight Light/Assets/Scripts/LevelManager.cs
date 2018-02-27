using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


    public GameObject currentCheckpoint;
    private PlayerController player;

    public GameObject deathParticle;
    public GameObject respawnParticle;

    public int pointPenaltyOnDeath;

    public float respawnDelay;
    public float gravityStore;


	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RespawnPlayer ()
    {
        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo()
    {
        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;
        gravityStore = player.GetComponent<Rigidbody2D>().gravityScale;
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ScoreManager.AddPoints(-pointPenaltyOnDeath);
        Debug.Log("Player respawn");
        yield return new WaitForSeconds(respawnDelay);
        player.GetComponent<Rigidbody2D>().gravityScale = gravityStore;
        player.enabled = true;
        player.GetComponent<Renderer>().enabled = true;
        player.transform.position = currentCheckpoint.transform.position;
        Instantiate(respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
    }
}
