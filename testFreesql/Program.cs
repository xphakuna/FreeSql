// See https://aka.ms/new-console-template for more information
using FreeSql;
using FreeSql.Extensions;

public class Program
{
    public static void Main()
    {
        var fsql = new FreeSql.FreeSqlBuilder()
        //.UseConnectionString(FreeSql.DataType.MySql, "Data Source=127.0.0.1;Port=3306;User ID=root;Password=laugh1234my;Initial Catalog=cccddd;Charset=utf8;SslMode=none;Max pool size=5;Allow User Variables=True")
        .UseConnectionString(FreeSql.DataType.MySql, "Data Source=127.0.0.1;Port=3306;User ID=root;Password=laugh1234my;database=test;Charset=utf8;SslMode=none;Max pool size=5;Allow User Variables=True")
        .UseAutoSyncStructure(true) //自动同步实体结构到数据库
        .Build(); //请务必定义成 Singleton 单例模式

        fsql.UseJsonMap();

        fsql.CodeFirst.SyncStructure<Blog>();
        fsql.CodeFirst.SyncStructure<Pid>();
        fsql.CodeFirst.SyncStructure<Touhou>();

        fsql.Aop.AuditValue += (s, e) =>
        {
            if (e.Property.Name.ToLower() == "createdat")
                e.Value = DateTime.Now;
            if (e.Property.Name.ToLower() == "updatedat") e.Value = DateTime.Now;
        };

        Touhou t = new Touhou();
        t.Pid = "pid0";
        t.AllMaxScore = new Map_n_n();

        t.AllMaxScore.Dict[1] = 10;
        t.AllMaxScore.Dict[2] = 20;

        int nrow = fsql.InsertOrUpdate<Touhou>().SetSource(t)
            .ExecuteAffrows();



        Console.WriteLine($"InsertOrUpdate affected {nrow}");

        var selected = fsql.Select<Touhou>().Where(t => t.Pid == "pid0").ToList();
        Console.WriteLine($"selected {selected.Count}");
        if (selected.Count > 0)
        {
            Console.WriteLine($"selected0 {selected[0].AllMaxScore}");
        }

        //var blog = new Blog { Url = "http://sample.com" };
        //blog.BlogId = (int)fsql.Insert<Blog>()
        //    .AppendData(blog)
        //    .ExecuteIdentity();

        Console.WriteLine("Hello, World!");
    }
}

