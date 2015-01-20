using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace VSXMusic.Commands
{
    public class PlayCommand : DynamicCommand
    {
        static IPlayer _player;
        public PlayCommand(IServiceProvider serviceProvider, IPlayer player)
            : base(serviceProvider, OnExecute,
            new CommandID(GuidList.guidVSXMusicCmdSet, (int)PkgCmdIDList.cmdidPlay))
        {
            _player = player;
        }

        protected override bool CanExecute(OleMenuCommand command)
        {
            return _player.CanPlay;
        }

        private static void OnExecute(object sender, EventArgs e)
        {
            _player.Play();
        }
    }
}
