using System;
using HarmonyLib;
using luachipmod.Patches;
using NLua;
using StationeersMods.Interface;

public class LuaChipMod : ModBehaviour
{
    public override void OnLoaded(ContentHandler contentHandler)
    {
        UnityEngine.Debug.Log("Hello World!");
        Harmony harmony = new Harmony("LuaChipMod");
        PrefabPatch.prefabs = contentHandler.prefabs;
        harmony.PatchAll();
        UnityEngine.Debug.Log("LuaChipMod Loaded with " + contentHandler.prefabs.Count + " prefab(s)");

        using (Lua lua = new Lua())
        {
            Console.WriteLine($"{(long)lua.DoString("return 2 + 2")[0]}");
        }
    }
}