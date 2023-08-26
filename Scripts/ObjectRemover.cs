using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRemover : MonoBehaviour
{
    [SerializeField] private float timeToremove;

    // Update is called once per frame
    private void Update()
    {
        if(timeToremove < 0)
        {
            Destroy(gameObject);
        }
        timeToremove -= Time.deltaTime;
    }
}
