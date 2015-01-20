using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Microsoft.VisualStudio.Text.Editor;

namespace VSXMusic
{
    public class LyricsMargin : IWpfTextViewMargin
    {
        public const string MarginName = "LyricsMargin";

        #region IWpfTextViewMargin 成员

        public System.Windows.FrameworkElement VisualElement
        {
            get { return new TextBox(); }
        }

        #endregion

        #region ITextViewMargin 成员

        public bool Enabled
        {
            get { return true; }
        }

        public ITextViewMargin GetTextViewMargin(string marginName)
        {
            return this;
            
        }

        public double MarginSize
        {
            get { return 25; }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
        }

        #endregion
    }
}
