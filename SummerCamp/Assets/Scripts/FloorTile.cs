using Elympics;
using UnityEngine;

public class FloorTile : ElympicsMonoBehaviour
{
    [SerializeField]
    private MeshRenderer rend;

    protected ElympicsBool Marked { get; private set; } = new ElympicsBool();
    

    private void Awake()
    {
        Marked.ValueChanged += HandleValueChanged;
    }

    private void HandleValueChanged(bool lastvalue, bool newvalue)
    {
        rend.material.color = Color.red;
    }

    public void SetColor()
    {
        Marked.Value = true;
    }
}