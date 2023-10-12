using Unity.Entities;
using Unity.NetCode;

namespace TrashCan.Scripts
{
    [UpdateInWorld(TargetWorld.Server)]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial class NewTrashCanSystem : PugSimulationSystemBase
    {
        private float _cdTime = 1.0f;

        protected override void OnUpdate()
        {
            _cdTime -= Time.DeltaTime;
            if (_cdTime > 0)
            {
                return;
            }

            _cdTime = 1.0f;

            Entities
                .WithAll<AutomatedTrashCanCD>()
                .WithAll<InventoryCD>()
                .WithAll<ContainedObjectsBuffer>()
                .ForEach((ref DynamicBuffer<ContainedObjectsBuffer> container) =>
                {
                    for (var i = 0; i < container.Length; i++)
                    {
                        container[i] = new ContainedObjectsBuffer();
                    }
                })
                .WithoutBurst()
                .Schedule();

            base.OnUpdate();
        }
    }
}