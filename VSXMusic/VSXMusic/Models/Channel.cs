using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VSXMusic.Models
{
    /// <summary>
    /// 频道信息实体
    /// </summary>
    [DataContract]
    public class Channel
    {
        /// <summary>
        /// 频道编号
        /// </summary>
        [DataMember(Name = "channel_id")]
        public int ID { get; set; }
        /// <summary>
        /// 频道名称
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
        /// <summary>
        /// 数据上下文
        /// </summary>
        public string Context { get; set; }
    }
}
