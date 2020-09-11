using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class Death : Agent
{
    [SerializeField] private float deathForce;
    [SerializeField] private KeyCode deathKey;

    //variables we need fuctions for
    private bool deathIsReady = true;
    private Rigidbody rBody;
    private Vector3 startingPosition;


    public event Action OnReset;

    public override void Initialize()
    {
        rBody = GetComponent<Rigidbody>();
        startingPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (deathIsReady)
            RequestDecision();
    }

    //the agents primary actions, i might move this to the colliders fuction
    public override void OnActionReceived(float[] vectorAction)
    {
        if (Mathf.FloorToInt(vectorAction[0]) == 1)
            Wake();
    }

    //training begins/resets
    public override void OnEpisodeBegin()
    {
        OnReset();
    }

    //some shit about heuristic control - heuristic is basically manual control
    //if player where to play my game, they would instantiate a new cube
    //so maybe nevermind about moving the huristic control
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0;

        if (Input.GetKey(deathKey))
            actionsOut[0] = 1;
    }


    private void Wake()
    {
        if (deathIsReady)
        {
            //    Instantiate(rBody, new Vector3(0,8,0), Quaternion.identity);
            rBody.AddForce(new Vector3(0, deathForce, 0), ForceMode.VelocityChange);

            deathIsReady = false;
        }
    }

    private void Reset()
    {
        deathIsReady = true;
        transform.position = startingPosition;
        rBody.velocity = Vector3.zero;

        OnReset?.Invoke();
    }

    //triggers and colliders/rewards
    private void OnCollisionEnter(Collision collidedObj)
    {
        if (collidedObj.gameObject.CompareTag("Street"))
            deathIsReady = true;
        else if (collidedObj.gameObject.CompareTag("Mover") || collidedObj.gameObject.CompareTag("DoubleMover"))
                {
            AddReward(-1.0f);
            EndEpisode();
        }
    }

    private void OnTriggerEnter(Collider collidedObj)
    {
        if (collidedObj.gameObject.CompareTag("newcube"))
        {
            AddReward(0.1f);
        }
    }
}
