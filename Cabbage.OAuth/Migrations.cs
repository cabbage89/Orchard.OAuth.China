using System.Data;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;
using Orchard.ContentManagement.MetaData;

namespace Cabbage.OAuth
{
    [OrchardFeature("Cabbage.OAuth")]
    public class QQMigrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(
                "QQSettingsPartRecord",
                table => table.ContentPartRecord()
                              .Column("ClientId", DbType.String, command => command.WithLength(512))
                              .Column("EncryptedClientSecret", DbType.String, command => command.WithLength(512))
                              .Column("Additional", DbType.String, command => command.WithLength(2000)));

            SchemaBuilder.CreateTable(
                "QQUserInfoPartRecord",
                table => table.Column<int>("Id", column => column.PrimaryKey().Identity())
                              .Column<int>("UserId")
                              .Column<string>("openid", c => c.WithLength(2048))
                              .Column<string>("nickname", c => c.WithLength(2048))
                              .Column<string>("figureurl", c => c.WithLength(2048))
                              .Column<string>("figureurl_1", c => c.WithLength(2048))
                              .Column<string>("figureurl_2", c => c.WithLength(2048))
                              .Column<string>("figureurl_qq_1", c => c.WithLength(2048))
                              .Column<string>("figureurl_qq_2", c => c.WithLength(2048))
                              .Column<string>("gender", c => c.WithLength(2048))
                              .Column<string>("is_yellow_vip", c => c.WithLength(2048))
                              .Column<string>("vip", c => c.WithLength(2048))
                              .Column<string>("yellow_vip_level", c => c.WithLength(2048))
                              .Column<string>("level", c => c.WithLength(2048))
                              .Column<string>("is_yellow_year_vip", c => c.WithLength(2048))
                              );

            return 1;
        }
    }

    [OrchardFeature("Cabbage.OAuth.WeiXin")]
    public class WeiXinMigrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(
                "WeiXinSettingsPartRecord",
                table => table.ContentPartRecord()
                              .Column("ClientId", DbType.String, command => command.WithLength(512))
                              .Column("EncryptedClientSecret", DbType.String, command => command.WithLength(512))
                              .Column("Additional", DbType.String, command => command.WithLength(2000)));
            SchemaBuilder.CreateTable(
                "WinXinUserInfoPartRecord",
                table => table.Column<int>("Id", column => column.PrimaryKey().Identity())
                              .Column<int>("UserId")
                              .Column<string>("openid", c => c.WithLength(2048))
                              .Column<string>("nickname", c => c.WithLength(2048))
                              .Column<string>("sex", c => c.WithLength(2048))
                              .Column<string>("province", c => c.WithLength(2048))
                              .Column<string>("city", c => c.WithLength(2048))
                              .Column<string>("country", c => c.WithLength(2048))
                              .Column<string>("headimgurl", c => c.WithLength(2048))
                              .Column<string>("privilege", c => c.WithLength(2048))
                              );
            return 1;
        }
        //public int UpdateFrom1()
        //{
        //    return 2;
        //}
    }

    [OrchardFeature("Cabbage.OAuth.Sina")]
    public class SinaMigrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(
                "SinaSettingsPartRecord",
                table => table.ContentPartRecord()
                              .Column("ClientId", DbType.String, command => command.WithLength(512))
                              .Column("EncryptedClientSecret", DbType.String, command => command.WithLength(512))
                              .Column("Additional", DbType.String, command => command.WithLength(2000)));
            return 1;
        }
    }

    [OrchardFeature("Cabbage.OAuth.Renren")]
    public class RenrenMigrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(
                "RenrenSettingsPartRecord",
                table => table.ContentPartRecord()
                              .Column("ClientId", DbType.String, command => command.WithLength(512))
                              .Column("EncryptedClientSecret", DbType.String, command => command.WithLength(512))
                              .Column("Additional", DbType.String, command => command.WithLength(2000)));
            return 1;
        }
    }

    [OrchardFeature("Cabbage.OAuth.Kaixin")]
    public class KaixinMigrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(
                "KaixinSettingsPartRecord",
                table => table.ContentPartRecord()
                              .Column("ClientId", DbType.String, command => command.WithLength(512))
                              .Column("EncryptedClientSecret", DbType.String, command => command.WithLength(512))
                              .Column("Additional", DbType.String, command => command.WithLength(2000)));
            return 1;
        }
    }

    [OrchardFeature("Cabbage.OAuth.Douban")]
    public class DoubanMigrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(
                "DoubanSettingsPartRecord",
                table => table.ContentPartRecord()
                              .Column("ClientId", DbType.String, command => command.WithLength(512))
                              .Column("EncryptedClientSecret", DbType.String, command => command.WithLength(512))
                              .Column("Additional", DbType.String, command => command.WithLength(2000)));
            return 1;
        }
    }

    [OrchardFeature("Cabbage.OAuth.Baidu")]
    public class BaiduMigrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(
                "BaiduSettingsPartRecord",
                table => table.ContentPartRecord()
                              .Column("ClientId", DbType.String, command => command.WithLength(512))
                              .Column("EncryptedClientSecret", DbType.String, command => command.WithLength(512))
                              .Column("Additional", DbType.String, command => command.WithLength(2000)));
            return 1;
        }
    }

    [OrchardFeature("Cabbage.OAuth.Taobao")]
    public class TaobaoMigrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(
                "TaobaoSettingsPartRecord",
                table => table.ContentPartRecord()
                              .Column("ClientId", DbType.String, command => command.WithLength(512))
                              .Column("EncryptedClientSecret", DbType.String, command => command.WithLength(512))
                              .Column("Additional", DbType.String, command => command.WithLength(2000)));
            return 1;
        }
    }
}
