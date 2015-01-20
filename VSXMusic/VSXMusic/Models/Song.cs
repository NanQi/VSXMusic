using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VSXMusic.Models
{
    [DataContract]
    public class Song : ViewModelBase
    {
        Uri _prcture;
        /// <summary>
        /// 封面图片
        /// </summary>
        [DataMember(Name = "picture")]
        public Uri Picture
        {
            get
            {
                return _prcture;
            }
            set
            {
                if (_prcture != value)
                {
                    _prcture = value;
                    OnPropertyChanged(() => Picture);
                }
            }
        }
        string _albumTitle;
        /// <summary>
        /// 唱片标题
        /// </summary>
        [DataMember(Name = "albumtitle")]
        public string AlbumTitle
        {
            get
            {
                return _albumTitle;
            }
            set
            {
                if (_albumTitle != value)
                {
                    _albumTitle = value;
                    OnPropertyChanged(() => AlbumTitle);
                }
            }
        }
        string _album;
        /// <summary>
        /// 唱片路径
        /// </summary>
        [DataMember(Name = "album")]
        public string Album
        {
            get
            {
                return _album;
            }
            set
            {
                if (_album != value)
                {
                    _album = value;
                    OnPropertyChanged(() => Album);
                }
            }
        }
        string _company;
        /// <summary>
        /// 发行公司
        /// </summary>
        [DataMember(Name = "company")]
        public string Company
        {
            get
            {
                return _company;
            }
            set
            {
                if (_company != value)
                {
                    _company = value;
                    OnPropertyChanged(() => Company);
                }
            }
        }
        double _rating;
        /// <summary>
        /// 平均评分
        /// </summary>
        [DataMember(Name = "rating_avg")]
        public double Rating
        {
            get
            {
                return _rating;
            }
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    OnPropertyChanged(() => Rating);
                }
            }
        }
        string _publicTime;
        /// <summary>
        /// 发行时间
        /// </summary>
        [DataMember(Name = "public_time")]
        public string PublicTime
        {
            get
            {
                return _publicTime;
            }
            set
            {
                if (_publicTime != value)
                {
                    _publicTime = value;
                    OnPropertyChanged(() => PublicTime);
                }
            }
        }
        string _ssid;
        /// <summary>
        /// ssid
        /// </summary>
        [DataMember(Name = "ssid")]
        public string Ssid
        {
            get
            {
                return _ssid;
            }
            set
            {
                if (_ssid != value)
                {
                    _ssid = value;
                    OnPropertyChanged(() => Ssid);
                }
            }
        }
        bool _like;
        /// <summary>
        /// 当前用户是否喜欢
        /// </summary>
        [DataMember(Name = "like")]
        public bool Like
        {
            get
            {
                return _like;
            }
            set
            {
                if (_like != value)
                {
                    _like = value;
                    OnPropertyChanged(() => Like);
                }
            }
        }
        string _artist;
        /// <summary>
        /// 歌手
        /// </summary>
        [DataMember(Name = "artist")]
        public string Artist
        {
            get
            {
                return _artist;
            }
            set
            {
                if (_artist != value)
                {
                    _artist = value;
                    OnPropertyChanged(() => Artist);
                }
            }
        }
        Uri _url;
        /// <summary>
        /// 歌曲路径
        /// </summary>
        [DataMember(Name = "url")]
        public Uri Url
        {
            get
            {
                return _url;
            }
            set
            {
                if (_url != value)
                {
                    _url = value;
                    OnPropertyChanged(() => Url);
                }
            }
        }
        string _title;
        /// <summary>
        /// 歌曲名称
        /// </summary>
        [DataMember(Name = "title")]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(() => Title);
                }
            }
        }
        string _subType;
        /// <summary>
        /// 普通音乐应该是""，广告应该是"T"
        /// </summary>
        [DataMember(Name = "subtype")]
        public string SubType
        {
            get
            {
                return _subType;
            }
            set
            {
                if (_subType != value)
                {
                    _subType = value;
                    OnPropertyChanged(() => SubType);
                }
            }
        }
        string _songID;
        /// <summary>
        /// 歌曲ID
        /// </summary>
        [DataMember(Name = "sid")]
        public string SongID
        {
            get
            {
                return _songID;
            }
            set
            {
                if (_songID != value)
                {
                    _songID = value;
                    OnPropertyChanged(() => SongID);
                }
            }
        }
        double _length;
        /// <summary>
        /// 不知道是什么东西的长度
        /// </summary>
        [DataMember(Name = "length")]
        public double Length
        {
            get
            {
                return _length;
            }
            set
            {
                if (_length != value)
                {
                    _length = value;
                    OnPropertyChanged(() => Length);
                }
            }
        }
        string _albumID;
        /// <summary>
        /// 专辑ID
        /// </summary>
        [DataMember(Name="aid")]
        public string AlbumID
        {
            get
            {
                return _albumID;
            }
            set
            {
                if (_albumID != value)
                {
                    _albumID = value;
                    OnPropertyChanged(() => AlbumID);
                }
            }
        }
        /// <summary>
        /// 是否是广告
        /// </summary>
        public bool IsAd
        {
            get
            {
                return this.SubType == "T";
            }
        }
    }
}
