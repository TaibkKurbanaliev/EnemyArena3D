using System.Collections;
using UnityEngine;

public class CoroutinePerformer : MonoBehaviour
{
    private static CoroutinePerformer instance
    {
        get
        {
            if (m_instance == null)
            {
                var gameObject = new GameObject("[ COROUTINE PERFORMER ]");
                m_instance = gameObject.AddComponent<CoroutinePerformer>();
                DontDestroyOnLoad(gameObject);
            }

            return m_instance;
        }
    }

    private static CoroutinePerformer m_instance;

    public static Coroutine StartRoutine(IEnumerator routine)
    {
        return instance.StartCoroutine(routine); ;
    }

    public static void EndRoutine(Coroutine routine)
    {
        instance.StopCoroutine(routine);
    }
}
