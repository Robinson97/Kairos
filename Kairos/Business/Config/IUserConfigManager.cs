using Kairos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kairos.Business.Config
{
    public interface IUserConfigManager
    {
        /// <summary>
        /// Pfad zur Benutzerdokumentation
        /// </summary>
        string PathToUserConfig { get; }

        /// <summary>
        /// Save the Userconfig
        /// </summary>
        void SaveUserConfig();

        /// <summary>
        /// Delete the current userconfig
        /// </summary>
        void DeleteUserConfig();

        /// <summary>
        /// Ovveride the current userconfig with default values
        /// </summary>
        void LoadDefaultConfig();

        /// <summary>
        /// Returns the current userConfig
        /// </summary>
        UserConfig GetUserConfig();
    }
}
