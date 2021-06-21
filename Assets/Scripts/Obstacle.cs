using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            List<Followers> allFollowers = other.GetComponent<PlayerController>().followers;
            if(allFollowers.Count>0)
            {

                Followers temp = allFollowers[allFollowers.Count - 1];

                
                other.GetComponent<PlayerController>().follower--;
                allFollowers.Remove(temp);
                Instantiate(gameManager.playerBlood, temp.gameObject.transform.position, Quaternion.identity);
                Destroy(temp.gameObject);
            }
            else
            {
                other.GetComponent<PlayerController>().follower--;
                Instantiate(gameManager.playerBlood, other.gameObject.transform.position, Quaternion.identity);
                Destroy(other.GetComponent<PlayerController>().gameObject);
                FindObjectOfType<GameManager>().LevelFail();
              
            }
        }
        else if (other.GetComponent<Followers>())
        {
            Followers obj = other.GetComponent<Followers>();
            if (obj.leader.gameObject.GetComponent<PlayerController>())
            {
                obj.leader.gameObject.GetComponent<PlayerController>().follower--;
                obj.leader.gameObject.GetComponent<PlayerController>().followers.Remove(obj);
                Instantiate(gameManager.playerBlood, obj.gameObject.transform.position, Quaternion.identity);
                Destroy(obj.gameObject);
            }
        }
    }
}
