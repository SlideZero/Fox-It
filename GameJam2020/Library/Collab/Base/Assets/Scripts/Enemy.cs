using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject jugador;
    private NavMeshAgent enemigo;
    private GameObject target;
    private float distanceTarget;

    [SerializeField]
    private float daño;
    [SerializeField]
    private float vida;
    public GameObject trashPref;
    [SerializeField]
    private EnemiesCount countEnemies;

    private float temporizadorDeDaño = 1.0f;
    private bool estasDentro;
    
    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.Find("MainTurret");
        enemigo = GetComponent<NavMeshAgent>();
        target = jugador;
        countEnemies = GameObject.Find("EnemiesCount").GetComponent<EnemiesCount>();
    }

    // Update is called once per frame
    void Update()
    {

    	if(target == null)
    	{
    		target = jugador;
    	}else if(target != jugador)
    	{
    		if(target.GetComponent<Turret>().isPicked() == true)
    		{
    			target = jugador;
    			estasDentro = false;
    		}
    	}

    	distanceTarget = Vector3.Distance(transform.position, target.transform.position);

    	if(distanceTarget <= 1f)
    	{
    		estasDentro = true;
    		
    	}
    	else
    	{
    		estasDentro = false;
    	}


        if(!estasDentro)
            enemigo.destination = target.transform.position;


        else
        {
            enemigo.destination = this.transform.position;
            if(temporizadorDeDaño <= 0)
            {
            	if(target == jugador)
            	{
                	jugador.GetComponent<MainTurret>().ApplyDamage(daño);
            	}else
            	{
            		target.GetComponent<Turret>().ApplyDamage(daño);
            	}

                temporizadorDeDaño = 1.0f;
            }
            temporizadorDeDaño = temporizadorDeDaño - Time.deltaTime;
        }
        Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8 || other.tag == "Torret")
        {
        	if(other.gameObject == target)
        	{
        		//estasDentro = true;
        	}
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8 || other.tag == "Torret")
        {
            if(other.gameObject == target)
        	{
        		//estasDentro = false;
        	}
        }
    }

    public void AplicarDaño(float dmg)
    {
        vida -= dmg;
        //Debug.Log(vida);
    }

    public float getLife()
    {
        return vida;
    }

    public void SetTarget(GameObject t)
    {
    	target = t;
    }

    public GameObject GetTarget()
    {
    	return target;
    }

    private void Die()
    {
        if (vida <= 0)
        {
            countEnemies.decrementEnemiesCount();
        	Instantiate(trashPref, transform.position, Quaternion.identity);
        	target = null;
        	vida = 20;
            gameObject.SetActive(false);
        }
    }
}
