using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainTurret : MonoBehaviour
{

	[SerializeField]
	private float maxLife;
	private float life;

    [SerializeField]
    private GameObject canvasGameOver;
    [SerializeField]
    private GameObject camaraHUD;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ParticleSystem _particleSystem;

    private void Awake()
    {
        life = maxLife;
    }

    private void Start()
    {
        if(life > 0)
        {
            canvasGameOver.SetActive(false);
        }
        _audioSource = GameObject.Find("BaseExplosion").GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
    	//Debug.Log(life);
        Death();
    }

    public float GetLife()
    {
    	return life;
    }

    public void ApplyDamage(float dmg)
    {
    	life -= dmg;
        //if(life > maxLife)
        //life = maxLife;
        _audioSource.Play();
    }

    public void Death()
    {
    	if(life <= 0)
    	{
            canvasGameOver.SetActive(true);
            //Time.timeScale = 0;
            //_audioSource.Stop();
            if (canvasGameOver.activeSelf == true)
            {
                GameObject particle = ObjectPooler.SharedInstance.GetPooledObject(Manager.turretSmokeTag);
                if (particle != null)
                {
                    _particleSystem = particle.GetComponent<ParticleSystem>();
                    particle.transform.position = new Vector3(transform.position.x, 0.01f, transform.position.z);
                    particle.SetActive(true);
                    _particleSystem.Play(true);
                }

                camaraHUD.SetActive(false);
                Destroy(gameObject);
            }
    	}
    }
}
