using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum incrementType
{
    addition
    ,multiplication
}
[Serializable]
public struct increment
{
    public incrementType incrementType;
    public int value;
}
public class IncreaseFollower : MonoBehaviour
{
    public GameManager gameManager;
    public increment increment;
    public Text textDisplay;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (increment.incrementType == incrementType.addition)
        {
            textDisplay.text = "+" + increment.value.ToString();
        }
        else if (increment.incrementType == incrementType.multiplication)
        {

            textDisplay.text = "x" + increment.value.ToString();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (increment.incrementType == incrementType.addition)
            {
                gameManager.GenerateFollower(other.gameObject.GetComponent<Leader>(),increment.value, FollowerOf.Player);
            }
            else if (increment.incrementType == incrementType.multiplication)
            {
                    
                
                int length = (other.gameObject.GetComponent<PlayerController>().follower * increment.value)  + increment.value;
                length = length - other.gameObject.GetComponent<PlayerController>().follower-1;
                gameManager.GenerateFollower(other.gameObject.GetComponent<Leader>(), length, FollowerOf.Player);
            }

            this.gameObject.SetActive(false);
        }


            
        
    }
}
