using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Assets.Scripts;
using Assets.Scripts.Objects;
using Assets.Scripts.UI;
using Cysharp.Threading.Tasks;
using HarmonyLib;
using luachipmod.UI;
using UnityEngine;
using UnityEngine.UI;

namespace luachipmod.Patches
{
    [HarmonyPatch]
    public class PrefabPatch
    {
        public static ReadOnlyCollection<GameObject> prefabs { get; set; }
       

        [HarmonyPatch(typeof(Prefab), "LoadAll")]
        public static void Prefix()
        {
            try
            {
                Debug.Log("Prefab Patch started");
                foreach (var gameObject in prefabs)
                {
                    Thing thing = gameObject.GetComponent<Thing>();
 
                    // Additional patching goes here, like setting references to materials(colors) or tools from the game
 
                    if (thing != null)
                    {
                        Debug.Log(gameObject.name + " added to WorldManager");
                        WorldManager.Instance.SourcePrefabs.Add(thing);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                Debug.LogException(ex);
            }
        }
        
        [HarmonyPatch(typeof(GameManager), "Start")]
        public static void Postfix(GameManager __instance)
        {
            Task.Run(async delegate
            {
                await UniTask.WaitUntil(() => GameManager.IsInitialized, PlayerLoopTiming.Update,
                    default(CancellationToken));
                GameObject sourceCodeEditorObject = PrefabPatch.prefabs.First((GameObject prefab) => prefab.name.Equals("LuaSourceCodeEditor"));
                Debug.Log("prefab name:" + sourceCodeEditorObject.name);
                LuaSourceCodeEditor sourceCodeEditor = sourceCodeEditorObject.GetComponent<LuaSourceCodeEditor>();
                LuaSourceCodeEditor instantiate = UnityEngine.Object.Instantiate<LuaSourceCodeEditor>(sourceCodeEditor);
                __instance.InputWindows.Add(instantiate);
                instantiate.Initialize();
                
                foreach (InputWindowBase inputWindowBase in __instance.InputWindows)
                { 
                    Debug.Log($"Input window {inputWindowBase.name}");
                }
            });
        }
    }
}