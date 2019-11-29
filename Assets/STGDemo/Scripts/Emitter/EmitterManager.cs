using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using UnityEngine;
using System;
using System.Collections.Generic;
public class EmitterManager
{
    EntityArchetype BulletArchetype;
    List<EmitterBase> EmitterList = new List<EmitterBase>();
    public EmitterManager()
    {
        BulletArchetype = World.Active.EntityManager.CreateArchetype(
            new ComponentType[]{
                typeof(Translation),
                typeof(Rotation),
                typeof(LocalToWorld),
                typeof(NonUniformScale),
                typeof(BulletBaseComponent),
                typeof(RenderMesh),
            }
        );
    }

    public T CreateEmitter<T>(Mesh mesh, Material mat) where T : EmitterBase
    { 
        T emitter = (T)Activator.CreateInstance(typeof(T), BulletArchetype);
        emitter.Create(mesh, mat);
        EmitterList.Add(emitter);
        return emitter;
    }
    public T CreateEmitter<T>(GameObject prefab) where T : EmitterBase
    { 
        T emitter = (T)Activator.CreateInstance(typeof(T), BulletArchetype);
        emitter.Create(prefab);
        EmitterList.Add(emitter);
        return emitter;
    }
}