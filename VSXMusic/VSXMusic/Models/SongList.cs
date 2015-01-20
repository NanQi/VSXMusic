using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VSXMusic.Models
{
    [DataContract]
    public class SongList
    {
        [DataMember(Name = "song")]
        public List<Song> Songs { get; set; }
    }
}
