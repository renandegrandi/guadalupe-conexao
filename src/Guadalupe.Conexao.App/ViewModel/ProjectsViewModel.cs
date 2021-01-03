using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.Repository;
using Guadalupe.Conexao.App.View;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Guadalupe.Conexao.App.ViewModel
{
    sealed class ProjectsViewModel : ViewModel
    {
        #region Dependencies

        private readonly IProjectRepository _projectRepository;

        #endregion

        #region Properties

        public List<Project> Projects { get; set; }

        #endregion

        public ICommand ProjectTapCommandAsync => new Command<Guid>(async (Guid id) =>
        {
            try
            {
                await _navigation.PushModalAsync(new ProjectView(id));
            }
            catch (Exception ex)
            {
                Log.Warning(nameof(ProjectsViewModel), ex.Message);
            }
        });

        public ProjectsViewModel(INavigation navigation, IProjectRepository projectRepository) : base(navigation)
        {
            _projectRepository = projectRepository;

            Projects = projectRepository.Get();
        }
    }
}
