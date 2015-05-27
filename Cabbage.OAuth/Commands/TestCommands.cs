using Cabbage.OAuth.Models;
using Orchard.Commands;
using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabbage.OAuth.Commands
{
    public class TestCommands : DefaultOrchardCommandHandler
    {
        private readonly IRepository<WinXinUserInfoPartRecord> _winXinUserInfoPartRecordRepository;
        public TestCommands(IRepository<WinXinUserInfoPartRecord> winXinUserInfoPartRecordRepository)
        {
            _winXinUserInfoPartRecordRepository = winXinUserInfoPartRecordRepository;
        }

        [CommandName("test1")]
        public void T1()
        {
            var record = new WinXinUserInfoPartRecord
            {
                UserId = 5,
                openid = "openid",
                nickname = "nickname",
                sex = "sex",
                province = "province",
                city = "city",
                country = "country",
                headimgurl = "headimgurl",
                privilege = "privilege",

            };
            _winXinUserInfoPartRecordRepository.Create(record);
            Console.ForegroundColor = ConsoleColor.Green;
            Context.Output.WriteLine("插入成功.");
        }
    }
}
