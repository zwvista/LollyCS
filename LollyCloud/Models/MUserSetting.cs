using System;
using System.Collections.Generic;

namespace LollyXamarinNative
{
    public class MUserSettings
    {
        public List<MUserSetting> USERSETTINGS { get; set; }
    }
    public class MUserSetting
    {
        public int ID { get; set; }
        public int USERID { get; set; }
        public int KIND { get; set; }
        public int ENTITYID { get; set; }
        public string VALUE1 { get; set; }
        public string VALUE2 { get; set; }
        public string VALUE3 { get; set; }
        public string VALUE4 { get; set; }
    }
}
