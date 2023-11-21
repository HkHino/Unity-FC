using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool stickToPlayer;
    [SerializeField] private Transform transformPlayer;         //SerializeField makes it possible to adjust values here rather than go to the scrip for each adjustment
    private Transform playerBallPosition;                       //serialization is removed as we have made another reference inside unity and is no longer needed
    float speed;
    Vector3 previousLocation;
    Player scriptPlayer;
    public bool StickToPlayer { get =>  stickToPlayer; set => stickToPlayer = value; }

    // Start is called before the first frame update
    void Start()
    {
        playerBallPosition = transformPlayer.Find("Geometry").Find("BallLocation");
        scriptPlayer = transformPlayer.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stickToPlayer) 
        {
            float distanceToPlayer = Vector3.Distance(transformPlayer.position, transform.position);
            if (distanceToPlayer < 0.5)
            {
                StickToPlayer = true;
                scriptPlayer.BallAttachedToPlayer = this;
            }
        }
        else
        {
            Vector2 currentLocation = new Vector2(transform.position.x, transform.position.z);
            speed = Vector2.Distance(currentLocation, previousLocation) / Time.deltaTime;
            transform.position = playerBallPosition.position;
            transform.Rotate(new Vector3(transformPlayer.right.x,0, transformPlayer.right.z), speed, Space.World);
            previousLocation = currentLocation;
        }
    }
}
