using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar_UI : MonoBehaviour
{
    public GameObject targetObject;

    private float actualLifeObject;
    private Camera _cam;

    public Slider sliderObject;

    private float timerUpdate = 0.5f;

    private void Start()
    {
        if (targetObject.layer == 9)
        {

            actualLifeObject = targetObject.GetComponent<Enemy>().getLife();
        }
        else if (targetObject.tag == "MainTorret")
        {
            actualLifeObject = targetObject.GetComponent<MainTurret>().GetLife();
        }
        else
        {
            actualLifeObject = targetObject.GetComponent<Turret>().GetTurretLife;
        }
        sliderObject.maxValue = actualLifeObject;
        sliderObject.value = sliderObject.maxValue;
        _cam = Camera.main;
    }

    private void Update()
    {
        if(timerUpdate <= 0)
        {
            sliderObject.value = actualLifeObject;
            if (targetObject.layer == 9)
            {
                actualLifeObject = targetObject.GetComponent<Enemy>().getLife();
            }
            else if (targetObject.tag == "MainTorret")
            {
                actualLifeObject = targetObject.GetComponent<MainTurret>().GetLife();
            }
            else
            {
                actualLifeObject = targetObject.GetComponent<Turret>().GetTurretLife;
            }

        }
        timerUpdate = timerUpdate - Time.deltaTime;

        transform.rotation = Quaternion.LookRotation(-_cam.transform.forward);
    }
}
