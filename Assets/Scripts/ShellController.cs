using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    /// <summary> íœ‚·‚éŠÔw’è </summary>
    public float deltaTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        /// <summary> íœİ’è </summary>
        Destroy(gameObject, deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //@‰½‚©‚ÉÚG‚µ‚½‚çÁ‚·
        Destroy(gameObject);
    }
}
