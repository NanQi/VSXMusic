using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;

namespace VSXMusic.Commands
{
    [CLSCompliant(false)]
    public abstract class DynamicCommand : OleMenuCommand
    {
        private static DTE _dte;
        private static VSXMusicPackage _package;
        private static IServiceProvider _serviceProvider;

        /// <summary>
        ///  建立命令,注册OnBeforeQueryStatus事件。
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="onExecute"></param>
        /// <param name="id"></param>
        protected DynamicCommand(IServiceProvider provider, EventHandler onExecute, CommandID id)
            : base(onExecute, id)
        {
            BeforeQueryStatus += OnBeforeQueryStatus;
            _serviceProvider = provider;
        }

        /// <summary>
        /// 可执行判断
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected virtual bool CanExecute(OleMenuCommand command)
        {
            return false;
        }

        /// <summary>
        /// 控制命令按钮显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnBeforeQueryStatus(object sender, EventArgs e)
        {
            var command = sender as OleMenuCommand;
            if (command == null) return;
            var canshow = CanExecute(command);
            //command.Enabled = false;
            command.Enabled = command.Supported = command.Visible = canshow;
           // command.Supported = false;
        }

        /// <summary>
        /// DTE对象
        /// </summary>
        protected static DTE Dte
        {
            get { return _dte ?? (_dte = (DTE) ServiceProvider.GetService(typeof(DTE))); }
        }

        /// <summary>
        /// Package对象
        /// </summary>
        public static VSXMusicPackage Package
        {
            get {
                return _package ??
                       (_package =
                        (VSXMusicPackage)ServiceProvider.GetService(typeof(VSXMusicPackage)));
            }
        }

        /// <summary>
        /// ServiceProvider对象
        /// </summary>
        protected static IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }
        }
    }
}
