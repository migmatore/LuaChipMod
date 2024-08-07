using System;
using System.Text;
using Assets.Scripts;
using Assets.Scripts.Objects.Electrical;
using Assets.Scripts.Objects.Motherboards;
using Assets.Scripts.UI;
using Assets.Scripts.Util;
using TMPro;
using UI.Tooltips;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace luachipmod.UI
{
    public class LuaEditorLineOfCode: UserInterfaceBase, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler,
        IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IComparable<EditorLineOfCode>

    {
        public string Text
        {
            get { return this.InputField.text; }
            set { this.InputField.text = value; }
        }
        
        private void Awake()
        {
            this.InputField.onValueChanged.AddListener(new UnityAction<string>(this.TextChanged));
        }
        
        public void TextChanged(string text)
        {
            this.ReformatText();
        }
        
        public void ReformatText()
        {
            string text = this.InputField.text;
            text = text.Replace("\n", string.Empty);
            this.InputText.text = text;
            //this.ReformatText(text);
            //this.Parent.UpdateFileSize();
        }
        
        public int GetIndex()
        {
            return this.RectTransform.GetSiblingIndex();
        }
        
        public static LuaEditorLineOfCode CurrentLine
        {
            get { return LuaEditorLineOfCode._currentLine; }
            set
            {
                if (LuaEditorLineOfCode._currentLine == value)
                {
                    return;
                }

                LuaEditorLineOfCode._currentLine = value;
                if (LuaEditorLineOfCode._currentLine == null)
                {
                    return;
                }

                LuaEditorLineOfCode._currentLine.InputField.ActivateInputField();
            }
        }
        
        public new void OnPointerEnter(PointerEventData eventData)
        {
        }

        public new void OnPointerExit(PointerEventData eventData)
        {
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            LuaEditorLineOfCode.CurrentLine = this;
        }

        public int CompareTo(EditorLineOfCode other)
        {
            if (other.RectTransform.GetSiblingIndex() < this.RectTransform.GetSiblingIndex())
            {
                return 1;
            }

            if (other.RectTransform.GetSiblingIndex() > this.RectTransform.GetSiblingIndex())
            {
                return -1;
            }

            return 0;
        }

        public void Activate()
        {
            this.InputField.ActivateInputField();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            LuaEditorLineOfCode.CurrentLine = this;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
        }

        public void HandleUpdate()
        {
            Vector3 mousePosition = Input.mousePosition;
            if (!RectTransformUtility.RectangleContainsScreenPoint(this.FormattedText.rectTransform, mousePosition))
            {
                return;
            }

            int num = TMP_TextUtilities.FindIntersectingWord(this.FormattedText, Input.mousePosition, null);
            if (num == -1)
            {
                return;
            }

            string word = this.FormattedText.textInfo.wordInfo[num].GetWord();
            if (num == 0)
            {
                foreach (ScriptCommand scriptCommand in EnumCollections.ScriptCommands.Values)
                {
                    if (!LogicBase.IsDeprecated(scriptCommand))
                    {
                        string name = EnumCollections.ScriptCommands.GetName(scriptCommand, false);
                        if (word.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                        {
                            StringBuilder stringBuilder = new StringBuilder();
                            string commandExample = ProgrammableChip.GetCommandExample(scriptCommand, "yellow", -1);
                            stringBuilder.Append("<color=white>").Append("<b>").Append("Instruction")
                                .AppendLine("</b></color>");
                            stringBuilder.Append("<i>").Append(commandExample).AppendLine("</i>");
                            StringManager.WrapLineLength(stringBuilder,
                                ProgrammableChip.GetCommandDescription(scriptCommand), 70, "grey");
                            UITooltipManager.SetTooltip(stringBuilder);
                            return;
                        }
                    }
                }
            }
        }

        private const string _cr = "\n";

        public TMP_InputField InputField;

        public TextMeshProUGUI InputText;

        public TextMeshProUGUI FormattedText;

        public TextMeshProUGUI LineNumber;

        public bool IsUsingReference;

        public bool IsVoidLine;

        public bool WaitToClear;

        private static LuaEditorLineOfCode _currentLine;

        public LuaSourceCodeEditor Parent;
    }
}