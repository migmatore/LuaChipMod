using Assets.Scripts.UI;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace testmod.UI
{
    public class WindowTitleBar : UserInterfaceBase
    {
        public void SetTitle(string titleText)
        {
            this.TitleText.text = titleText;
        }

        // Token: 0x04003015 RID: 12309
        public TMP_Text TitleText;

        // Token: 0x04003016 RID: 12310
        public Image Background;
    }
}