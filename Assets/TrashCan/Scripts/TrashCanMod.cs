using HarmonyLib;
using PugMod;
using Unity.Entities;
using UnityEngine;

namespace TrashCan.Scripts
{
    [Harmony]
    public class TrashCanMod : IMod
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public const string MOD_NAME = "Trash Can";

        // ReSharper disable once MemberCanBePrivate.Global
        public const string MOD_VERSION = "0.0.1";

        private bool _isInit;

        private bool _hasSpawned;
        private float _spawnTime;

        // ReSharper disable once MemberCanBePrivate.Global
        public static void TrashCanLog(object message)
        {
            Debug.Log($"[{MOD_NAME}]: {message}");
        }

        public void EarlyInit()
        {
        }

        public void Init()
        {
            if (_isInit) return;
            _isInit = true;
            TrashCanLog("Mod Version: " + MOD_VERSION);
        }

        public void Shutdown()
        {
        }

        public void ModObjectLoaded(Object obj)
        {
        }

        public void Update()
        {
        }

        [HarmonyPrefix, HarmonyPatch(typeof(TrashCanInventoryUI), "TrashItemInSlot")]
        public static void TrashCanInventoryUI_TrashItemInSlot_Patch()
        {
            var player = Manager.main.player;
            if (player == null)
            {
                return;
            }

            var inventoryHandler = player.trashCanHandler.inventoryHandler;
            var objectData = inventoryHandler.GetObjectData(0);

            if (objectData.objectID != ObjectID.InventoryChest) return;
            API.Server.DropObject((int)API.Authoring.GetObjectID("TrashCan:TrashCan"), 0, objectData.amount, player.WorldPosition);
        }
    }
}