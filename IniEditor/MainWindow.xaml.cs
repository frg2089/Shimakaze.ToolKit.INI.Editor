﻿using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using IniEditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace IniEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ReLoadTheme();

            Editor.Text = "[Section]\nKey = Value; Summary";
            Editor.Document.Changed += Document_Changed;
            Editor.MouseMove += (o, e) => Method();
            Editor.KeyUp += (o, e) => Method();
            Editor.TextArea.TextEntering += TextArea_TextEntering;
            Editor.TextArea.TextEntered += TextArea_TextEntered; ;
        }

        private void TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("TextEntered  " + Editor.GetCursorWord());
            Editor.ShowCompletionWindow(Editor.GetCursorWord());
        }
        private void TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("TextEntering " +e.Text);
        }

        private void Document_Changed(object sender, ICSharpCode.AvalonEdit.Document.DocumentChangeEventArgs e)
        {
            Method();
        }

        void ReLoadTheme()
        {
            using (XmlTextReader reader = new XmlTextReader("IniRule.xml"))
                Editor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
        }

        void Method()
        {
            var line = Editor.TextArea.Document.GetLineByOffset(Editor.CaretOffset);
            tbLine.Text = Editor.TextArea.Document.GetText(line.Offset, line.Length);
        }
    }
}
