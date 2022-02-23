using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql.Extensions;

using FreeSql.DataAnnotations;



[Table(Name = "tabless")]
public class Blog
{
    [Column(IsIdentity = true, IsPrimary = true)]
    public int BlogId { get; set; }
    public string Url { get; set; }
    public int Rating { get; set; }
}



[Table(Name = "pids")]
public class Pid
{
    [Column(IsIdentity = true, IsPrimary = true)]
    public string BindId { get; set; }
    public string Country { get; set; }
    public string DeviceInfo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}


[Table(Name = "touhous")]
public class Touhou
{
    [Column(IsIdentity = true, IsPrimary = true)]
    public string Pid { get; set; }

    [JsonMap]
    public Map_n_n? AllMaxScore { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class Map_n_n
{
    public Dictionary<int, int> Dict { get; set; } = new Dictionary<int, int>();
}
