using UnityEngine;
using UnityEngine.UI;

namespace luachipmod.UI
{
    public class ImageToggle : GameBase
    {
        // Token: 0x1700000A RID: 10
        // (get) Token: 0x06000074 RID: 116 RVA: 0x00006CC8 File Offset: 0x00004EC8
        public bool Hidden
        {
            get
            {
                return (double)this.ImageComponent.color.a == 0.0;
            }
        }

        // Token: 0x06000075 RID: 117 RVA: 0x00006CF8 File Offset: 0x00004EF8
        public void SetImage(int index)
        {
            bool flag = index >= this.Sprites.Length || this.Sprites[index] == null;
            if (!flag)
            {
                this.ImageComponent.sprite = this.Sprites[index];
            }
        }

        // Token: 0x06000076 RID: 118 RVA: 0x00006D3C File Offset: 0x00004F3C
        public void HideImage(bool isHidden = true)
        {
            bool flag = isHidden == this.Hidden;
            if (!flag)
            {
                this.ImageComponent.color = (isHidden ? Color.clear : Color.white);
            }
        }

        // Token: 0x040000E7 RID: 231
        public Sprite[] Sprites = new Sprite[2];

        // Token: 0x040000E8 RID: 232
        public Image ImageComponent;
    }
}