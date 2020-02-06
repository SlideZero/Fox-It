using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Vector3 direction;
	private Camera _cam;
	private Rigidbody _rb;
	public Animator _anim;

    //Stats
    private float maxTrash = 100.0f;
	[SerializeField]
	private float Trash = 0.0f;

	[SerializeField]
	private float speed = 5;


	//CarryTorret
	private bool canPick = false;
	private bool picked = false;
	private GameObject PickTorret;
	private Quaternion originalDir;
	[SerializeField]
	private Transform TorretHold;				//The place where the player hold the torret while carring it
	[SerializeField]
	private Transform TorretPlace;				//The where the torret is giong to be place

    [SerializeField] private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    		//Input
        	direction.x = Input.GetAxisRaw("Horizontal");
        	direction.z = Input.GetAxisRaw("Vertical");

        	Movement();


        	//To pick object
        	if(canPick)
        	{
        		
        		if(Input.GetButtonDown("Fire1"))
        		{
        			if(!picked)
        			{
        				originalDir = PickTorret.transform.rotation;
        				PickTorret.transform.position = TorretHold.position;
        				PickTorret.transform.parent = gameObject.transform;
        				PickTorret.GetComponent<Turret>().SetPicked(true);
        				picked = true;
        				_anim.SetFloat("Carring", 1.0f);
        			}else
	        		{
	        			PickTorret.transform.position = TorretPlace.position;
	        			PickTorret.transform.parent = null;
	        			PickTorret.transform.rotation = originalDir;
	        			PickTorret.GetComponent<Turret>().SetPicked(false);
	        			picked = false;
	        			PickTorret.GetComponent<Turret>().ResetTarget();
	        			_anim.SetFloat("Carring", 0.0f);
	        		}
        		}

        		if(Input.GetButton("Fire2"))
        		{
        			if(!picked && Trash > 0 && PickTorret != null)
        			{
        				PickTorret.GetComponent<Turret>().ApplyDamage(-10.0f * Time.deltaTime);
        				Trash -= 10.0f * Time.deltaTime;
        				_anim.SetFloat("Carring", 0.5f);
        				if(Trash < 0)
        				{
        					Trash = 0;
        				}

                        if (!_audioSource.isPlaying)
                        {
                        _audioSource.Play();
                        }
        			}
                    else
                    {
                        _audioSource.Stop();
                    }
        		}else if(Input.GetButtonUp("Fire2"))
        		{
        			_anim.SetFloat("Carring", 0.0f);
        		}
            }
    }

    void FixedUpdate()
    {
    	if(direction != Vector3.zero)
    	{
    		_rb.MovePosition(_rb.position + (direction * speed * Time.deltaTime));
    		_rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
    	}
    }


    private void Movement()
    {
    	if(direction.x != 0 || direction.z != 0)
    	{
    		_anim.SetFloat("Moving", 1.0f);
    	}else
    	{
    		_anim.SetFloat("Moving", 0.0f);
    	}
    	direction = direction.normalized;
    	direction = _cam.transform.TransformDirection(direction);
    	direction.y = 0.0f;
    }

    public float getTrash()
    {
        return Trash;
    }

    public float getMaxTrash()
    {
        return maxTrash;
    }

    private void OnTriggerEnter(Collider other)
    {
    	if(other.tag == "Torret" && !picked)
    	{
    		canPick = true;
    		PickTorret = other.gameObject;
    		
    	}

    	if(other.tag == "Trash")
    	{
            if(Trash < maxTrash)
            {
                Trash += 15.0f;
                Destroy(other.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }
    	}
    }

    private void OnTriggerExit(Collider other)
    {
    	if(other.tag == "Torret" && !picked)
    	{
    		canPick = false;
    		PickTorret = null;
    		
    	}
    }
}
