using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameInput input;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI inventoryBatText;
    public TextMeshProUGUI inventoryCleatsText;
    public Button buyBatButton;
    public Button sellBatButton;
    public Button buyCleatsButton;
    public Button sellCleatsButton;
    [SerializeField] private GameObject canvas;
    private bool canvasStatus = false;
    
    
    public static UIManager instance
    {
        get;
        private set;
    }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
        
        input = new GameInput();
    }

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateUI();
    }
    
    private void OnEnable()
    {
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    private void Update()
    {
        if (input.Player.OpenStore.WasPressedThisFrame())
        {
            ToggleStoreUI();
        }
    }

    public void updateUI()
    {
        cashText.text = "Cash: " + GameManager.instance.getCash().ToString("f2");
        inventoryBatText.text = "Baseball Bat(s): " + GameManager.instance.getBatInventory();
        inventoryCleatsText.text = "Cleats: " + GameManager.instance.getCleatInventory();
    }

    private void ToggleStoreUI()
    {
        canvas.SetActive(!canvas.activeSelf);
        canvasStatus = !canvasStatus;
        Debug.Log(canvasStatus);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public bool getCanvasStatus()
    {
        return canvasStatus;
    }
}
