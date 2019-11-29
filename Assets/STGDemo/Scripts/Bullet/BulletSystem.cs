using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class BulletSystem : JobComponentSystem
{
    // This declares a new kind of job, which is a unit of work to do.
    // The job is declared as an IJobForEach<Translation, Rotation>,
    // meaning it will process all entities in the world that have both
    // Translation and Rotation components. Change it to process the component
    // types you want.
    //
    // The job is also tagged with the BurstCompile attribute, which means
    // that the Burst compiler will optimize it for the best performance.
    [BurstCompile]
    struct BulletSystemJob : IJobForEach<Translation, Rotation, BulletBaseComponent>
    {
        public float DeltaTime;
        public void Execute(ref Translation translation, ref Rotation rotation, [ReadOnly] ref BulletBaseComponent bullet)
        {
            translation.Value += math.normalizesafe(bullet.moveDirection) * bullet.moveSpeed * DeltaTime;
            rotation.Value = quaternion.AxisAngle(bullet.rotAxis, bullet.rotSpeed * DeltaTime);
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new BulletSystemJob();

        job.DeltaTime = UnityEngine.Time.deltaTime;
        
        // Now that the job is set up, schedule it to be run. 
        return job.Schedule(this, inputDependencies);
    }
}