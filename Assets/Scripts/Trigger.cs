using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
    enemyGenerate,
    completeLevel
}
public class Trigger : MonoBehaviour
{

    public TriggerType triggerType;

    public GameManager gameManager;
    public Level parent;
    public GameObject Enemy;
    public int FollowerCount;
    int index = 0;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(triggerType == TriggerType.enemyGenerate)
            {
                for (int i = 0; i < parent.triggerPoints.Count; i++)
                {
                    if (this == parent.triggerPoints[i])
                    {
                        index = i;
                        break;
                    }
                }
                if (index + 1 < parent.triggerPoints.Count)

                    parent.triggerPoints[index + 1].gameObject.SetActive(true);
                Enemy.SetActive(true);
                gameManager.GenerateFollower(Enemy.gameObject.GetComponent<Leader>(), FollowerCount, FollowerOf.Enemy);
                this.gameObject.SetActive(false);

            }
            else
            {
                if (triggerType == TriggerType.completeLevel)
                {
                    FindObjectOfType<GameManager>().LevelComplete();
                }
            }
        }
    }
}
