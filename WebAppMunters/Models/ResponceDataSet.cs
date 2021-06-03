using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMunters.Models
{
    public class ResponceDataSet
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public ResponceDataSet(string id, string title, string url)
        {
            this.Id = id;
            this.Title = title;
            this.Url = url;
        }
    }
}
