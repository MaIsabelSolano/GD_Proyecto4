using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LVLManager : MonoBehaviour
{

    [SerializeField] BoxCollider2D[] triggers;

    [SerializeField] Slider hpSlider;
    [SerializeField] Slider manaSlider;

    public PlayerMovement player;

    private int currentCharacter;
    [SerializeField] GameObject cc;
    [SerializeField] Sprite[] pf;

    # region UI
    public bool isPaused = false;
    [SerializeField] GameObject uiPause;
    [SerializeField] GameObject uiGameOver;
    [SerializeField] GameObject uiClearedLvl;

    #endregion

    // Start is called before the first frame update

    public UnityEvent playerEvents;
    void Start()
    {

        Time.timeScale = 1; 
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(pm.mana);

        hpSlider.value = player.life;
        manaSlider.value = player.mana;

        if (currentCharacter != player.currentCharacter) {

            currentCharacter = player.currentCharacter;
            cc.GetComponent<Image>().sprite = pf[currentCharacter];

        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        }

        if (player.life <= 0) {
            GameOver();
        }

    }

    public void ReloadLvl() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
    }

    public void Pause() {
        if (!isPaused) {
            uiPause.SetActive(true);
            isPaused = true;       
            Time.timeScale = 0f;     
        } else {
            uiPause.SetActive(false);
            isPaused = false; 
            Time.timeScale = 1;
        }
    }

    public void GameOver() {
        Time.timeScale = 0f; 
        uiGameOver.SetActive(true);
    }
}
