using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.Repository;
using Guadalupe.Conexao.App.Service;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Guadalupe.Conexao.App.ViewModel
{
    sealed class ProjectViewModel : ViewModel
    {
        #region Dependencies

        private readonly IWhatsappService _whatsappService;

        #endregion

        #region Properties

        public Project Project { get; private set; }

        public ICommand WantParticipateCommandAsync => new Command(async () =>
        {
            await _whatsappService.OpenAsync(Project.Contact, $"Oi, acabei de ler sobre o projeto: {Project.Name}, no APP da missão guadalupe e gostaria de participar!");
        });

        #endregion

        public ProjectViewModel(INavigation navigation, IProjectRepository projectRepository, IWhatsappService whatsappService, Guid project) : base(navigation)
        {
            _whatsappService = whatsappService;
            Project = projectRepository.Get(project);
        }
    }
}
