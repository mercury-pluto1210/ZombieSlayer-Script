using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    int maxScore = 999999;
    [SerializeField]
    int maxKill = 100;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text killText;
    [SerializeField]
    Text reticle;
    [SerializeField]
    FirstPersonAIO firstPerson;
    [SerializeField]
    FirstPersonGunController gunController;
    [SerializeField]
    GameObject gunSound;
    //[SerializeField]
    //GameObject menuStopSound;
    [SerializeField]
    Text centerText;
    [SerializeField]
    float waitTime = 7;
    [SerializeField]
    EnemySpawner[] spawners;
    [System.NonSerialized]
    int score = 0;
    int kill = 0;
    bool gameOver = false;
    bool gameClear = false;
    public int Score
    {
        set
        {
            score = Mathf.Clamp(value, 0, maxScore);
            scoreText.text = score.ToString("D6");
        }
        get
        {
            return score;
        }
    }
    public int Kill
    {
        set
        {
            kill = value;
            killText.text = kill.ToString("D2") + " / " + maxKill.ToString();
            if (kill >= maxKill)
            {
                StartCoroutine(GameClear());
            }
        }
        get
        {
            return kill;
        }
    }
    void Start()
    {
        InitGame();
        StartCoroutine(GameStart());
        SetSpawners(false);
    }
    void InitGame()
    {
        Score = 0;
        Kill = 0;
        firstPerson.playerCanMove = false;
        firstPerson.enableCameraMovement = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gunController.shootEnabled = false;
        //menuStopSound.SetActive(false);
        gunSound.SetActive(false);
    }
    /*public void StartGameByButton()
    {
        StartCoroutine(GameStart());
    }*/
    public IEnumerator GameStart()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        firstPerson.enableCameraMovement = true;
        yield return new WaitForSeconds(2);
        centerText.enabled = true;
        centerText.text = "3";
        yield return new WaitForSeconds(1);
        centerText.text = "2";
        yield return new WaitForSeconds(1);
        centerText.text = "1";
        yield return new WaitForSeconds(1);
        centerText.text = "GO!!";
        firstPerson.playerCanMove = true;
        gunController.shootEnabled = true;
        gunSound.SetActive(true);
        //menuStopSound.SetActive(true);
        SetSpawners(true);
        yield return new WaitForSeconds(1);
        centerText.text = "";
        reticle.text = "・";
        centerText.enabled = false;
    }
    public IEnumerator GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            firstPerson.playerCanMove = false;
            firstPerson.enableCameraMovement = true;
            gunController.shootEnabled = false;
            //menuStopSound.SetActive(false);
            gunSound.SetActive(false);
            reticle.text = "";
            SetSpawners(false);
            centerText.enabled = true;
            centerText.text = "Game Over";
            StopEnemies();
            yield return new WaitForSeconds(waitTime);
            centerText.text = "";
            centerText.enabled = false;
            gameOver = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            yield return SceneManager.LoadSceneAsync("TitleScene");
        }
        else
        {
            yield return null;
        }
    }
    public IEnumerator GameClear()
    {
        if (!gameClear)
        {
            gameClear = true;
            firstPerson.playerCanMove = false;
            firstPerson.enableCameraMovement = true;
            gunController.shootEnabled = false;
            //menuStopSound.SetActive(false);
            gunSound.SetActive(false);
            reticle.text = "";
            SetSpawners(false);
            centerText.enabled = true;
            centerText.text = "Game Clear!!";
            StopEnemies();
            yield return new WaitForSeconds(waitTime);
            centerText.text = "";
            centerText.enabled = false;
            gameClear = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            yield return SceneManager.LoadSceneAsync("TitleScene");
        }
        else
        {
            yield return null;
        }
    }
    void StopEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
            /*if(enemy.name == "Zombie1(Clone)")
            {
                EnemyController_player controller1 = enemy.GetComponent<EnemyController_player>();
                controller1.moveEnabled = false;
            }
            else if(enemy.name == "Zombie2(Clone)")
            {
                EnemyController_GameOverZone controller2 = enemy.GetComponent<EnemyController_GameOverZone>();
                controller2.moveEnabled = false;
            }*/
        }
    }
    void SetSpawners(bool isEnable)
    {
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.spawnEnabled = isEnable;
        }
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    int maxScore = 99999999;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    FirstPersonAIO firstPerson;
    [SerializeField]
    FirstPersonGunController gunController;
    [SerializeField]
    Text centerText;
    [SerializeField]
    Text reticle;
    [SerializeField]
    float waitTime = 2;
    [SerializeField]
    EnemySpawner[] spawners;
    [System.NonSerialized]
    public bool gameOver = false;
    int score = 0;

    public int Score
    {
        set
        {
            score = Mathf.Clamp(value, 0, maxScore);
            scoreText.text = score.ToString("D8");
        }
        get
        {
            return score;
        }
    }
    void Start()
    {
        InitGame();
        StartCoroutine(GameStart());
        SetSpawners(false);
    }
    void InitGame()
    {
        Score = 0;
        firstPerson.playerCanMove = false;
        firstPerson.enableCameraMovement = true;
        gunController.shootEnabled = false;
    }
    public IEnumerator GameStart()
    {
        yield return new WaitForSeconds(waitTime);
        centerText.enabled = true;
        centerText.text = "3";
        yield return new WaitForSeconds(1);
        centerText.text = "2";
        yield return new WaitForSeconds(1);
        centerText.text = "1";
        yield return new WaitForSeconds(1);
        centerText.text = "GO!!";
        firstPerson.playerCanMove = true;
        firstPerson.enableCameraMovement = true;
        gunController.shootEnabled = true;
        SetSpawners(true);
        yield return new WaitForSeconds(1);
        centerText.text = "";
        reticle.text = "・";
        centerText.enabled = false;
    }
    public IEnumerator GameOver()
    {
        gameOver = true;
        firstPerson.playerCanMove = false;
        firstPerson.enableCameraMovement = false;
        gunController.shootEnabled = false;
        SetSpawners(false);
        centerText.enabled = true;
        centerText.text = "Game Over";
        yield return new WaitForSeconds(waitTime);
        DestroyEnemies();
        centerText.text = "";
        centerText.enabled = false;
        gameOver = false;
    }
    void SetSpawners(bool isEnable)
    {
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.spawnEnabled = isEnable;
        }
    }
    void DestroyEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}*/



/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    int maxScore = 999999;
    [SerializeField]
    int maxKill = 100;
    [SerializeField]
    Canvas mainCanvas;
    [SerializeField]
    Canvas titleCanvas;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text killText;
    [SerializeField]
    Text reticle;
    [SerializeField]
    FirstPersonAIO firstPerson;
    [SerializeField]
    FirstPersonGunController gunController;
    [SerializeField]
    Text centerText;
    [SerializeField]
    float waitTime = 2;
    [SerializeField]
    EnemySpawner[] spawners;
    [System.NonSerialized]
    int score = 0;
    int kill = 0;
    bool gameOver = false;
    bool gameClear = false;
    public int Score
    {
        set
        {
            score = Mathf.Clamp(value, 0, maxScore);
            scoreText.text = score.ToString("D6");
        }
        get
        {
            return score;
        }
    }
    public int Kill
    {
        set
        {
            kill = value;
            killText.text = kill.ToString("D3") + "/" + maxKill.ToString();
            if (kill >= maxKill)
            {
                StartCoroutine(GameClear());
            }
        }
        get
        {
            return kill;
        }
    }
    void Start()
    {
        InitGame();
    }
    void InitGame()
    {
        Score = 0;
        Kill = 0;
        firstPerson.playerCanMove = false;
        firstPerson.enableCameraMovement = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gunController.shootEnabled = false;
    }
    public void StartGameByButton()
    {
        StartCoroutine(GameStart());
    }
    public IEnumerator GameStart()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        firstPerson.enableCameraMovement = true;
        yield return new WaitForSeconds(waitTime);
        centerText.enabled = true;
        centerText.text = "3";
        yield return new WaitForSeconds(1);
        centerText.text = "2";
        yield return new WaitForSeconds(1);
        centerText.text = "1";
        yield return new WaitForSeconds(1);
        centerText.text = "GO!!";
        firstPerson.playerCanMove = true;
        gunController.shootEnabled = true;
        SetSpawners(true);
        yield return new WaitForSeconds(1);
        centerText.text = "";
        reticle.text = "・";
        centerText.enabled = false;
    }
    public IEnumerator GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            firstPerson.playerCanMove = false;
            firstPerson.enableCameraMovement = true;
            gunController.shootEnabled = false;
            SetSpawners(false);
            centerText.enabled = true;
            centerText.text = "Game Over";
            StopEnemies();
            yield return new WaitForSeconds(waitTime);
            centerText.text = "";
            centerText.enabled = false;
            gameOver = false;
            yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            yield return null;
        }
    }
    public IEnumerator GameClear()
    {
        if (!gameClear)
        {
            gameClear = true;
            firstPerson.playerCanMove = false;
            firstPerson.enableCameraMovement = true;
            gunController.shootEnabled = false;
            SetSpawners(false);
            centerText.enabled = true;
            centerText.text = "Game Clear!!";
            StopEnemies();
            yield return new WaitForSeconds(waitTime);
            centerText.text = "";
            centerText.enabled = false;
            gameClear = false;
            yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            yield return null;
        }
    }
    void StopEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            EnemyController controller = enemy.GetComponent<EnemyController>();
            controller.moveEnabled = false;
        }
    }
    void SetSpawners(bool isEnable)
    {
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.spawnEnabled = isEnable;
        }
    }
}*/
