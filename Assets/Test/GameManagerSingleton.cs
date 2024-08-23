using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSingleton : MonoBehaviour
{
    // Статическое поле для хранения ссылки на единственный экземпляр класса
    private static GameManagerSingleton instance;

    // Свойство для доступа к экземпляру
    public static GameManagerSingleton Instance
    {
        get
        {
            // Если экземпляр еще не был создан, найдем его или создадим новый
            if (instance == null)
            {
                instance = FindObjectOfType<GameManagerSingleton>();

                // Если экземпляр все еще не найден, создадим новый объект с этим компонентом
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<GameManagerSingleton>();
                    singletonObject.name = typeof(GameManagerSingleton).ToString() + " (Singleton)";
                }
            }

            return instance;
        }
    }

    // Признак того, что объект не должен уничтожаться при загрузке новой сцены
    public bool dontDestroyOnLoad = true;

    private void Awake()
    {
        // Если есть другой экземпляр, который уже существует, уничтожаем этот экземпляр
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    public bool isScrewing = false;
    public List<ScrewMechanic> screwMechanics = new List<ScrewMechanic>();
    public List<PlayerScript> player = new List<PlayerScript>();
    [SerializeField] private GameObject WonMenu;
    [SerializeField] private GameObject LoseMenu;

    private void Start()
    {
        
    }

    public void AddScrewToList(ScrewMechanic screwMechanic)
    {
        screwMechanics.Add(screwMechanic);
    }
    
    public void ChangeCheck()
    {
            isScrewing = !isScrewing;
    }

    public void OpenWonMenu()
    {
        WonMenu.SetActive(true);
    }
    
    public void OpenLoseMenu()
    {
        LoseMenu.SetActive(true);
    }
}
