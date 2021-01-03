using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.Repository;
using System;
using Xamarin.Forms;

namespace Guadalupe.Conexao.App.ViewModel
{
    sealed class ProjectViewModel : ViewModel
    {
        #region Properties

        public Project Project { get; private set; }

        #endregion

        public ProjectViewModel(INavigation navigation, IProjectRepository projectRepository, Guid project) : base(navigation)
        {
            Project = projectRepository.Get(project);
        }
    }
}
