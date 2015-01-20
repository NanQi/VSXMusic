using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSXMusic
{
    public interface IPlayer
    {
        Models.ChannelList GetChannelList();
        Models.SongList GetSongList(Models.Channel channel, Models.Song song, string type = "n");

        Models.Channel CurrentChannel { get; set; }
        Models.Song CurrentSong { get; set; }
        TimeSpan CurrentTime { get; set; }
        TimeSpan TotalTime { get; }
        double Volumn { get; set; }
        bool IsPlaying { get; set; }
        bool CanPlay { get; set; }

        void Play();

        void Pause();

        void Next();

        void Never();

        void Unlike();

        void Like();
    }
}
