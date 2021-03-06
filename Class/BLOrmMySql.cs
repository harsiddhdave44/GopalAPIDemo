using GopalAPIDemo.Model;
using NLog;
//using Microsoft.Extensions.Logging;
using ServiceStack.OrmLite;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GopalAPIDemo.Class
{
    public class BLOrmMySql
    {
        NLog.Logger Logger;
        public OrmLiteConnectionFactory connectionFactory = null;
        public IDbConnection connection;
        public BLOrmMySql(string connectionString)
        {
            Logger = LogManager.GetCurrentClassLogger();
            connectionFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
            Logger.Info("Opening the database connection");
            connection = connectionFactory.Open();
        }

        public List<Contacts> GetContacts()
        {
            List<Contacts> contacts = null;
            try
            {
                // using (RedisClient redisClient = new RedisClient(new RedisEndpoint(host: "rkitdemoredis.redis.cache.windows.net", port: 6379, password: "YYSuzltQFpwWqOJht0ivqCgaRAuVEcW0lcKTT9S9ar4=")))
                using (RedisClient redisClient = new RedisClient("localhost", 6379))
                {
                   if (redisClient.Get<List<Contacts>>("contacts") == null)
                   {
                        contacts = connection.Select<Contacts>();
                       redisClient.Set<List<Contacts>>("contacts", contacts);
                   }
                   else
                   {
                       contacts = redisClient.Get<List<Contacts>>("contacts");
                   }
                }
                return contacts;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, "Error while fetching data (in Logic)");
                return null;
            }
        }
        public bool AddContact(Contacts contact)
        {
            long isInserted=0;
            try
            {
                isInserted = connection.Insert<Contacts>(contact);

                using (RedisClient redisClient = new RedisClient("localhost", 6379))
                // using (RedisClient redisClient = new RedisClient(new RedisEndpoint(host: "rkitdemoredis.redis.cache.windows.net", port: 6379, password: "YYSuzltQFpwWqOJht0ivqCgaRAuVEcW0lcKTT9S9ar4=")))
                   redisClient.FlushDb();
                //    redisClient.Append("contacts",Encoding.UTF8.GetBytes())
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, "Error while inserting contact");
            }
            return isInserted > 0 ? true : false;
        }
    }
}
