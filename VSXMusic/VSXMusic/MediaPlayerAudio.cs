using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace VSXMusic
{
    [Export(typeof(IAudio))]
    public class MediaPlayerAudio : IAudio
    {
        MediaPlayer _player;

        public MediaPlayerAudio()
        {
            _player = new MediaPlayer();
            _player.MediaEnded += OnMediaEnded;
        }

        private void OnMediaEnded(object sender, EventArgs e)
        {
            if (this.MediaEnded != null)
            {
                this.MediaEnded(sender, e);
            }
        }

        #region IAudio 成员
        public TimeSpan TotalTime 
        {
            get
            {
                return _player.NaturalDuration.TimeSpan;
            }
        }

        public Uri Url
        {
            get { return _player.Source; }
            set { _player.Open(value); }
        }

        public double Volume
        {
            get { return _player.Volume; }
            set { _player.Volume = value; }
        }

        public void Play()
        {
            _player.Play();
        }

        public void Pause()
        {
            _player.Pause();
        }

        public event EventHandler MediaEnded;

        #endregion
    }
}
