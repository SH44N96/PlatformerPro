using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform targetA, targetB;
    [SerializeField] private float speed = 5;
    private Player player;
    private bool switching;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if(player == null)
        {
            Debug.LogError("MovingPlatform: Player is NULL");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position == targetA.position)
        {
            switching = false;
        }
        else if(transform.position == targetB.position)
        {
            switching = true;
        }

        if(switching)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetA.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetB.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player.transform.parent = this.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            player.transform.parent = null;
        }
    }
}
