using Guadalupe.Conexao.Backoffice.Models;
using Guadalupe.Conexao.Backoffice.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Backoffice.Controllers
{
    public class NoticeController : Controller
    {
        #region Dependencies

        private readonly INoticeRepository _noticeRepository;

        #endregion

        #region Constructor

        public NoticeController(INoticeRepository noticeRepository)
        {
            _noticeRepository = noticeRepository;
        }

        #endregion

        public async Task<ActionResult> Index(string search, int index = 1, CancellationToken cancellationToken = default)
        {
            var result = await _noticeRepository.GetPaginatedAsync(search, index, cancellationToken);

            return View(result);
        }


        public async Task<ActionResult> Detail(Guid id, CancellationToken cancellationToken)
        {
            var result = await _noticeRepository.GetByIdAsync(id, cancellationToken);

            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NoticeViewModel form, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(form);

                await _noticeRepository.AddAsync(form, cancellationToken);

                //Adicionar notificação que foi add com sucesso.

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _noticeRepository.DeleteAsync(id, cancellationToken);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
