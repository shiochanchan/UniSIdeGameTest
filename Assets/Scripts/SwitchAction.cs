using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAction : MonoBehaviour
{
    public GameObject targetMoveBlock;
    public Sprite imageOn;
    public Sprite imageOff;
    public bool _on = false;

    // Start is called before the first frame update
    void Start()
    {
        if (_on)
        {
            GetComponent<SpriteRenderer>().sprite = imageOn;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = imageOff;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary> 
    /// ê⁄êGäJén
    /// </summary>
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (_on)
            {
                _on = false;
                GetComponent<SpriteRenderer>().sprite = imageOff;
                MovigBlock movBlock = targetMoveBlock.GetComponent<MovigBlock>();
                movBlock.Stop();
            }
            else
            {
                _on= true; 
                GetComponent<SpriteRenderer>().sprite = imageOn;
                MovigBlock movBlock = targetMoveBlock.GetComponent<MovigBlock>();
                movBlock.Move();
            }
        }
    }
}
