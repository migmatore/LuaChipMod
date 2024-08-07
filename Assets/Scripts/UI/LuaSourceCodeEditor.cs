using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Assets.Scripts;
using Assets.Scripts.Objects.Electrical;
using Assets.Scripts.Objects.Motherboards;
using Assets.Scripts.UI;
using Assets.Scripts.Util;
using Cysharp.Threading.Tasks;
using LeTai.Asset.TranslucentImage;
using TMPro;
using UI.Tooltips;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace luachipmod.UI
{
    public class LuaSourceCodeEditor : InputWindowBase
    {
        public static LuaSourceCodeEditor Instance;
        
        public TextMeshProUGUI TitleText;
        
        public Canvas Canvas;

        [SerializeField] private Toggle _pauseToggle;

        public LuaEditorLineOfCode LineOfCodePrefab;

        public RectTransform LineParent;

        public TranslucentImage Background;

        [SerializeField] private UiComponentRenderer CodeInputWindow;

        public static InputPanelState InputState;

        public RectTransform Window;

        public Button SubmitButton;

        public MotherboardProgrammableChipLua MotherboardProgrammableChipLua;

        //public List<ScriptHelpWindow> HelpWindows = new List<ScriptHelpWindow>();

        public List<LuaEditorLineOfCode> LinesOfCode;
        
        public const int LINE_LENGTH_LIMIT = 90;

        private float _defaultWidth;

        private bool _isOpaque = true;

        private int _acceptedStringsCount;

        private int _previousCaretPosition;

        private const char CHAR_ZERO_WIDTH_SPACE = '\u200b';

        private const char CHAR_SPACE = ' ';

        private int _fileSize;

        private bool _editorIsFocused;

        public delegate void InputEvent(string result);
        
        public static event LuaSourceCodeEditor.InputEvent OnSubmit;

        public static event UserInterfaceBase.Event OnCancel;

        public void Awake()
        {
            this._defaultWidth = this.Window.sizeDelta.x;
        }

        private void Start()
        {
            WorldManager.OnPaused += this.WorldManagerOnPaused;
            this._pauseToggle.onValueChanged.AddListener(new UnityAction<bool>(WorldManager.SetGamePause));
        }
        
        public void Update()
        {
            if (LuaSourceCodeEditor.InputState == InputPanelState.Waiting && this._editorIsFocused)
            {
                this.HandleInput();
            }
            
            this._editorIsFocused = this.CurrentLine.InputField.isFocused;
            // UITooltipCanvas.Instance.DoUpdate();
            if ((float)Screen.width < this.Window.sizeDelta.x)
            {
                this.ClampWindowSize();
            }
        }

        protected override void CloseOnClientConnected(bool isPaused, string message)
        {
            if (isPaused && this.IsVisible)
            {
                LuaSourceCodeEditor.Instance.ButtonInputSubmit();
            }
        }

        private void WorldManagerOnPaused(bool isOn)
        {
            this._pauseToggle.isOn = isOn;
        }

        public override void SetVisible(bool isVisble)
        {
            base.SetVisible(isVisble);
            this.Canvas.enabled = isVisble;
            if (!isVisble && this._pauseToggle.isOn)
            {
                this._pauseToggle.isOn = false;
            }

            this.ClampWindowSize();
        }

        private void ClampWindowSize()
        {
            float num = Mathf.Min((float)Screen.width, this._defaultWidth);
            if (num < 800f)
            {
                num = 800f;
            }

            this.Window.sizeDelta = new Vector2(num, this.Window.sizeDelta.y);
        }

        public override void SetActive(bool active)
        {
            base.SetActive(active);
            this.Canvas.enabled = active;
        }

        public override bool IsVisible
        {
            get { return this.Canvas.enabled; }
        }

        private LuaEditorLineOfCode CurrentLine
        {
            get { return LuaEditorLineOfCode.CurrentLine; }
            set { LuaEditorLineOfCode.CurrentLine = value; }
        }

        private int CaretPosition
        {
            get
            {
                if (!(this.CurrentLine == null))
                {
                    return this.CurrentLine.InputField.caretPosition;
                }

                return 0;
            }
            set
            {
                if (this.CurrentLine != null)
                {
                    this.CurrentLine.InputField.caretPosition = value;
                }
            }
        }
        
        // private static void SortLines()
        // {
        //     SourceCodeEditor.Instance.LinesOfCode.Sort((CodeEditorLineOfCode x, CodeEditorLineOfCode y) => x.CompareTo(y));
        //     for (int i = 0; i < SourceCodeEditor.Instance.LinesOfCode.Count; i++)
        //     {
        //         SourceCodeEditor.Instance.LinesOfCode[i].LineNumber.text = string.Format("{0}.", i);
        //     }
        // }

        public override void Initialize()
        {
            base.Initialize();
            this.CodeInputWindow.gameObject.SetActive(true);
            LuaSourceCodeEditor.Instance = this;
            this.SetVisible(false);
            Debug.Log("Finish LuaSourceCodeEditor init");
            // this.BackGroundFadeImage.enabled = true;
            this.LinesOfCode = new List<LuaEditorLineOfCode>(1);
            for (int i = 0; i < 1; i++)
            {
                LuaEditorLineOfCode luaEditorLineOfCode =
                    global::UnityEngine.Object.Instantiate<LuaEditorLineOfCode>(this.LineOfCodePrefab, this.LineParent);
                luaEditorLineOfCode.Parent = this;
                luaEditorLineOfCode.LineNumber.text = string.Format("{0}.", i);
                luaEditorLineOfCode.name = string.Format("~LineOfCode_{0}", i);
                this.LinesOfCode.Add(luaEditorLineOfCode);
                //luaEditorLineOfCode.GetComponent<TMP_InputField>().characterLimit = 90;
            }
            this.CurrentLine = this.LinesOfCode[0];
            
            //
            // if (this.LinesOfCode.Count > 0)
            // {
            //     this.CurrentLine = this.LinesOfCode[0];
            // }
            //
            // SourceCodeEditor.SortLines();
            // this.UpdateFileSize();
            // this.SetVisible(false);
            // foreach (ScriptHelpWindow scriptHelpWindow in this.HelpWindows)
            // {
            //     scriptHelpWindow.Initialize();
            // }
        }

        // public void OnSave()
        // {
        //     if (SourceCodeEditor.Instance.PCM == null)
        //     {
        //         return;
        //     }
        //
        //     SourceCodeEditor.Instance.PCM.SetSourceCode(SourceCodeEditor.Copy());
        //     SourceCodeEditor.Instance.SetVisible(false);
        //     CursorManager.SetCursor(true);
        // }

        public static void CancelInput()
        {
            LuaSourceCodeEditor.Instance.ButtonInputCancel();
        }
        
        public static void SubmitInput()
        {
            LuaSourceCodeEditor.Instance.ButtonInputSubmit();
        }
        
        // public void ButtonCopyToClipboard()
        // {
        //     GameManager.Clipboard = LuaSourceCodeEditor.Copy();
        // }
        
        // public void ButtonPasteFromClipboard()
        // {
        //     SourceCodeEditor.Paste(GameManager.Clipboard);
        // }
        
        // public static void Paste(string value)
        // {
        //     SourceCodeEditor.Instance.AcceptedStrings = new List<string>();
        //     if (string.IsNullOrEmpty(value))
        //     {
        //         value = string.Empty;
        //     }
        //
        //     value = value.TrimEnd();
        //     string[] array = value.Split('\n', StringSplitOptions.None);
        //     for (int i = 0; i < SourceCodeEditor.Instance.LinesOfCode.Count; i++)
        //     {
        //         CodeEditorLineOfCode CodeEditorLineOfCode = SourceCodeEditor.Instance.LinesOfCode[i];
        //         string text = ((i >= array.Length) ? string.Empty : array[i].TrimEnd());
        //         text = AsciiString.ParseLine(text, 90);
        //         CodeEditorLineOfCode.InputField.text = text;
        //         //CodeEditorLineOfCode.ReformatText(text);
        //     }
        //
        //     SourceCodeEditor.Instance.UpdateFileSize();
        // }

        // public static string Copy()
        // {
        //     StringBuilder stringBuilder = new StringBuilder();
        //     foreach (CodeEditorLineOfCode CodeEditorLineOfCode in SourceCodeEditor.Instance.LinesOfCode)
        //     {
        //         stringBuilder.AppendLine(AsciiString.ParseLine(CodeEditorLineOfCode.InputField.text, 90));
        //     }
        //
        //     string text = stringBuilder.ToString();
        //     if (text.Length > 4096)
        //     {
        //         text = text.Substring(0, 4096);
        //     }
        //
        //     return text.TrimEnd();
        // }

        private bool IsOpaque
        {
            get { return this._isOpaque; }
            set
            {
                this._isOpaque = value;
                this.Background.color = this.Background.color.SetAlpha(this._isOpaque ? 1f : 0.75f);
                this.Background.spriteBlending = (this._isOpaque ? 1f : 0.65f);
            }
        }

        public void ButtonOpacity()
        {
            this.IsOpaque = !this.IsOpaque;
        }

        public static void DeleteInstruction(string directoryName)
        {
            string text = Path.Combine(StationSaveUtils.GetSavePathScriptsSubDir().FullName, directoryName);
            if (Directory.Exists(text))
            {
                List<string> list = Directory.GetDirectories(text).ToList<string>();
                list.Add(text);
                foreach (string text2 in list)
                {
                    string[] files = Directory.GetFiles(text2);
                    for (int i = 0; i < files.Length; i++)
                    {
                        File.Delete(files[i]);
                    }

                    Directory.Delete(text2);
                }

                ScriptHelpWindow.ScriptLibraryWindow.ReloadFileList();
            }
        }
        
        // public void ButtonClear()
        // {
        //     SourceCodeEditor.Paste(string.Empty);
        // }

        public void ButtonInputCancel()
        {
            CursorManager.SetCursor(true);
            LuaSourceCodeEditor.InputState = InputPanelState.Cancelled;
            LuaSourceCodeEditor.Instance.SetVisible(false);
            if (LuaSourceCodeEditor.OnCancel != null)
            {
                LuaSourceCodeEditor.OnCancel = null;
            }

            LuaSourceCodeEditor.OnCancel = null;
            LuaSourceCodeEditor.OnSubmit = null;
            LuaSourceCodeEditor.Instance.MotherboardProgrammableChipLua = null;
            LuaSourceCodeEditor.InputState = InputPanelState.None;
        }

        public void ButtonInputSubmit()
        {
            CursorManager.SetCursor(true);
            LuaSourceCodeEditor.InputState = InputPanelState.Submitted;
            LuaSourceCodeEditor.Instance.SetVisible(false);
            if (LuaSourceCodeEditor.OnSubmit != null)
            {
                //LuaSourceCodeEditor.OnSubmit(LuaSourceCodeEditor.Copy());
            }

            LuaSourceCodeEditor.OnCancel = null;
            LuaSourceCodeEditor.OnSubmit = null;
            LuaSourceCodeEditor.Instance.MotherboardProgrammableChipLua = null;
            LuaSourceCodeEditor.InputState = InputPanelState.None;
        }

        public static string GetLineText(string sourceCode)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < LuaSourceCodeEditor.CountLines(sourceCode); i++)
            {
                stringBuilder.Append(StringManager.Get(i));
                stringBuilder.Append(".\n");
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        private static int CountLines(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str == string.Empty)
            {
                return 0;
            }

            int num = -1;
            int num2 = 0;
            while (-1 != (num = str.IndexOf("\n", num + 1, StringComparison.Ordinal)))
            {
                num2++;
            }

            return num2 + 1;
        }

        public static bool ShowInputPanel(string title, string defaultText,
            TMP_InputField.ContentType contentType = TMP_InputField.ContentType.Standard)
        {
            if (LuaSourceCodeEditor.InputState != InputPanelState.None)
            {
                return false;
            }

            CursorManager.SetCursor(false);
            LuaSourceCodeEditor.Instance.CodeInputWindow.SetVisible(true);
            LuaSourceCodeEditor.InputState = InputPanelState.Waiting;
            LuaSourceCodeEditor.Instance.SetVisible(true);
            LuaSourceCodeEditor.Instance.TitleText.text = title;
            //SourceCodeEditor.Paste(defaultText);
            //SourceCodeEditor.Instance.InputType.text = contentType.ToString().ToProper() + " Input";
            LuaSourceCodeEditor.WaitForInput().Forget();
            return true;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            UITooltipCanvas instance = UITooltipCanvas.Instance;
            if (instance == null)
            {
                return;
            }

            instance.DoUpdate();
        }

        // Token: 0x06003874 RID: 14452 RVA: 0x001132B0 File Offset: 0x001114B0
        private void HandleInput()
        {
            LuaEditorLineOfCode currentLine = this.CurrentLine;
            if (Input.GetKeyDown(KeyCode.Home))
            {
                this.CaretPosition = 0;
            }
            
            if (Input.GetKeyDown(KeyCode.End))
            {
                this.CaretPosition = this.CurrentLine.Text.Length;
            }
            
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                this.CaretPosition = this._previousCaretPosition;
                int caretPosition = this.CaretPosition;
                string text = this.CurrentLine.Text;
                if (caretPosition < 0)
                {
                    return;
                }
            
                string text2 = text.Substring(0, caretPosition);
                string text3 = text.Substring(caretPosition);
                text3 = text3.Replace('\u200b', ' ');
                //int num = currentLine.GetIndex() + 1;
                LuaEditorLineOfCode luaEditorLineOfCode = global::UnityEngine.Object.Instantiate<LuaEditorLineOfCode>(this.LineOfCodePrefab, this.LineParent);
                luaEditorLineOfCode.Parent = this;
                luaEditorLineOfCode.LineNumber.text = $"{LinesOfCode.Count}.";
                luaEditorLineOfCode.name = $"~LineOfCode_{LinesOfCode.Count}";
                this.LinesOfCode.Add(luaEditorLineOfCode);
                //luaEditorLineOfCode.RectTransform.SetSiblingIndex(num);
                this.CurrentLine.Text = text2;
                //this.CurrentLine.ReformatText();
                this.CurrentLine = luaEditorLineOfCode;
                this.CurrentLine.Text = text3;
                //this.UpdateFileSize();
                //LuaSourceCodeEditor.SortLines();
            }
            
            if (Input.GetKeyDown(KeyCode.Backspace) && this._previousCaretPosition == 0)
            {
                int num2 = this.CurrentLine.GetIndex();
                if (num2 <= 0)
                {
                    return;
                }
            
                this.CurrentLine = this.LinesOfCode[num2 - 1];
                this.CaretPosition = this.CurrentLine.Text.Length;
                num2 = this.CurrentLine.GetIndex();
                LuaEditorLineOfCode currentLine2 = this.CurrentLine;
                currentLine2.Text += this.LinesOfCode[num2 + 1].Text;
                if (this.CurrentLine.Text.Length > 90)
                {
                    this.CurrentLine.Text = this.CurrentLine.Text.Substring(0, 90);
                }
            
                this.LinesOfCode[num2 + 1].Text = null;
                //this.RemoveLine(1);
            }
            
            if (Input.GetKeyDown(KeyCode.Delete) && this.CaretPosition >= this.CurrentLine.Text.Length - 1)
            {
                int index = this.CurrentLine.GetIndex();
                if (index >= 127)
                {
                    return;
                }
            
                LuaEditorLineOfCode currentLine3 = this.CurrentLine;
                currentLine3.Text += this.LinesOfCode[index + 1].Text;
                if (this.CurrentLine.Text.Length > 90)
                {
                    this.CurrentLine.Text = this.CurrentLine.Text.Substring(0, 90);
                }
            
                this.LinesOfCode[index + 1].Text = null;
                //this.RemoveLine(1);
            }
            
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                int num3 = this.CurrentLine.GetIndex();
                num3++;
                if (num3 < this.LinesOfCode.Count)
                {
                    this.CurrentLine = this.LinesOfCode[num3];
                    this.CaretPosition = this._previousCaretPosition;
                    return;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                int num4 = this.CurrentLine.GetIndex();
                num4--;
                if (num4 >= 0)
                {
                    this.CurrentLine = this.LinesOfCode[num4];
                    this.CaretPosition = this._previousCaretPosition;
                    return;
                }
            }
            
            // if (this.AcceptedStrings.Count != this._acceptedStringsCount)
            // {
            //     this._acceptedStringsCount = this.AcceptedStrings.Count;
            //     this.ForceRefresh();
            // }

            this._previousCaretPosition = this.CaretPosition;
            foreach (LuaEditorLineOfCode luaEditorLineOfCode in this.LinesOfCode)
            {
                luaEditorLineOfCode.HandleUpdate();
            }
        }

        // public void UpdateFileSize()
        // {
        //     this._fileSize = 0;
        //     int num = 0;
        //     for (int i = this.LinesOfCode.Count - 1; i >= 0; i--)
        //     {
        //         num = i;
        //         if (this.LinesOfCode[i].Text.Length > 0)
        //         {
        //             break;
        //         }
        //     }
        //
        //     for (int j = 0; j < this.LinesOfCode.Count; j++)
        //     {
        //         CodeEditorLineOfCode CodeEditorLineOfCode = this.LinesOfCode[j];
        //         this._fileSize += CodeEditorLineOfCode.Text.Length;
        //         if (j < this.LinesOfCode.Count - 1 && j < num)
        //         {
        //             this._fileSize++;
        //             this._fileSize++;
        //         }
        //     }
        //
        //     this.SizeText.text =
        //         GameStrings.CodeEditorFileSize.AsString(StringManager.Get(this._fileSize), StringManager.Get(4096));
        //     this.SizeText.color = ((this._fileSize > 4096) ? Color.red : Color.white);
        //     this.SubmitButton.interactable = this._fileSize <= 4096;
        // }

        // private void RemoveLine(int direction)
        // {
        //     CodeEditorLineOfCode currentLine = CodeEditorLineOfCode.CurrentLine;
        //     if (currentLine == null)
        //     {
        //         return;
        //     }
        //
        //     int num = currentLine.GetIndex() + direction;
        //     if (num < 0 || num >= 128)
        //     {
        //         return;
        //     }
        //
        //     CodeEditorLineOfCode codeEditorLineOfCode = this.LinesOfCode[num];
        //     if (codeEditorLineOfCode == null)
        //     {
        //         return;
        //     }
        //
        //     if (!string.IsNullOrEmpty(codeEditorLineOfCode.InputField.text.TrimEnd()))
        //     {
        //         return;
        //     }
        //
        //     codeEditorLineOfCode.RectTransform.SetAsLastSibling();
        //     //this.UpdateFileSize();
        //     SourceCodeEditor.SortLines();
        // }

        private static async UniTask WaitForInput()
        {
            while (LuaSourceCodeEditor.InputState == InputPanelState.Waiting)
            {
                await UniTask.NextFrame();
            }

            LuaSourceCodeEditor.InputState = InputPanelState.None;
        }

        private void ForceRefresh()
        {
            //this.AcceptedStrings.Clear();
            //this.AcceptedJumps.Clear();
            //Localization.ParseDefines(this.LinesOfCode, ref this.AcceptedStrings, ref this.AcceptedJumps);
            foreach (LuaEditorLineOfCode luaEditorLineOfCode in this.LinesOfCode)
            {
                if (luaEditorLineOfCode.IsUsingReference)
                {
                    string text = luaEditorLineOfCode.InputField.text;
                    text = Regex.Replace(text, "([<>])", "<noparse>$1</noparse>");
                    // luaEditorLineOfCode.FormattedText.text = Localization.ParseScript(text, ref this.AcceptedStrings,
                    //     ref this.AcceptedJumps, null);
                }
            }
        }
    }
}
