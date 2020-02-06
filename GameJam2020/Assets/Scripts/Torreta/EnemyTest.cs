using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public float life = 15f;

    private void Update()
    {
        if (life <= 0)
            Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
