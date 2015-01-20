using System;
using Microsoft.VisualStudio.Shell;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.ComponentModelHost;

namespace VSXMusic.Commands
{
    internal class CommandSet
    {
        private readonly OleMenuCommandService _menuCommandService;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// OleMenuCommandService对象
        /// </summary>
        /// <param name="provider"></param>
        public CommandSet(IServiceProvider provider)
        {
            _serviceProvider = provider;
            _menuCommandService = (OleMenuCommandService)_serviceProvider.GetService(typeof(IMenuCommandService));
        }

        public void Initialize()
        {
            RegisterMenuCommands();
        }
        /// <summary>
        /// 注册命令
        /// </summary>
        private void RegisterMenuCommands()
        {
            var componentModel = (IComponentModel)_serviceProvider.GetService(typeof(SComponentModel));
            var player = componentModel.DefaultExportProvider.GetExportedValue<IPlayer>();

            var playCommand = new PlayCommand(_serviceProvider, player);
            _menuCommandService.AddCommand(playCommand);

            var pauseCommand = new PauseCommand(_serviceProvider, player);
            _menuCommandService.AddCommand(pauseCommand);

            var nextCommand = new NextCommand(_serviceProvider, player);
            _menuCommandService.AddCommand(nextCommand);

            var neverCommand = new NeverCommand(_serviceProvider, player);
            _menuCommandService.AddCommand(neverCommand);

            var likeCommand = new LikeCommand(_serviceProvider, player);
            _menuCommandService.AddCommand(likeCommand);

            var unlikeCommand = new UnlikeCommand(_serviceProvider, player);
            _menuCommandService.AddCommand(unlikeCommand);
        }

    }
}
