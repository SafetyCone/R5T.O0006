using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.L0032.T000;
using R5T.T0131;
using R5T.T0172;
using R5T.T0172.Extensions;
using R5T.T0179.Extensions;
using R5T.T0181;
using R5T.T0208;


namespace R5T.O0006
{
    public partial interface IProjectFileOperations
    {
        /// <summary>
        /// Determines if the project file is in a local Git repository clone of a remote GitHub repository,
        /// and if that remote GitHub repository is private, adds the <see cref="L0032.Z000.ICustomProjectElementNames.PrivateGitHubRepository"/> property to the project.
        /// </summary>
        /// <returns></returns>
        public async Task Set_PrivateGitHubRepositoryPropertyValue(IProjectFilePath projectFilePath)
        {
            var isPrivate = await this.Is_InPrivateGitHubRepository(projectFilePath);
            if (isPrivate)
            {
                await this.Ensure_HasPrivateGitHubRepositoryProperty(projectFilePath);
            }
        }
    }
}
