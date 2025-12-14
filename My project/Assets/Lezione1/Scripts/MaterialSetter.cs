using UnityEngine;

public class MaterialSetter : MonoBehaviour
{
    [SerializeField] 
    private MeshRenderer _meshRenderer;
    
    [ContextMenuItem("Set Material", nameof(SetMaterial))]
    public bool _setMat;
    
    public void SetMaterial()
    {
        var mpb = new MaterialPropertyBlock();
        mpb.SetColor("_BaseColor", Color.black);
        mpb.SetFloat("_Metallic", 0.2f);
        _meshRenderer.SetPropertyBlock(mpb);
    }
}