using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine;

public class SphereEmitter : EmitterBase
{
    public SphereEmitter(EntityArchetype _entityArchetype) : base(_entityArchetype)
    {

    }

    public override void Create(Mesh mesh, Material mat)
    {
        var entityManager = World.Active.EntityManager;
        int count = 10000;
        Translation translationZero = new Translation { Value = float3.zero };
        Rotation rotation = new Rotation { Value = quaternion.identity };
        NonUniformScale scale = new NonUniformScale { Value = new float3(0.1f, 0.1f, 0.1f) };
        RenderMesh renderMesh = new RenderMesh
        {
            mesh = mesh,
            material = mat,
        };
        for (int i = 0; i < count; i++)
        {
            var entity = entityManager.CreateEntity(entityArchetype);
            entityManager.SetComponentData(entity, translationZero);
            entityManager.SetComponentData(entity, rotation);
            entityManager.SetComponentData(entity, scale);
            entityManager.SetComponentData(entity, new BulletBaseComponent
            {
                moveSpeed = 1f,
                moveDirection = CreatPointOnSphere(count, 1, i),
                rotSpeed = 0.1f,
                rotAxis = math.up()
            });
            entityManager.SetSharedComponentData(entity, renderMesh);
        }
    }

    public override void Create(GameObject prefab)
    {
        float3 dir = new float3(1, 0, 0);
        var entityManager = World.Active.EntityManager;
        Entity entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefab, World.Active);
        int count = 50000;
        for (int i = 0; i < count; i++)
        {
            var instance = entityManager.Instantiate(entity);
            entityManager.SetComponentData(instance, new Translation { Value = float3.zero });
            entityManager.SetComponentData(instance, new Rotation { Value = quaternion.identity });
            entityManager.AddComponentData<BulletBaseComponent>(instance, new BulletBaseComponent
            {
                moveSpeed = 1f,
                moveDirection = CreatPointOnSphere(count, 1, i),
                rotSpeed = 0.1f,
                rotAxis = math.up()
            });
        }
        entityManager.DestroyEntity(entity);
    }
    float3 CreatPointOnSphere(int _wBornPointSum, float _dwDectRadius, int i)
    {
        //生成
        float inc = Mathf.PI * (3.0f - Mathf.Sqrt(5.0f));
        float off = 2.0f / _wBornPointSum;   //注意保持数值精度  m_wBornPointSum：生成的点数
        float y;
        float r;
        float phi;

        y = (float)i * off + (off / 2.0f) - 1.0f;
        r = math.sqrt(1.0f - y * y);
        phi = i * inc;
        return new float3(math.cos(phi) * r * _dwDectRadius, y * _dwDectRadius, math.sin(phi) * r * _dwDectRadius); //m_dwDectRadius  距离球心的距离
    }
}