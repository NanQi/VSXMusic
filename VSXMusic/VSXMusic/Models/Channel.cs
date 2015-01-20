using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VSXMusic.Models
{
    /// <summary>
    /// 频道信息实体
    /// </summary>
    [DataContract]
    public class Channel : ViewModelBase
    {
        int _id;

        /// <summary>
        /// 频道编号
        /// </summary>
        [DataMember(Name = "channel_id")]
        public int ID 
        { 
            get
            {
                return _id; 
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(() => ID);
                }
            }
        }
        string _name;

        /// <summary>
        /// 频道名称
        /// </summary>
        [DataMember(Name = "name")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }
        string _context;
        /// <summary>
        /// 数据上下文
        /// </summary>
        public string Context
        {
            get
            {
                return _context;
            }
            set
            {
                if (_context != value)
                {
                    _context = value;
                    OnPropertyChanged(() => Context);
                }
            }
        }
    }
}
