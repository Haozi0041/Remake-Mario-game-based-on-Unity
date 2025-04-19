using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int world { get; private set; }//世界索引
    public int stage { get; private set; }//关卡索引
    public int lives { get; private set; }//生命索引

    public int coins { get; private set; }//硬币数量
    /// <summary>
    /// GameManager 的一种写法，当切换关卡 销毁当前场景游戏物体时 会保留原来的管理器
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
            DontDestroyOnLoad(gameObject);//常用于切换场景时  使对象不销毁
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

    private void LoadLevel(int world,int stage)//加载关卡
    {
        this.world = world;
        this.stage = stage;
        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void NextLevel()
    {
        LoadLevel(world, stage + 1);
    }

    public void RestLevel(float delay)//重置关卡 利用重载 在短暂延时后重置
    {
        Invoke(nameof(RestLevel), 3f);
    }
    
    public void RestLevel()//重置关卡 用于死亡后重置
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
