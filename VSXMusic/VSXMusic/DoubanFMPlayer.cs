using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;

namespace VSXMusic
{
    [Export(typeof(IPlayer))]
    [Export(typeof(DoubanFMPlayer))]
    public class DoubanFMPlayer : PlayerBase
    {
        [Import]
        public Common.HtmlHelper HtmlHelper { get; set; }

        [Import]
        public override IAudio Audio { get; set; }

        public override Models.ChannelList GetChannelList()
        {
            var json = HtmlHelper.Get("http://doubanfmcloud-channelinfo.stor.sinaapp.com/channelinfo");
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
            string json = HtmlHelper.Get(url, @"application/json, text/javascript, */*; q=0.01", @"http://douban.fm");
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
