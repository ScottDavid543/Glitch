using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GlitchController : MonoBehaviour {

    public float randGlitch;
    public float randPlayer;
    private float previous;
    /* public bool gunJam = false;
     public bool slowSpeed = false;
     public bool freeMini = false;
     public bool extraHp = false;*/
    public JoyStickController joy;
    public JoyStickController joy2;

    public GameController gameControl;
    public GameObject glitchGo;
    public Text glitchTxt;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    public void GLITCH()
    {

        randGlitch = Random.Range(1, 5);
        randPlayer = Random.Range(1, 3);
        
        if(randPlayer == previous)
        {
            GLITCH();
        }
        //glitchTxt.enabled = true;
        glitchGo.SetActive(true);

         if(randGlitch == 1)
        {
            StartCoroutine(gunJam());
        }
         if(randGlitch == 2)
        {
            StartCoroutine(slowSpeed());
        }
         if(randGlitch == 3)
        {
            StartCoroutine(freeMini());
        }
         if(randGlitch == 4)
        {
            StartCoroutine(extraHp());
        }
        previous = randPlayer;

    }

    IEnumerator gunJam()
    {
        glitchTxt.color = Color.red;
        glitchTxt.text = "GLITCH";
        yield return new WaitForSeconds(2.0f);
       
        if (randPlayer == 1)
        {
            joy.canShoot = false;
            glitchTxt.text = "PLAYER 1 GUN JAM";
        }
        if (randPlayer == 2)
        {
            joy2.canShoot = false;
            glitchTxt.text = "PLAYER 2 GUN JAM";
        }

        yield return new WaitForSeconds(5f);

        if (randPlayer == 1)
        {
            joy.canShoot = true;
        }
        if (randPlayer  == 2)
        {
            joy2.canShoot = true;
        }
        glitchGo.SetActive(false);

    }
    IEnumerator slowSpeed()
    {
        glitchTxt.color = Color.red;
        glitchTxt.text = "GLITCH";
        yield return new WaitForSeconds(2.0f);

        if (randPlayer == 1)
        {
            joy.speed = 3;
            glitchTxt.text = "PLAYER 1 SLOW";
        }
        if (randPlayer == 2)
        {
            joy2.speed = 3;
            glitchTxt.text = "PLAYER 2 SLOW";
        }

        yield return new WaitForSeconds(5f);

        if (randPlayer == 1)
        {
           joy.speed = 9;
        }
        if (randPlayer == 2)
        {
            joy2.speed = 9;
        }
        glitchGo.SetActive(false);
    }
    IEnumerator freeMini()
    {
        glitchTxt.color = Color.green;
        glitchTxt.text = "GLITCH";
        yield return new WaitForSeconds(2.0f);
        if (randPlayer == 1)
        {
            joy.MiniPickup();
            glitchTxt.text = "PLAYER 1 FREE MINIGUN";
        }
        if (randPlayer == 2)
        {
            joy2.MiniPickup();
            glitchTxt.text = "PLAYER 2 FREE MINIGUN";
        }
        yield return new WaitForSeconds(5.0f);
        glitchGo.SetActive(false);
    }
    IEnumerator extraHp()
    {
        glitchTxt.color = Color.green;
        glitchTxt.text = "GLITCH";
        yield return new WaitForSeconds(2.0f);
        glitchTxt.text = "+ 10 BASE HP";
        for (int i = 1; i < 10; i++)
        {
            yield return new WaitForSeconds(0.2f);
            gameControl.baseHealth += 1;
        }
        glitchGo.SetActive(false);
    }
}
