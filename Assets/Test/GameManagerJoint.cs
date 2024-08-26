using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerJoint : MonoBehaviour
{
    // Статическое поле для хранения ссылки на единственный экземпляр класса
    private static GameManagerJoint instance;

    // Свойство для доступа к экземпляру
    public static GameManagerJoint Instance
    {
        get
        {
            // Если экземпляр еще не был создан, найдем его или создадим новый
            if (instance == null)
            {
                instance = FindObjectOfType<GameManagerJoint>();

                // Если экземпляр все еще не найден, создадим новый объект с этим компонентом
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<GameManagerJoint>();
                    singletonObject.name = typeof(GameManagerJoint).ToString() + " (Singleton)";
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

    public bool weHaveScrew = false;
    public GameObject screwInHand;
}
