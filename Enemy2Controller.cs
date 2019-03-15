using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2Controller : MonoBehaviour {

    public NavMeshAgent agent;
    public float Health;
    public GameObject goal;
    private GameController gameControl;
    public GameObject[] Players;
    public GameObject deathParticle;
    AudioSource audioSource;
    public AudioClip deathSound;

    // Use this for initialization
    void Start () {
        gameControl = FindObjectOfType<GameController>();
        //goal = FindClosestPlayer();
        Vector3 target = transform.position + Vector3.forward;
        // Players[0] = GameObject.FindGameObjectWithTag("Player1");
        // Players[1] = GameObject.FindGameObjectWithTag("Player2");
        // agent.SetDestination(goal.transform.position);
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        //Players[0] = GameObject.FindGameObjectWithTag("Player1");
       // Players[1] = GameObject.FindGameObjectWithTag("Player2");
        if (Health <= 0)
        {
            audioSource.PlayOneShot(deathSound, 0.2f);
            Instantiate(deathParticle, this.transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        goal = FindClosestPlayer();
        agent.SetDestination(goal.transform.position);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag =="Player")
        {
            audioSource.PlayOneShot(deathSound, 0.2f);
            Instantiate(deathParticle, this.transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        if (col.gameObject == goal)
        {
            audioSource.PlayOneShot(deathSound, 0.2f);
            Instantiate(deathParticle, this.transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        if (col.gameObject.name == "Goal")
        {
            audioSource.PlayOneShot(deathSound, 0.2f);
            gameControl.player1Score -= Health;
            Instantiate(deathParticle, this.transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == "Player1Bullet")
        {
            Health = Health - 1;
            gameControl.player1Score += 1;
        }
        if (col.gameObject.tag == "Player2Bullet")
        {
            Health = Health - 1;
            gameControl.player2Score += 1;
        }
    }
    public GameObject FindClosestPlayer()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
