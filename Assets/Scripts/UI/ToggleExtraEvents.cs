using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace luachipmod.UI
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleExtraEvents : MonoBehaviour
    {
        // Token: 0x0600010F RID: 271 RVA: 0x00009CE3 File Offset: 0x00007EE3
        private void Start()
        {
            this._toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleChanged));
        }

        // Token: 0x06000110 RID: 272 RVA: 0x00009D04 File Offset: 0x00007F04
        private void OnToggleChanged(bool isOn)
        {
            if (isOn)
            {
                UnityEvent onEvent = this.OnEvent;
                if (onEvent != null)
                {
                    onEvent.Invoke();
                }
            }
            else
            {
                UnityEvent offEvent = this.OffEvent;
                if (offEvent != null)
                {
                    offEvent.Invoke();
                }
            }
        }

        // Token: 0x04000151 RID: 337
        [SerializeField]
        private Toggle _toggle;

        // Token: 0x04000152 RID: 338
        public UnityEvent OnEvent;

        // Token: 0x04000153 RID: 339
        public UnityEvent OffEvent;
    }
}