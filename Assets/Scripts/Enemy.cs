using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public List<Followers> followers =  new List<Followers>();
    public int follower = 0;
    public Text followersCountText;
    void Update()
    {

      
        UpdateFollowers();
    }
    public void UpdateFollowers()
    {
        followersCountText.text = (follower + 1).ToString();

    }
}
