using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina_UI : MonoBehaviour
{
    public GameObject targetObject;
    public Slider sliderObject;

    private float actualStamina;
    private float maxStamina;
    private float timerUpdate = 0.5f;

    private void Awake()
    {
        actualStamina = targetObject.GetComponent<Player>().getTrash();
        maxStamina = targetObject.GetComponent<Player>().getMaxTrash();
    }
    // Start is called before the first frame update
    void Start()
    {
        sliderObject.maxValue = maxStamina;
        sliderObject.value = actualStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerUpdate <= 0)
        {
            sliderObject.value = actualStamina;
            if (targetObject.layer == 8)
            {
                actualStamina = targetObject.GetComponent<Player>().getTrash();
            }
        }
        timerUpdate = timerUpdate - Time.deltaTime;
    }
}
