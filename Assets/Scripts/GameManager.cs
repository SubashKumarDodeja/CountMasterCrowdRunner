using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public PlayerController player;
    public ParticleSystem playerBlood;
    public ParticleSystem enemyBlood;

    public Enemy enemy;
    public Followers followers;
    public float offset;
    public GameObject MainCamera;

    public GameObject levelCompletePanel;
    public GameObject levelFailPanel;
   

    void Start()
    {
        Time.timeScale = 1;
    

    }
    public void GenerateFollower(Leader leader,int number,FollowerOf followertype)
    {
        GameObject lead = new GameObject(leader.name);
        for (int i = 0; i < number; i++)
        {
            Vector3 tempOffset = new Vector3(Random.Range(-offset, offset), 0, Random.Range(-offset, offset));
            Followers tempfollowers = Instantiate(followers, leader.gameObject.transform.position + tempOffset, leader.gameObject.transform.rotation, lead.transform);
            tempfollowers.leader = leader;
            tempfollowers.followerOf = followertype;
            if(tempfollowers.followerOf == FollowerOf.Player)
            {
                leader.gameObject.GetComponent<PlayerController>().follower++;
                leader.gameObject.GetComponent<PlayerController>().followers.Add(tempfollowers);

            }
            else
            {
                leader.gameObject.GetComponent<Enemy>().follower++;
                leader.gameObject.GetComponent<Enemy>().followers.Add(tempfollowers);
            }
        }
    }
    public void LevelFail()
    {
        MainCamera.GetComponent<CameraFollow>().enabled = false;
        Time.timeScale = 0;
        levelFailPanel.SetActive(true);
    }
    public void LevelComplete()
    {

        levelCompletePanel.SetActive(true);
        MainCamera.GetComponent<CameraFollow>().enabled = false;
        Time.timeScale = 0;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
