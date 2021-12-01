using UnityEngine;

public class RoofVisuals : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] mMeshes= null;
    
    public void InsideBuilding()
    {
        foreach(MeshRenderer m in mMeshes)
        {
            m.enabled = false;
        }
    }

    public void OutsideBuilding()
    {
        foreach (MeshRenderer m in mMeshes)
        {
            m.enabled = true;
        }
    }
}
