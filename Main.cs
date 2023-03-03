using BepInEx;
using BepInEx.Unity.IL2CPP;
using Il2CppInterop.Runtime.Attributes;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;

namespace ConsoleToggle
{
    [BepInPlugin("org.fours.plugins.consoletoggle", "ConsoleToggle", "1.0")]
    public class ConsoleToggle : BasePlugin
    {
        public override void Load()
        {
            Log.LogInfo("Loading Console Toggle");

            ClassInjector.RegisterTypeInIl2Cpp<ConsoleToggleComp>();

            GameObject gameObject = new GameObject("FourConsoleToggle");
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            var comp = gameObject.AddComponent<ConsoleToggleComp>();

            comp.OnUpdate += Comp_OnUpdate;

        }

        private void Comp_OnUpdate()
        {

            if (TheForest.DebugConsole.Instance && Input.GetKeyDown(KeyCode.F1))
                TheForest.DebugConsole.Instance.ShowConsole(!TheForest.DebugConsole.Instance._showConsole);
        }

        public class ConsoleToggleComp : MonoBehaviour
        {
            [method: HideFromIl2Cpp]
            public event Action OnUpdate;

            public ConsoleToggleComp(IntPtr obj) : base(obj) { }

            public void Update() => OnUpdate?.Invoke();

        }
    }
}