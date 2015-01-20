using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSXMusic
{
    public interface IAudio
    {
        TimeSpan TotalTime { get; }

        Uri Url { get; set; }

        double Volume { get; set; }

        void Play();

        void Pause();

        event EventHandler MediaEnded;
    }
}
