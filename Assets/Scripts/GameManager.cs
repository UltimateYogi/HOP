
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject startButton, gameOver, player, ball, blockPrefab;

    [SerializeField]
    private Text scoreText, diamondText,gameoverText;

    [SerializeField]
    private Vector3 startPos, offset=new Vector3(0,0,20);

    [SerializeField]
    private int poolSize = 10; // Set a default size for the pool

    private Queue<GameObject> blockPool = new Queue<GameObject>();


    private int score, diamonds;

    public static GameManager instance;
    public bool isGamerunning;

    public delegate void SetComboAnimation(bool isCombo);
    public event SetComboAnimation updateComboAnimation;



    private void Awake()
    {
        isGamerunning = false;
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        startButton.SetActive(true);
        scoreText.gameObject.SetActive(false);
        gameOver.SetActive(false);

        score = 0;
        scoreText.text = score.ToString();
        diamondText.text = diamonds.ToString();


      
        for (int i = 0; i < 4; i++)
        {
            SpawnBlock();
        }

        
    }

    
    
   
    public void SpawnBlock()
    {
        GameObject tempBlock;

        // Reuse blocks from the pool
        if (blockPool.Count > 0)
        {
            tempBlock = blockPool.Dequeue();
           
        }
        else
        {
            tempBlock = Instantiate(blockPrefab);
        }

        startPos += offset;
        float xPos = Random.Range(-8f, 8f);
        tempBlock.transform.position = startPos + new Vector3(xPos, 0, 0);
        tempBlock.GetComponent<Block>().SubscribeToMethod();
        tempBlock.SetActive(true);
    }

    public void ReturnBlockToPool(GameObject block)
    {
        block.SetActive(false);
        blockPool.Enqueue(block);
    }

    public void UpdateScore()
    {
       
        score++;
        scoreText.text = score.ToString();
    
    }

    public void UpdateDiamond()
    {
        diamonds++;

        diamondText.text = diamonds.ToString();
    }

    public void GameStart()
    {
        startButton.SetActive(false);
        scoreText.gameObject.SetActive(true);
        isGamerunning = true;
        
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        isGamerunning=false;
        gameoverText.text =("Your Score " + score.ToString());
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void GameRestart()
    {
        blockPool.Clear();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}
