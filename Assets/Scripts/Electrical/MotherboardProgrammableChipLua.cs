using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Objects.Electrical;
using Assets.Scripts.Objects.Items;
using Assets.Scripts.Objects.Pipes;
using Cysharp.Threading.Tasks;
using luachipmod.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace luachipmod
{
    public class MotherboardProgrammableChipLua : Motherboard, ISourceCode
    {
        [Header("Programmable Chip Motherboard")]
        private bool _devicesChanged;

        private readonly List<ICircuitHolder> _circuitHolders = new List<ICircuitHolder>();

        private Canvas _templateCanvas;

        [SerializeField] private Dropdown AccessibleProgrammableChipList;

        [SerializeField] private GameObject DropDownTemplate;

        [SerializeField] private Text SourceCode;

        [SerializeField] private Text SourceCodeLines;

        [SerializeField] private Camera ScreenshotCamera;

        [SerializeField] private Text ScreenTitle;

        [SerializeField] private GameObject ChipList;

        [SerializeField] private GameObject ImportButton;

        [SerializeField] private GameObject ExportButton;

        public void SendUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void SetSourceCode(string sourceCode)
        {
            throw new System.NotImplementedException();
        }

        public AsciiString GetSourceCode()
        {
            throw new System.NotImplementedException();
        }

        public char[] SourceCodeCharArray { get; set; }
        public int SourceCodeWritePointer { get; set; }

        protected async UniTaskVoid HandleDeviceListChange()
        {
            List<ILogicable> devices = this.ParentComputer.DeviceList();
            devices.Sort((ILogicable a, ILogicable b) => a.DisplayName.CompareTo(b.DisplayName));

            this.AccessibleProgrammableChipList.options.Clear();
            this._circuitHolders.Clear();

            List<ILogicable>.Enumerator enumerator = devices.GetEnumerator();

            try
            {
                while (enumerator.MoveNext())
                {
                    ILogicable logicable = enumerator.Current;
                    ICircuitHolder circuitHolder = logicable as ICircuitHolder;

                    if (circuitHolder != null)
                    {
                        this.AccessibleProgrammableChipList.options.Add(new Dropdown.OptionData(logicable.DisplayName));
                        this._circuitHolders.Add(circuitHolder);
                    }
                }
            }
            finally
            {
                enumerator.Dispose();
            }

            this.AccessibleProgrammableChipList.RefreshShownValue();
            this._devicesChanged = false;

            await UniTask.Yield();
        }

        // Token: 0x060052B0 RID: 21168 RVA: 0x001A14D8 File Offset: 0x0019F6D8
        public override void OnDeviceListChanged()
        {
            base.OnDeviceListChanged();
            if (!this._devicesChanged)
            {
                this._devicesChanged = true;
                this.HandleDeviceListChange().Forget();
            }
        }

        public void OnEdit()
        {
            Debug.Log("Enter on Edit");
            Debug.Log("Set PCM instance");
            LuaSourceCodeEditor.Instance.MotherboardProgrammableChipLua = this;
            Debug.Log("Show input panel");
            if (LuaSourceCodeEditor.ShowInputPanel("Edit Script", "test", TMP_InputField.ContentType.Standard))
            {
                Debug.Log("show input panel true");
                LuaSourceCodeEditor.OnSubmit += this.InputFinished;
            }
            else
            {
                Debug.Log("show input panel false");
            }
        }

        public void InputFinished(string result)
        {
            this.SetSourceCode(result);
            this.SendUpdate();
            LuaSourceCodeEditor.Instance.MotherboardProgrammableChipLua = null;
        }
    }
}