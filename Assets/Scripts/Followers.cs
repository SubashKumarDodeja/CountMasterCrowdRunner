using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public enum FollowerOf
{
    Player,
    Enemy
}

public class Followers : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Vector3 _destination;

    public Leader leader;  
    public float offset=2f;
    public FollowerOf followerOf;

    public Vector3 Destination
    {
        get => _destination;
        set
        {
            _destination = value;
            _agent.SetDestination(_destination);
        }
    }
    void Start()
    {
        _agent = this.GetComponent<NavMeshAgent>();
        //Destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (followerOf == FollowerOf.Enemy)
        {

        }
        else
        {

            Vector3 tempOffset = new Vector3(Random.Range(-offset, offset), 0, Random.Range(-offset, offset));
            Destination = leader.gameObject.transform.position + tempOffset;
            if (this._agent.velocity.magnitude > 5)
            {
                this.GetComponent<Animator>().SetBool("Runing", true);
            }
        }



        
    }
 
}
