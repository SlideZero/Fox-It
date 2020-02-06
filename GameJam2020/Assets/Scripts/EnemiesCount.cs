using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesCount : MonoBehaviour
{
    [SerializeField]
    private int numEnemies;
    private int enemies;

    [SerializeField]
    private GameObject cvYouWin;
    [SerializeField]
    private GameObject cvGameOver;
    [SerializeField]
    private GameObject cameraHUD;
    [SerializeField]
    private Text enemyText;


    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        cvYouWin.SetActive(false);
        enemies = 0;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyText.gameObject.activeInHierarchy)
            enemyText.text = "ENEMIES: " + enemies.ToString() + "/" + numEnemies.ToString();

        if(enemies >= numEnemies && !cvGameOver.activeInHierarchy)
        {
            timer += Time.deltaTime;
            //Debug.Log("GanastePrro");

            if(timer >= 2.0f)
            {
                cvYouWin.SetActive(true);
            }
            
            if (cvYouWin.activeSelf == true)
                cameraHUD.SetActive(false);
        }
    }

    public void decrementEnemiesCount()
    {
        enemies++;
    }
}
