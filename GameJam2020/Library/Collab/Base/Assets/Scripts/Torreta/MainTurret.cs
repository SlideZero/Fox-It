using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            if(canvasGameOver.activeSelf == true)
            {
                camaraHUD.SetActive(false);
            }
    	}
    }
}
