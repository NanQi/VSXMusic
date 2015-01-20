using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace VSXMusic
{
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType(VSXMusicConstants.ContentType)]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    public sealed class PlayerAdornment : IWpfTextViewCreationListener
    {
        const string AdornmentName = "PlayerAdornment";

        private MusicPlayer _musicPlayer;
        private IAdornmentLayer _adornmentLayer;
        private IWpfTextView _textView;

        [Export(typeof(AdornmentLayerDefinition))]
        [Name(AdornmentName)]
        [Order(After = PredefinedAdornmentLayers.Selection)]
        [TextViewRole(PredefinedTextViewRoles.Interactive)]
        public AdornmentLayerDefinition editorAdornmentLayer = null;

        [Import]
        public Lazy<IPlayer> Player { get; set; }

        public void TextViewCreated(IWpfTextView textView)
        {
            _textView = textView;
            _adornmentLayer = textView.GetAdornmentLayer(AdornmentName);
            _musicPlayer = new MusicPlayer(Player);

            _textView.ViewportHeightChanged += OnSizeChange;
            _textView.ViewportWidthChanged += OnSizeChange;
        }

        public void OnSizeChange(object sender, System.EventArgs e)
        {
            _adornmentLayer.RemoveAllAdornments();

            Canvas.SetLeft(_musicPlayer, _textView.ViewportRight - 335);
            Canvas.SetTop(_musicPlayer, _textView.ViewportTop + 10);

            _adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, _musicPlayer, null);
        }
    }
}
