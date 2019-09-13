using Kairos.Business.Config;
using Kairos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kairos.DataAccess.ConfigManager.UserConfig
{
    public class UserConfigManager : IUserConfigManager
    {
        public string PathToUserConfig { get; private set; }

        public UserConfigManager()
        {
            PathToUserConfig = "";
        }

        public void DeleteUserConfig()
        {
            throw new NotImplementedException();
        }

        public Data.UserConfig GetUserConfig()
        {
            throw new NotImplementedException();
        }

        public void LoadDefaultConfig()
        {
            throw new NotImplementedException();
        }

        public void SaveUserConfig()
        {
            throw new NotImplementedException();
        }
    }
}
