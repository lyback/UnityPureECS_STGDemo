using Unity.Entities;
using UnityEngine;

public class STGManager : MonoBehaviour
{
    public Mesh mesh;
    public Material material;
    public GameObject prefab;

    EmitterManager _emitterManager;
    // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    void Awake()
    {
        _emitterManager = new EmitterManager();
    }
    // Start is called before the first frame update
    void Start()
    {
        _emitterManager.CreateEmitter<SphereEmitter>(mesh, material);
        // _emitterManager.CreateEmitter<SphereEmitter>(prefab);
    }

}
