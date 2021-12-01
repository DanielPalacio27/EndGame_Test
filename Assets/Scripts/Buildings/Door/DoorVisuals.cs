using UnityEngine;

public class DoorVisuals : MonoBehaviour
{
    private MeshRenderer mMeshRenderer = null;

    private void Awake() => mMeshRenderer = GetComponent<MeshRenderer>();

    public void OpenAnimation()
    {
        AudioController.Instance.PlaySingle(AudioController.Instance.data.door, false);
        mMeshRenderer.enabled = false;
    }

    public void CloseAnimation()
    {
        AudioController.Instance.PlaySingle(AudioController.Instance.data.door, false);
        mMeshRenderer.enabled = true; 
    }

}
