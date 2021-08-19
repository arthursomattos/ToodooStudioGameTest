using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    Animator anime;
    public NavMeshAgent AI;

    public static GameObject player;

    public float enemyHP;          // This is the HP of the enemy

    public GameObject[] Waypoints; //This is the Waypoints that the enemy will be patroling 

    public float timestop = 2.0f; //This is the amount of time that the enemy will be stoped after taking damage
    public float timestopOrginal;
    public bool resetTimer;

    public int stamina;
    public int maxStamina;
    public bool staminaRecover;
    public float stopedTime; //This is the amount of time that the enemy will be stoped after using all of it's stamina

    public GameObject ParticleDie;

    public GameObject OutMagic;
    public GameObject Magic;

    // Use this for initialization
    void Start()
    {
        AI = GetComponent<NavMeshAgent>();
        anime = GetComponent<Animator>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        timestopOrginal = timestop;
    }

    void Update()
    {
        anime.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));

        ResetTime();

        if (stamina >= maxStamina && staminaRecover == false)
        {
            StartCoroutine(Sta());
            staminaRecover = true;
        }
        if (stamina == 0)
        {
            staminaRecover = false;
            anime.SetBool("Idle", false);
        }
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public GameObject[] GetWaypoints()
    {
        return Waypoints;
    }

    void Attack()
    {
        if (stamina < maxStamina)
        {

        }
        else if (stamina >= maxStamina)
        {
            StartCoroutine(Sta());
        }

    }

    public void EnemyDie()
    {
        StartCoroutine(Destroy());
    }


    public void Count()
    {
        stamina++;
    }

    public void StopAttacking()
    {
        CancelInvoke("Attack");
    }

    public void StartAttacking()
    {
        InvokeRepeating("Attack", 0.5f, 0.5f);
    }

    public void MagicAttack()
    {
        Instantiate(Magic, OutMagic.transform.position, OutMagic.transform.rotation);
    }

    public void ResetTime()
    {
        if (resetTimer)
        {
            timestop -= Time.deltaTime;

            //If the parameter bool is set to true, a timer start, when the timer
            //runs out (because the enemy doesn't get hit again fast)
            //timestop is set again to zero, and you need to press hit the enemy again
            if (timestop <= 0)
            {
                resetTimer = false;
                timestop = timestopOrginal;
                anime.SetBool("Damage", false);
            }
        }
    }

    IEnumerator Destroy()
    {
        ParticleDie.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);

    }

    IEnumerator Sta()
    {
        anime.SetBool("Idle", true);
        yield return new WaitForSeconds(stopedTime);
        stamina = 0;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "PlayerMagic1")
        {
            CancelInvoke("Attack");
            if (anime.GetBool("Death") == false)
            {
                anime.Play("GetHit", -1, 0f);
                anime.SetBool("Damage", true);
            }
            enemyHP--;
            resetTimer = true;
            // If the enemy HP is <= 0 he is destroyed
            if (enemyHP <= 0)
            {
                CancelInvoke("Attack");
                anime.SetBool("Death", true);
                EnemyDie();
            }
        }
        else if (c.tag == "PlayerMagic2")
        {
            CancelInvoke("Attack");
            if (anime.GetBool("Death") == false)
            {
                anime.Play("GetHit", -1, 0f);
                anime.SetBool("Damage", true);
            }
            enemyHP -= 2;
            resetTimer = true;
            // If the enemy HP is <= 0 he is destroyed
            if (enemyHP <= 0)
            {
                CancelInvoke("Attack");
                anime.SetBool("Death", true);
                EnemyDie();
            }
        }
    }

}
