using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace VSXMusic.Commands
{
    public class NeverCommand : DynamicCommand
    {
        IPlayer _player;
        public NeverCommand(IServiceProvider serviceProvider, IPlayer player)
            : base(serviceProvider, OnExecute,
            new CommandID(GuidList.guidVSXMusicCmdSet, (int)PkgCmdIDList.cmdidNever))
        {
            _player = player;
        }

        protected override bool CanExecute(OleMenuCommand command)
        {
            return base.CanExecute(command);
        }

        private static void OnExecute(object sender, EventArgs e)
        {

        }
    }
}
