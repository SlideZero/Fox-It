using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyTurret : Turret
{

	private Collider[] _enemies;

    // Start is called before the first frame update
    void Start()
    {
        mainTurret = GameObject.Find("MainTurret");
    }

    // Update is called once per frame
    void Update()
    {
    	if(mainTurret != null)
    	{
    	CheckEnemyInRange();
        FollowEnemy();
        Attack();
        Die();
        _enemies = Physics.OverlapSphere(transform.position, _sphereRadius, _enemyLayer);

        foreach(Collider c in _enemies)
        {
            if(c.gameObject.activeInHierarchy)
            {
                c.gameObject.GetComponent<Enemy>().SetTarget(gameObject);
            }
            
        }
    	}
    }
}
