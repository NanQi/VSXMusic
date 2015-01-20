using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VSXMusic.Models
{
    /// <summary>
    /// 频道列表实体
    /// </summary>
    [DataContract]
    public class ChannelList
    {
        /// <summary>
        /// 私人频道列表
        /// </summary>
        [DataMember(Name="personal")]
        public IList<Channel> PersonalChannelList { get; set; }
        /// <summary>
        /// 公共频道列表
        /// </summary>
        [DataMember(Name = "public")]
        public IList<Channel> PublicChannelList { get; set; }
        /// <summary>
        /// DJ频道列表
        /// </summary>
        [DataMember(Name = "dj")]
        public IList<Channel> DJChannelList { get; set; }
    }
}
