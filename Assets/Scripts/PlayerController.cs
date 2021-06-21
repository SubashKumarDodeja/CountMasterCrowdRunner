using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private bool tap;
    private bool isDragging;
    private bool swipeLeft,swipeRight;
    private Vector2 startTouch, swipeDelta;

    public Vector2 SwipeDelte { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public int direction=0;
    public float directionSpeed=2f;
    public GameManager gameManager;


    public CharacterController characterController;
    public Vector3 moveVector;
    public float speed = 5f;
    public List<Followers> followers = new List<Followers>();
    public int follower=0;
    public Text followersCountText;
    // Start is called before the first frame update
    public void UpdateFollowers()
    {
        followersCountText.text = (follower + 1).ToString();

    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Reset()
    {
        startTouch= swipeDelta = Vector2.zero;
        isDragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = Vector3.zero;
        direction = 0;
        tap = swipeLeft = swipeRight=false;
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDragging = true;
            startTouch = Input.mousePosition;
            
          
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Reset();
        }
        if (Input.touches.Length > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDragging = true;
                startTouch = Input.touches[0].position;
            }else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }
        }
        swipeDelta = Vector2.zero;
        if (isDragging)
        {

            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if(Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;

                
            }
        }

        if (swipeDelta.magnitude > 125)
        {

           
            float x = swipeDelta.x;
            if (x < 0)
            {
                swipeLeft = true;
                direction = -1;
            }
            else if (x > 0)
            {
                swipeRight = true;
                direction = 1;
            }

        }
        moveVector = new Vector3(direction, 0, speed);

      //  moveVector = new Vector3( nput.GetAxisRaw("Horizontal")I,0,speed);
        
        characterController.Move(moveVector * speed * Time.deltaTime);
        if (characterController.velocity.magnitude > 2 && this.GetComponent<Animator>().GetBool("Runing")==false)
        {
            this.GetComponent<Animator>().SetBool("Runing",true);
        }
        UpdateFollowers();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<Enemy>().follower > follower)
            {
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(DestroyFollers(other.GetComponent<Enemy>()));

            }
            else {

                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(DestroyFollers(other.GetComponent<Enemy>()));


            }

        }
    }

    IEnumerator DestroyFollers(Enemy enemy)
    {
        float tempSpeed = speed;
        speed = 0;
        int length = this.follower > enemy.follower ? enemy.follower : this.follower;
        List<Followers> playertemp =followers;
        List<Followers> enemyTemp = enemy.followers;
        
        for (int i = 0; i < length; i++)
        {
            // playertemp.Remove(this.followers[i]);
            this.follower--;
            Instantiate(gameManager.playerBlood, this.followers[i].gameObject.transform.position, Quaternion.identity);
            Destroy(this.followers[i].gameObject);
            //enemyTemp.Remove(this.followers[i]);
            enemy.follower--;

            Instantiate(gameManager.enemyBlood, enemy.followers[i].gameObject.transform.position, Quaternion.identity);
            Destroy(enemy.followers[i].gameObject);
            yield return new WaitForSeconds(0.2f);
        }


       
        //Destroy(this.followers[length].gameObject);
        enemy.followers = enemy.followers.Where(item => item != null).ToList();
        followers = followers.Where(item => item != null).ToList();
        
        if(enemy.followers.Count == 0 && followers.Count == 0)
        {
            enemy.follower--;

            Instantiate(gameManager.enemyBlood, enemy.gameObject.transform.position, Quaternion.identity);
            Destroy(enemy.gameObject);
            
            this.follower--;
            FindObjectOfType<GameManager>().LevelComplete();

            Instantiate(gameManager.playerBlood, this.gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (enemy.followers.Count == 0)
        {
            this.follower--;
            Followers temp=followers[followers.Count - 1];
            followers.Remove(temp);
            Instantiate(gameManager.playerBlood, temp.gameObject.transform.position, Quaternion.identity);
            Destroy(temp);
            enemy.follower--;

            Instantiate(gameManager.enemyBlood, enemy.gameObject.transform.position, Quaternion.identity);
            Destroy(enemy.gameObject);
           
        }
        else if (followers.Count == 0)
        {
            //player Dead
            //Fail call
            FindObjectOfType<GameManager>().LevelFail();

            Instantiate(gameManager.playerBlood, this.gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        speed = tempSpeed;
    }
  
}
