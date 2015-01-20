using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace VSXMusic
{
    [Export(typeof(IPlayer))]
    [Export(typeof(DoubanFMPlayer))]
    public class DoubanFMPlayer : Player
    {
        Common.HtmlHelper _htmlHelper;

        [ImportingConstructor]
        public DoubanFMPlayer(IAudio audio, Common.HtmlHelper htmlHelper) : base(audio)
        {
            _htmlHelper = htmlHelper;
            Initialization();
        }

        public override Models.ChannelList GetChannelList()
        {
            //尝试获取本地频道列表
            try
            {
                if (File.Exists(Common.Paths.ChannelFile))
                {
                    var localChannelTime = File.GetLastWriteTime(Common.Paths.ChannelFile);

                    if (DateTime.Now - localChannelTime < TimeSpan.FromHours(6))
                    {
                        var content = File.ReadAllText(Common.Paths.ChannelFile);
                        return Common.JsonHelper.Deserialize<Models.ChannelList>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(DateTime.Now + " 获取本地频道列表失败：" + ex.Message);
            }

            var json = _htmlHelper.Get("http://doubanfmcloud-channelinfo.stor.sinaapp.com/channelinfo");

            try
            {
                File.WriteAllText(Common.Paths.ChannelFile, json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(DateTime.Now + " 写入本地频道列表失败：" + ex.Message);
            }

            return Common.JsonHelper.Deserialize<Models.ChannelList>(json);
        }

        /// <summary>
        /// 根据频道和歌曲,得到歌曲列表
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="song"></param>
        /// <param name="type">n-New</param>
        /// <returns></returns>
        public override Models.SongList GetSongList(Models.Channel channel, Models.Song song, string type = "n")
        {
            Common.Parameters parameters = new Common.Parameters();
            parameters["from"] = "mainsite";
            parameters["context"] = channel.Context;
            parameters["sid"] = song != null ? song.SongID : null;
            parameters["channel"] = channel.ID.ToString();
            parameters["type"] = type;
            Random rnd = new Random();
            var number = rnd.NextDouble();
            parameters["r"] = number.ToString();

            string url = Common.HtmlHelper.ConstructUrlWithParameters("http://douban.fm/j/mine/playlist", parameters);

            //获取列表
            string json = _htmlHelper.Get(url, @"application/json, text/javascript, */*; q=0.01", @"http://douban.fm");
            var songList = Common.JsonHelper.Deserialize<Models.SongList>(json);

            //将小图更换为大图
            foreach (var s in songList.Songs)
            {
                s.Picture = new Uri(s.Picture.ToString().Replace("/mpic/", "/lpic/").Replace("//otho.", "//img3."));
            }

            //去广告
            songList.Songs.RemoveAll(s => s.IsAd);

            return songList;
        }
    }
}
