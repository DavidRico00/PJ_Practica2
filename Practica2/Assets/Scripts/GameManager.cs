using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform puntoInicial;
    public GameObject player;
    private CharacterController cc;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (player == null || puntoInicial == null)
            return;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MoverPuntoInicial();
    }

    private void MoverPuntoInicial()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cc = player.GetComponent<CharacterController>();
        puntoInicial = GameObject.FindGameObjectWithTag("SpawnPoint").transform;

        cc.enabled = false;
        player.transform.position = puntoInicial.position;
        cc.enabled = true;
    }
    
    public void CambiarScena(int escena){
        SceneManager.LoadScene(escena);
    }

    public void BotonSalir(){
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public bool isPaused = false;
    public GameObject menuPause;
    public void Pause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            menuPause.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            menuPause.SetActive(false);
        }

        isPaused = !isPaused;
    }

    bool i = false;
    public void BotonPruebas_P()
    {
        player.GetComponent<PlayerHealth>().RecibirDanio(Random.Range(10, 20));
    }
    public void BotonPruebas_O()
    {
        player.GetComponent<PlayerHealth>().Curar(100);
    }

}
