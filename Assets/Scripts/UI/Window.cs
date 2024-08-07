using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI;
using UnityEngine;

namespace testmod.UI
{
    public class Window : UserInterfaceBase, IWindow
    {
        public static event Window.WindowVisibleEvent onVisibilityChanged;

        // Token: 0x06003C1B RID: 15387 RVA: 0x00129077 File Offset: 0x00127277
        public override void SetVisible(bool isVisble)
        {
            base.SetVisible(isVisble);
            this.InvokeOnVisibilityChanged(isVisble);
        }

        // Token: 0x06003C1C RID: 15388 RVA: 0x00129087 File Offset: 0x00127287
        protected void InvokeOnVisibilityChanged(bool isVisible)
        {
            Window.WindowVisibleEvent windowVisibleEvent = Window.onVisibilityChanged;
            if (windowVisibleEvent == null)
            {
                return;
            }
            windowVisibleEvent(this, isVisible);
        }

        // Token: 0x170009C8 RID: 2504
        // (get) Token: 0x06003C1D RID: 15389 RVA: 0x0012909A File Offset: 0x0012729A
        public int ZLayer
        {
            get
            {
                return base.GetComponent<Canvas>().sortingOrder;
            }
        }

        // Token: 0x06003C1E RID: 15390 RVA: 0x001290A8 File Offset: 0x001272A8
        public void SetZLayer(int layer)
        {
            Canvas component = base.GetComponent<Canvas>();
            if (component)
            {
                component.sortingOrder = layer;
            }
        }

        // Token: 0x02000FE5 RID: 4069
        // (Invoke) Token: 0x0600791A RID: 31002
        public delegate void WindowVisibleEvent(Window window, bool isVisible);
    }
}
