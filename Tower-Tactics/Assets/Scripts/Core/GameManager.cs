using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region Properties
    private InputManager _inputManager;
    private AudioManager _audioManager;
    private Camera mainCam;
    public Grid grid;
    public bool isPaused = false;

    #region Selectables Properties
    private GameObject _selectedObjectPrefab;
    public bool _objectSelected = false;
    public bool _clearSelected = false;
    #endregion

    #region  Enmey Properties
    public GameObject enemyPrefab;
    public Transform startpoint;
    public float spawnEnemyInterval = 0.5f;
    public float spawnWaveInterval = 10.0f;
    public int spawnCount = 5;
    private float elapsedTime;
    private int enemiesSpawnedInCurrentWave;
    #endregion

    #endregion

    #region Control Properties
    private void Awake() 
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            _inputManager = gameObject.GetComponent<InputManager>();
            _audioManager = gameObject.GetComponent<AudioManager>();
            grid.CreateGrid();
            PauseGame();
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StartGame()
    {
        _audioManager.PlayGameBackgroundMusic();
        isPaused = false;
        elapsedTime = 0f;
        enemiesSpawnedInCurrentWave = 0;
        StartCoroutine(SpawnEnemyRoutine());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        _audioManager.PlayMenuBackgroundMusic();
        isPaused = true;
        Time.timeScale = 0;
        DeselectPlaceable();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    // private void Start()
    // {
    //     //PauseGame();
    //     grid.CreateGrid();
    //     elapsedTime = 0f;
    //     enemiesSpawnedInCurrentWave = 0;
    //     StartCoroutine(SpawnEnemyRoutine());
    // }



    #endregion
    
    #region Private: GridCell Methods

    // Drops a tower into a cell
    private void PlaceOjbectInCell(GridCell cell)
    {
        if (_selectedObjectPrefab && !cell.isOccupied)
        {
            GameObject newTower = Instantiate(_selectedObjectPrefab, cell.transform.position, Quaternion.identity);
            cell.objectInThisGridSpace = newTower;
            cell.isOccupied = true;
        }
    }
    
    // Removes a object from a cell
    private void RemoveObjectFromCell(GridCell cell)
    {
        if (cell.isOccupied)
        {
            Destroy(cell.objectInThisGridSpace);
            cell.objectInThisGridSpace = null;
            cell.isOccupied = false;
        }
    }

    #endregion

    #region Public: GridCell Interface Methods

    public void ControlGridInput(Touch touch)
    {
        Ray ray = mainCam.ScreenPointToRay(touch.position);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, LayerMask.GetMask("GridCell")))
        {
            GridCell cell = raycastHit.collider.GetComponent<GridCell>();
            if (_clearSelected)
                RemoveObjectFromCell(cell);
            else if(_objectSelected)
                PlaceOjbectInCell(cell);
        }
        else
           DeselectEverything();
    }

    public void ClearField()
    {
        grid.ClearGrid();
    }

    #endregion

    #region Object Selection Methods
    // Selects a tower from the UI
    public void SelectSwitch(GameObject objectPrefab)
    {
        if (_objectSelected && objectPrefab == _selectedObjectPrefab) DeselectPlaceable();
        else SelectPlaceable(objectPrefab);
    }

    // Selects the clear button from the UI
    public void SelectSwitchClearButton()
    {
        DeselectPlaceable();
        _clearSelected = !_clearSelected;
    }

    // Selects a tower from the UI
    private void SelectPlaceable(GameObject objectPrefab)
    {
        _clearSelected = false;
        _selectedObjectPrefab = objectPrefab;
        _objectSelected = true;
    }

    // De-selects a tower from the UI
    private void DeselectPlaceable()
    {
        _selectedObjectPrefab = null;
        _objectSelected = false;
    }

    // De-selects everything
    private void DeselectEverything()
    {
        DeselectPlaceable();
        _clearSelected = false;
    }
    
    #endregion

    #region Waves Control Methods
    // Spawns a wave of enemies
    private IEnumerator SpawnEnemyRoutine(){
        while (true){
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= spawnWaveInterval + (enemiesSpawnedInCurrentWave * spawnEnemyInterval)){
                if (enemiesSpawnedInCurrentWave < spawnCount){
                    SpawnEnemy();
                    enemiesSpawnedInCurrentWave++;
                }
                else{
                    elapsedTime = 0f;
                    enemiesSpawnedInCurrentWave = 0;
                }
            }
            yield return null;
        }
    }

    private void SpawnEnemy(){
        Instantiate(enemyPrefab, startpoint.position, startpoint.rotation);
    }
    
    #endregion
}