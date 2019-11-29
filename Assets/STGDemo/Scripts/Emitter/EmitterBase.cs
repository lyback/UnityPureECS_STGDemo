using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using UnityEngine;

public class EmitterBase
{
    protected EntityArchetype entityArchetype;
    public EmitterBase(EntityArchetype _entityArchetype)
    {
        entityArchetype = _entityArchetype;
    }
    public virtual void Create(Mesh mesh, Material mat)
    {

    }
    
    public virtual void Create(GameObject prefab)
    {

    }
}