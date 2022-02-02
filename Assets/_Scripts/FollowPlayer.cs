using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform playerPos;
    private float xPos;
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.states == GameStates.inGame)
        {
            xPos = playerPos.position.x;
            this.transform.position = new Vector3(xPos, this.transform.position.y, this.transform.position.z);
        }
    }
}
