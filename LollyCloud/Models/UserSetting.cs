using System;
using System.Collections.Generic;

namespace LollyCloud
{
    public class UserSettings
    {
        public List<UserSetting> USERSETTINGS { get; set; }
    }
    public class UserSetting
    {
        public int ID { get; set; }
        public int USERID { get; set; }
        public int KIND { get; set; }
        public int ENTITYID { get; set; }
        public String VALUE1 { get; set; }
        public String VALUE2 { get; set; }
        public String VALUE3 { get; set; }
        public String VALUE4 { get; set; }
    }
}
