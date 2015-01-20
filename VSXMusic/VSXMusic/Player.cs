using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace VSXMusic
{
    public abstract class Player : Models.ViewModelBase, IPlayer
    {
        public IAudio Audio { get; set; }

        Models.SongList _songList;

        public Player(IAudio audio)
        {
            Audio = audio;
            Audio.MediaEnded += (s, e) => Next();
        }

        protected void Initialization()
        {
            if (this.CurrentChannel == null)
            {
                _currentChannel = this.GetChannelList().PublicChannelList.OrderBy(_ => new Guid()).First();
                _songList = this.GetSongList(_currentChannel, _currentSong);
                _currentSong = _songList.Songs.FirstOrDefault();
                Audio.Url = _currentSong.Url;
            }
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
                    OnPropertyChanged(() => CurrentChannel);
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
                    OnPropertyChanged(() => CurrentSong);
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
        bool _isPlaying;

        public bool IsPlaying
        {
            get
            {
                return _isPlaying;
            }
            set
            {
                if (_isPlaying != value)
                {
                    _isPlaying = value;
                    OnPropertyChanged(() => IsPlaying);
                }
            }
        }

        public bool CanPlay
        {
            get
            {
                return this.CurrentSong != null && !this.IsPlaying;
            }
        }

        #endregion
    }
}
