using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    #region Properties
    private InputManager _inputManager;
    private AudioManager _audioManager;
    private ShopManager _shopManager;
    private WaveManager _waveManager;
    private Camera mainCam;
    public Grid grid;
    public bool isPaused = false;
    public int lives = 20;
    public TextMeshProUGUI livesText;

    #region Selectables Properties
    private GameObject _selectedObjectPrefab;
    private int _itemNumber;
    public bool _objectSelected = false;
    public bool _clearSelected = false;
    public GameObject shopCanvas;
    public GameObject gameCanvas;
    public GameObject menuCanvas;
    public GameObject loseCanvas;
    public GameObject winCanvas;
    public GameObject levelCompleteCanvas;
    public GameObject homeErrorMessage;
    #endregion

    // #region  Enmey Properties
    // public GameObject enemyPrefab;
    // public Transform startpoint;
    // public float spawnEnemyInterval = 0.5f;
    // public float spawnWaveInterval = 1.0f;
    // public int spawnCount = 5;
    // private float elapsedTime;
    // private int enemiesSpawnedInCurrentWave;
    // #endregion

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
            _waveManager = gameObject.GetComponent<WaveManager>();
            _inputManager = gameObject.GetComponent<InputManager>();
            _audioManager = gameObject.GetComponent<AudioManager>();
            _shopManager = gameObject.GetComponent<ShopManager>();
            grid.CreateGrid();
            PauseGame();
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StartGame(TMP_Text errorMessage)
    {
        _waveManager.level = 0;
        if (_shopManager.GetNumberOfSelectedTowers() == 0) { 
            errorMessage.text = "No Towers Selected!";
            //homeErrorMessage.SetActive(true);
            return;}
        errorMessage.text = "";
        Debug.Log("Number of selected items: " + _shopManager.GetNumberOfSelectedItems());
        ResumeGame();
        shopCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        //elapsedTime = 0f;
        //enemiesSpawnedInCurrentWave = 0;
        //StartCoroutine(SpawnEnemyRoutine());
        _shopManager.RenderSelectedItems();
        _shopManager.StartGame();
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
        _audioManager.PlayGameBackgroundMusic();
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
    
    void Update()
    {
        livesText.text = $"{lives}";
    }

    #endregion
    
    #region Private: GridCell Methods

    // Drops a tower into a cell
    private void PlaceOjbectInCell(GridCell cell)
    {
        if (_selectedObjectPrefab && !cell.isOccupied && CanPlaceObstacle(_waveManager.startPoint, _waveManager.endPoint, _selectedObjectPrefab, cell.transform.position))
        {
            if (!_shopManager.CanAffordItem(_itemNumber)) { 
                _audioManager.PlayErrorSound();
                return;}
            _shopManager.PurchasePlaceableItem(_itemNumber);
            GameObject newTower = Instantiate(_selectedObjectPrefab, cell.transform.position, Quaternion.identity);
            cell.objectInThisGridSpace = newTower;
            cell.isOccupied = true;
            cell.itemID = _itemNumber;
        }
    }
    
    // Removes a object from a cell
    private void RemoveObjectFromCell(GridCell cell)
    {
        if (cell.isOccupied)
        {
            _shopManager.RefundPlaceableItem(cell.itemID);
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
            if(_objectSelected)
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
    private void SelectSwitch(GameObject objectPrefab)
    {
        if (_objectSelected && objectPrefab == _selectedObjectPrefab) DeselectPlaceable();
        else SelectPlaceable(objectPrefab);
    }

    public void SelectItemNumber(int itemNumber)
    {
        _itemNumber = itemNumber;
        SelectSwitch(_shopManager.GetSelectedItemPrefab(itemNumber));
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

    #region Level Control

    public void subtractlives(int livesCost) {
        lives -= livesCost;
        if(lives < 0) {
            lives = 0;
            _audioManager.PlayMenuBackgroundMusic();
            isPaused = true;
            Time.timeScale = 0;
            DeselectPlaceable();
            gameCanvas.SetActive(false);
            loseCanvas.SetActive(true);
        }
    }

    public void levelComplete(){
        _audioManager.PlayMenuBackgroundMusic();
        isPaused = true;
        Time.timeScale = 0;
        DeselectPlaceable();
        gameCanvas.SetActive(false);
        levelCompleteCanvas.SetActive(true);
    }

    public void win(){
        _audioManager.PlayMenuBackgroundMusic();
        isPaused = true;
        Time.timeScale = 0;
        DeselectPlaceable();
        gameCanvas.SetActive(false);
        winCanvas.SetActive(true);
    }


    #endregion

    // #region Waves Control Methods
    // // Spawns a wave of enemies
    // private IEnumerator SpawnEnemyRoutine(){
    //     while (true){
    //         elapsedTime += Time.deltaTime;
    //         if (elapsedTime >= spawnWaveInterval + (enemiesSpawnedInCurrentWave * spawnEnemyInterval)){
    //             if (enemiesSpawnedInCurrentWave < spawnCount){
    //                 SpawnEnemy();
    //                 enemiesSpawnedInCurrentWave++;
    //             }
    //             else{
    //                 elapsedTime = 0f;
    //                 enemiesSpawnedInCurrentWave = 0;
    //             }
    //         }
    //         yield return null;
    //     }
    // }

    // private void SpawnEnemy(){
    //     Instantiate(enemyPrefab, startpoint.position, Quaternion.identity);
    // }
    
    // #endregion
    public bool CanPlaceObstacle(Transform startPoint, Transform endPoint, GameObject obstaclePrefab, Vector3 obstaclePosition)
    {
        // Instantiate an obstacle in the desired position
        GameObject tempObstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);

        // Calculate the path between the start and end points
        UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
        bool pathFound = UnityEngine.AI.NavMesh.CalculatePath(startPoint.position, endPoint.position, UnityEngine.AI.NavMesh.AllAreas, path);

        // Destroy the temporary obstacle
        Destroy(tempObstacle);

        return pathFound;
    }

}