using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private Ball ballAttachedToPlayer;
    private float timeShot = -1f;
    public const int ANIMATION_LAYER_SHOOT = 1;
    
    public Ball BallAttachedToPlayer
    {
        get => ballAttachedToPlayer; set => ballAttachedToPlayer = value;
    }
    // Start is called before the first frame update
    void Start()
    {
     starterAssetsInputs = GetComponent <StarterAssetsInputs>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(starterAssetsInputs.shoot) 
        {
            Debug.Log("outer if statement");
            starterAssetsInputs.shoot = false;
            timeShot = Time.time;
            animator.Play("Shoot", ANIMATION_LAYER_SHOOT, 0f);
            animator.SetLayerWeight(ANIMATION_LAYER_SHOOT, 1f);
            ballAttachedToPlayer = null;
        }
       
        if(timeShot > 0) 
        {
            Debug.Log("inner if statement");
            //shootball
            if (ballAttachedToPlayer != null && Time.time - timeShot > 0.2)
            {
                ballAttachedToPlayer.StickToPlayer = false;

                Rigidbody rigidbody = ballAttachedToPlayer.transform.gameObject.GetComponent<Rigidbody>();
                rigidbody.AddForce(transform.forward * 20f, ForceMode.Impulse);

                ballAttachedToPlayer = null;
            }

            //finish kicking animation
            if(Time.time - timeShot > 0.5)
            {
                timeShot = -1f;
            }            
        }
        else
        {
            animator.SetLayerWeight(ANIMATION_LAYER_SHOOT, Mathf.Lerp(animator.GetLayerWeight(ANIMATION_LAYER_SHOOT), 0f, Time.deltaTime * 10f));
        }
    }
}
