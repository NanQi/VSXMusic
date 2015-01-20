using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace VSXMusic
{
    [Export(typeof(IWpfTextViewMarginProvider))]
    [ContentType(VSXMusicConstants.ContentType)]
    [MarginContainer(PredefinedMarginNames.Bottom)]
    [Name(LyricsMargin.MarginName)]
    [TextViewRole(PredefinedTextViewRoles.Editable)]
    public sealed class LyricsMarginProvider : IWpfTextViewMarginProvider 
    {
        IWpfTextViewMargin IWpfTextViewMarginProvider.CreateMargin(IWpfTextViewHost wpfTextViewHost, IWpfTextViewMargin marginContainer)
        {
            return new LyricsMargin();
        }
    }
}
