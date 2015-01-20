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

        private MusicPlayer _player;
        private IAdornmentLayer _adornmentLayer;
        private IWpfTextView _textView;

        [Export(typeof(AdornmentLayerDefinition))]
        [Name(AdornmentName)]
        [Order(After = PredefinedAdornmentLayers.Selection)]
        [TextViewRole(PredefinedTextViewRoles.Interactive)]
        public AdornmentLayerDefinition editorAdornmentLayer = null;

        public void TextViewCreated(IWpfTextView textView)
        {
            _textView = textView;
            _adornmentLayer = textView.GetAdornmentLayer(AdornmentName);
            _player = new MusicPlayer();

            _textView.ViewportHeightChanged += OnSizeChange;
            _textView.ViewportWidthChanged += OnSizeChange;
        }

        public void OnSizeChange(object sender, System.EventArgs e)
        {
            _adornmentLayer.RemoveAllAdornments();

            Canvas.SetLeft(_player, _textView.ViewportRight - 255);
            Canvas.SetTop(_player, _textView.ViewportTop + 10);

            _adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, _player, null);
        }
    }
}
