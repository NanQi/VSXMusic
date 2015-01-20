using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;

namespace VSXMusic
{
    /// <summary>
    /// Interaction logic for ucInfoBox.xaml
    /// </summary>
    public partial class MusicPlayer : UserControl
    {
        public MusicPlayer(Lazy<IPlayer> player)
        {
            InitializeComponent();
            this.DataContext = new MusicPlayerViewModel(player);
        }
    }

    public class MusicPlayerViewModel
    {
        Lazy<IPlayer> _player;

        public IPlayer Player
        {
            get { return _player.Value; }
        }

        public MusicPlayerViewModel(Lazy<IPlayer> player)
        {
            _player = player;
        }
    }
}
