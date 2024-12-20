using HarmonyLib;
using SDG.Framework.Modules;
using SDG.Unturned;

namespace UnityExplorer;

public class ProjectMain : IModuleNexus
{
    ExplorerStandalone explorerStandalone;

    Harmony Harmony = new Harmony("com.unityexplorer.module");

    public void initialize()
    {
        ExplorerStandalone.OnLog += ExplorerStandalone_OnLog;
        explorerStandalone = ExplorerStandalone.CreateInstance();

        Harmony.PatchAll(this.GetType().Assembly);

        CommandWindow.LogWarning("UnityExplorer integration module loaded");
    }

    private void ExplorerStandalone_OnLog(string arg1, UnityEngine.LogType arg2)
    {
        CommandWindow.Log($"[ExplorerStandalone]<{arg2.ToString()}> {arg1}");
    }

    public void shutdown()
    {
        Harmony.UnpatchAll(this.Harmony.Id);

        CommandWindow.Log("UnityExplorer unloaded");
    }
}
