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
    [ValuesMarker]
    public partial interface IProjectFileOperations : IValuesMarker
    {
        /// <summary>
        /// Allows writing out the modified project file to a different path (useful during construction and testing).
        /// </summary>
        public async Task IdentifyAndRemedyVariances(
            IProjectFilePath inputProjectFilePath,
            IProjectFilePath outputProjectFilePath)
        {
            var projectIsInPrivateGitHubRepository = await this.Is_InPrivateGitHubRepository(inputProjectFilePath);

            await this.In_Modify_ProjectElementContext(
                inputProjectFilePath,
                outputProjectFilePath,
                projectElement =>
                {
                    Instances.ProjectElementOperations.IdentifyAndRemedyVariances(
                        projectElement,
                        projectIsInPrivateGitHubRepository);
                });
        }

        public Task IdentifyAndRemedyVariances(IProjectFilePath projectFilePath)
        {
            return this.IdentifyAndRemedyVariances(
                projectFilePath,
                projectFilePath);
        }

        public async Task In_Modify_ProjectElementContext(
            IProjectFilePath inputProjectFilePath,
            IProjectFilePath outputProjectFilePath,
            Action<IProjectElement> projectElementAction = default)
        {
            var projectElement = await Instances.ProjectElementOperations.From(inputProjectFilePath);

            Instances.ActionOperator.Run(
                projectElement,
                projectElementAction);

            await Instances.ProjectElementOperations.To_File(
                outputProjectFilePath,
                projectElement);
        }

        public async Task In_Modify_ProjectElementContext(
            IProjectFilePath inputProjectFilePath,
            IProjectFilePath outputProjectFilePath,
            Func<IProjectElement, Task> projectElementAction = default)
        {
            var projectElement = await Instances.ProjectElementOperations.From(inputProjectFilePath);

            await Instances.ActionOperator.Run(
                projectElementAction,
                projectElement);

            await Instances.ProjectElementOperations.To_File(
                outputProjectFilePath,
                projectElement);
        }

        public Task In_Modify_ProjectElementContext(
            IProjectFilePath projectFilePath,
            Action<IProjectElement> projectElementAction = default)
        {
            return this.In_Modify_ProjectElementContext(
                projectFilePath,
                projectFilePath,
                projectElementAction);
        }

        public Task In_Modify_ProjectElementContext(
            IProjectFilePath projectFilePath,
            Func<IProjectElement, Task> projectElementAction = default)
        {
            return this.In_Modify_ProjectElementContext(
                projectFilePath,
                projectFilePath,
                projectElementAction);
        }

        /// <summary>
        /// Adds the <see cref="L0032.Z000.ICustomProjectElementNames.PrivateGitHubRepository"/>.
        /// </summary>
        public Task Add_PrivateGitHubRepositoryProperty(IProjectFilePath projectFilePath)
        {
            return this.Ensure_HasPrivateGitHubRepositoryProperty(projectFilePath);
        }

        /// <summary>
        /// Sets the <see cref="L0032.Z000.ICustomProjectElementNames.PrivateGitHubRepository"/> based on whether the project file is in a private GitHub repository.
        /// </summary>
        public Task Set_PrivateGitHubRepositoryProperty(IProjectFilePath projectFilePath)
        {
            return this.In_Modify_ProjectElementContext(
                projectFilePath,
                projectElement =>
                {
                    return Instances.ProjectElementOperations.Set_PrivateGitHubRepositoryProperty(
                        projectElement,
                        projectFilePath);
                });
        }

        public Task Ensure_HasPrivateGitHubRepositoryProperty(IProjectFilePath projectFilePath)
        {
            return Instances.ProjectFileXmlOperator.In_ModifyProjectElementContext(
                projectFilePath,
                projectElement =>
                {
                    Instances.ProjectXmlOperator.In_CustomPropertyGroupElementContext(
                        projectElement,
                        customPropertyGroupElement =>
                        {
                            Instances.CustomPropertyGroupElementOperator.Ensure_HasPrivateGitHubRepositoryProperty(customPropertyGroupElement);
                        });
                });
        }

        public Task<bool> Is_InPrivateGitHubRepository(IProjectFilePath projectFilePath)
        {
            var output = Instances.ProjectFilePathOperations.Is_InPrivateGitHubRepository(projectFilePath);
            return output;
        }

        public async Task<PackageReference[]> Get_PackageReferences(IProjectFilePath projectFilePath)
        {
            var packageReferences = await Instances.ProjectFileXmlOperator.Query_ProjectElementContext(
                projectFilePath,
                Instances.ProjectXmlOperator.Get_PackageReferences);

            return packageReferences;
        }

        public async Task<IProjectFilePath[]> Get_TopLevelProjectReferences(
            IEnumerable<IProjectFilePath> projectFilePaths)
        {
            var values = await Instances.ProjectReferencesOperator.Get_TopLevelProjectReferences(
                projectFilePaths.Get_Values())
                ;

            var output = values.To_Typeds(x => x.ToProjectFilePath()).Now();
            return output;
        }

        /// <inheritdoc cref="F0016.F001.IProjectReferencesOperator.GetAllRecursiveProjectReferences(IEnumerable{string})"/>
        public async Task<IProjectFilePath[]> Get_RecursiveProjectReferences(
            IProjectFilePath projectFilePath)
        {
            var recursiveProjectReferences = await Instances.ProjectReferencesOperator.GetAllRecursiveProjectReferences(
                projectFilePath.Value);

            var output = recursiveProjectReferences.To_Typeds(x => x.ToProjectFilePath()).Now();
            return output;
        }

        /// <inheritdoc cref="F0016.F001.IProjectReferencesOperator.GetAllRecursiveProjectReferences(IEnumerable{string})"/>
        public async Task<IProjectFilePath[]> Get_RecursiveProjectReferences(
            T0159.ITextOutput textOutput,
            IEnumerable<IProjectFilePath> projectFilePaths)
        {
            var recursiveProjectReferences = await Instances.ProjectReferencesOperator.GetAllRecursiveProjectReferences(
                projectFilePaths.Get_Values());

            var output = recursiveProjectReferences.To_Typeds(x => x.ToProjectFilePath()).Now();
            return output;
        }

        /// <inheritdoc cref="F0016.F001.IProjectReferencesOperator.GetAllRecursiveProjectReferences(IEnumerable{string})"/>
        public Task<IProjectFilePath[]> Get_RecursiveProjectReferences(
            T0159.ITextOutput textOutput,
            params IProjectFilePath[] projectFilePaths)
        {
            return this.Get_RecursiveProjectReferences(
                textOutput,
                projectFilePaths.AsEnumerable());
        }

        /// <summary>
        /// Inclusive, in that the input project files are included in the output.
        /// <inheritdoc cref="F0016.F001.IProjectReferencesOperator.GetAllRecursiveProjectReferences(IEnumerable{string})"/>
        /// </summary>
        public async Task<IProjectFilePath[]> Get_RecursiveProjectReferences_Inclusive(
            IProjectFilePath projectFilePath)
        {
            var recursiveProjectReferences = await this.Get_RecursiveProjectReferences(projectFilePath);

            var output = recursiveProjectReferences.Append(projectFilePath).Now();
            return output;
        }

        /// <inheritdoc cref="Get_RecursiveProjectReferences_Inclusive(IProjectFilePath)"/>
        public async Task<IProjectFilePath[]> Get_RecursiveProjectReferences_Inclusive(
            T0159.ITextOutput textOutput,
            IEnumerable<IProjectFilePath> projectFilePaths)
        {
            var recursiveProjectReferences = await this.Get_RecursiveProjectReferences(
                textOutput,
                projectFilePaths);

            var output = recursiveProjectReferences.Union(projectFilePaths).Now();
            return output;
        }

        /// <inheritdoc cref="Get_RecursiveProjectReferences_Inclusive(IProjectFilePath)"/>
        public Task<IProjectFilePath[]> Get_RecursiveProjectReferences_Inclusive(
            T0159.ITextOutput textOutput,
            params IProjectFilePath[] projectFilePaths)
        {
            return this.Get_RecursiveProjectReferences_Inclusive(
                textOutput,
                projectFilePaths.AsEnumerable());
        }

        /// <inheritdoc cref="F0016.F001.IProjectReferencesOperator.Get_RecursiveProjectReferences_InDependencyOrder(IEnumerable{string})"/>
        public async Task<IProjectFilePath[]> Get_RecursiveProjectReferences_InDependencyOrder(
            IProjectFilePath projectFilePath)
        {
            var recursiveProjectReferences = await Instances.ProjectReferencesOperator.Get_RecursiveProjectReferences_InDependencyOrder(
                projectFilePath.Value);

            var output = recursiveProjectReferences.To_Typeds(x => x.ToProjectFilePath()).Now();
            return output;
        }

        public async Task Write_RecursiveProjectReferencesTo(
            IProjectFilePath projectFilePath,
            ITextFilePath textFilePath)
        {
            var recursiveProjectReferences = await this.Get_RecursiveProjectReferences(projectFilePath);

            await Instances.FileOperator.Write_Lines(
                textFilePath.Value,
                recursiveProjectReferences.Get_Values().OrderAlphabetically());
        }

        /// <inheritdoc cref="F0020.IProjectFileOperator.IsProjectFile(string)"/>
        public bool Is_ProjectFile(
            IProjectFilePath projectFilePath)
        {
            var output = Instances.ProjectFileOperator.IsProjectFile(
                projectFilePath.Value);

            return output;
        }
    }
}
