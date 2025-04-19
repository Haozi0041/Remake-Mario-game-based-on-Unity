using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int world { get; private set; }//��������
    public int stage { get; private set; }//�ؿ�����
    public int lives { get; private set; }//��������

    public int coins { get; private set; }//Ӳ������
    /// <summary>
    /// GameManager ��һ��д�������л��ؿ� ���ٵ�ǰ������Ϸ����ʱ �ᱣ��ԭ���Ĺ�����
    /// </summary>
    private void Awake()
    {
        if(Instance!=null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);//�������л�����ʱ  ʹ��������
        }
    }

    private void OnDestroy()
    {
        if(Instance==this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }
    private void NewGame()
    {
        lives = 3;
        coins = 0;
        LoadLevel(1,1);
    }

    private void LoadLevel(int world,int stage)//���عؿ�
    {
        this.world = world;
        this.stage = stage;
        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void NextLevel()
    {
        LoadLevel(world, stage + 1);
    }

    public void RestLevel(float delay)//���ùؿ� �������� �ڶ�����ʱ������
    {
        Invoke(nameof(RestLevel), 3f);
    }
    
    public void RestLevel()//���ùؿ� ��������������
    {
        lives--;
        if(lives>0)
        {
            LoadLevel(world, stage);
        }
        else
        {
            GameOver();
        }
    }

    public void AddCoins()
    {
        coins++;
        if(coins==100)
        {
            coins = 0;
            AddLives();
        }
    }

    public void AddLives()
    {
        lives++;
    }


    private void GameOver()
    {
        NewGame();
    }

}
