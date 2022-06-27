using System;
using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour
 where T : Component
{
    private static int m_referenceCount = 0;


    private static T m_instance;

    public static T instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<T>();
                if (m_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    m_instance = obj.AddComponent<T>();
                }
            }
            return m_instance;
        }
    }

    [HideInInspector]
    public Transform[] activateOnLoad;

    public void Awake()
    {
        m_referenceCount++;
        if (m_referenceCount > 1)
        {
            Debug.LogWarning("Attemped to create additional static instance: " + this.gameObject.name);
        }

        m_instance = this as T;

        // This is used to activate singleton nodes at the start to initialize their static instance

        if (activateOnLoad != null)
            foreach (var item in activateOnLoad)
            {
                if (item != null)
                    item.gameObject.SetActive(true);
            }

        OnAwake();
    }

    void OnDestroy()
    {
        m_referenceCount--;
        if (m_referenceCount == 0)
        {
            m_instance = null;
        }
    }

    public virtual void OnAwake()
    {

    }
}
