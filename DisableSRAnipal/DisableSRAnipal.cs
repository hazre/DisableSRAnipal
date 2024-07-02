using Elements.Core;
using HarmonyLib;
using ResoniteModLoader;

namespace DisableSRAnipal
{
    public class DisableSRAnipal : ResoniteMod
    {
        [AutoRegisterConfigKey]
        public static ModConfigurationKey<bool> DISABLE_SRANIPAL = new("disable_sranipal", "Should SRAnipal be disabled? (Requires restart) ", () => false);
        public static ModConfiguration config;
        public override string Name => "DisableSRAnipal";
        public override string Author => "hazre";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/hazre/DisableSRAnipal/";
        public override void OnEngineInit()
        {
            config = GetConfiguration();
            Harmony harmony = new Harmony("dev.hazre.DisableSRAnipal");
            harmony.PatchAll();

        }
        [HarmonyPatch(typeof(ViveProEyeTrackingDriver), "ShouldNotRegister", MethodType.Getter)]
        class DisableSRAnipalPatch
        {
            public static bool Prefix(ref bool __result)
            {
                if (config.GetValue(DISABLE_SRANIPAL))
                {
                    UniLog.Log("Disabling SRAnipal");
                    __result = true;
                    return false;
                }
                return true;
            }
        }
    }
}