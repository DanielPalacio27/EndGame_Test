using System.Collections;
using UnityEngine;

public class CoroutineUtilities : MonoBehaviour
{
    public static IEnumerator WaitForEndOfFrame(System.Action _action)
    {
        yield return new WaitForEndOfFrame();
        _action.Invoke();
    }
}
