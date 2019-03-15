using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1Controller : MonoBehaviour {

    public NavMeshAgent agent;
    public float Health;
    public GameObject goal;
    public GameController gameControl;
    public GameObject deathParticle;
    AudioSource audioSource;
    public AudioClip deathSound;

    void Start()
    {
        gameControl = FindObjectOfType<GameController>();
        goal = GameObject.FindGameObjectWithTag("Goal");
        Vector3 target = transform.position + Vector3.forward;
        agent.SetDestination(goal.transform.position);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        goal = GameObject.FindGameObjectWithTag("Goal");
        //goal = FindClosestGoal();
        agent.SetDestination(goal.transform.position);
        if (Health<= 0)
        {
            audioSource.PlayOneShot(deathSound, 0.2f);
            Instantiate(deathParticle, this.transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Col");
        if (col.gameObject.layer == LayerMask.NameToLayer("Goals1"))
        {
            gameControl.baseHealth -= Health;
           // GameObject explosion;
            Instantiate(deathParticle, this.transform.position,transform.rotation);
            audioSource.PlayOneShot(deathSound, 0.2f);
            Destroy(this.gameObject);
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Goals2"))
        {
            gameControl.baseHealth -= Health;
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
    public GameObject FindClosestGoal()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Goal");
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
