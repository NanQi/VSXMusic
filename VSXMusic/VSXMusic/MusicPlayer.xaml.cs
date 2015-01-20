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
        private ExportProvider _exportProvider;
        public MusicPlayer()
        {
            InitializeComponent();
            //_exportProvider = ((IComponentModel)Package.GetGlobalService(typeof(SComponentModel))).DefaultExportProvider;
            //var player = _exportProvider.GetExportedValueOrDefault<IPlayer>();

            //if (player.CurrentChannel == null)
            //{
            //    player.CurrentChannel = player.GetChannelList().PublicChannelList.FirstOrDefault();
            //}
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            //if (Player.IsPlaying)
            //{
            //    Player.Pause();
            //}
            //else
            //{
            //    Player.Play();
            //}
        }
    }

    public class MusicPlayerViewModel
    {
        
    }
}
