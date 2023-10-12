using PugConversion;
using Unity.Entities;
using UnityEngine;

namespace TrashCan.Scripts
{
    // ReSharper disable once InconsistentNaming
    public struct AutomatedTrashCanCD : IComponentData
    {
    }

    // ReSharper disable once InconsistentNaming
    public class TrashCanCDAuthoring : MonoBehaviour
    {
    }

    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedType.Global
    public class TrashCanCDConverter : SingleAuthoringComponentConverter<TrashCanCDAuthoring>
    {
        protected override void Convert(TrashCanCDAuthoring authoring)
        {
            AddComponentData(new AutomatedTrashCanCD());
        }
    }
}