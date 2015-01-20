using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace VSXMusic
{
    public abstract class PlayerBase : IPlayer
    {
        public abstract IAudio Audio { get; set; }

        Models.SongList _songList;

        public PlayerBase()
        {
            //Audio.MediaEnded += OnMediaEnded;
        }

        private void OnMediaEnded(object sender, EventArgs e)
        {
            Next();
        }

        #region IPlayer 成员

        public abstract Models.ChannelList GetChannelList();
        public abstract Models.SongList GetSongList(Models.Channel channel, Models.Song song, string type = "n");

        public void Play()
        {
            Audio.Play();
            IsPlaying = true;
        }

        public void Pause()
        {
            Audio.Pause();
            IsPlaying = false;
        }

        public void Next()
        {
            if (_songList == null)
            {
                //当还没有加载歌曲列表时,加载歌曲列表
                _songList = this.GetSongList(this.CurrentChannel, this.CurrentSong);
            }
            else
            {
                if (this.CurrentSong == null)
                {
                    //当加载了歌曲列表,当前歌曲为空时,播放歌曲列表的第一首
                    this.CurrentSong = _songList.Songs.FirstOrDefault();
                }
                else
                {
                    var pos = _songList.Songs.IndexOf(this.CurrentSong);
                    if (pos == _songList.Songs.Count - 1)
                    {
                        //当前歌曲已经是播放列表的最后一首歌,重新加载列表
                        _songList = this.GetSongList(this.CurrentChannel, this.CurrentSong);
                    }
                    else
                    {
                        //当前歌曲不是播放列表的最后一首歌,播放下一首
                        this.CurrentSong = _songList.Songs[pos + 1];
                    }
                }
            }
        }

        public void Never()
        {
            throw new NotImplementedException();
        }

        public void Unlike()
        {
            throw new NotImplementedException();
        }

        public void Like()
        {
            throw new NotImplementedException();
        }

        Models.Channel _currentChannel;

        public Models.Channel CurrentChannel
        {
            get
            {
                return _currentChannel;
            }
            set
            {
                if (_currentChannel != value)
                {
                    _currentChannel = value;
                    _songList = this.GetSongList(_currentChannel, _currentSong);
                    CurrentSong = _songList.Songs.FirstOrDefault();
                }
            }
        }

        Models.Song _currentSong;

        public Models.Song CurrentSong
        {
            get
            {
                return _currentSong;
            }
            set
            {
                if (_currentSong != value)
                {
                    _currentSong = value;
                    Audio.Url = _currentSong.Url;
                    Play();
                }
            }
        }

        public TimeSpan CurrentTime
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public TimeSpan TotalTime
        {
            get
            {
                return Audio.TotalTime;
            }
        }

        public double Volumn
        {
            get
            {
                return Audio.Volume;
            }
            set
            {
                Audio.Volume = value;
            }
        }

        public bool IsPlaying
        {
            get;
            set;
        }

        public bool CanPlay
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
