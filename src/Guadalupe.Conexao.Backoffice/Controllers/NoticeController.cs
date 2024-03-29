﻿using Guadalupe.Conexao.Backoffice.Extension;
using Guadalupe.Conexao.Backoffice.Models;
using Guadalupe.Conexao.Backoffice.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Backoffice.Controllers
{
    public class NoticeController : Controller
    {
        public static readonly string[] MimetypesAllowed = new string[] { "image/jpeg", "image/png", "image/webp"};
        public static readonly string[] MimetypesToConvert = new string[] { "image/jpeg", "image/png" };

        private readonly ILogger<NoticeController> _logger;
        private readonly INoticeRepository _noticeRepository;

        //TODO: é necessário tratar os erros da API.

        #region Constructor

        public NoticeController(INoticeRepository noticeRepository,
            ILogger<NoticeController> logger)
        {
            _noticeRepository = noticeRepository;
            _logger = logger;
        }

        #endregion

        public async Task<ActionResult> Index(string search, int index = 1, CancellationToken cancellationToken = default)
        {
            var result = new Core.PaginatorViewModel<NoticeViewModel>();

            try
            {
                result = await _noticeRepository.GetPaginatedAsync(search, index, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return View(result);
        }

        public async Task<ActionResult> Detail(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _noticeRepository.GetByIdAsync(id, cancellationToken);

                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return RedirectToAction(nameof(Index));
            }
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

                var file = form.ImageFile.OpenReadStream();

                MemoryStream fileMemoryStream = null;

                if (MimetypesToConvert.Contains(form.ImageFile.ContentType))
                    fileMemoryStream = file.ToWebpMemoryStream();
                else
                    fileMemoryStream = await file.ToMemoryStreamAsync()
                        .ConfigureAwait(false);

                var filename = $"Images/Notice/{Guid.NewGuid()}.webp";

                await _noticeRepository.FileUploadAsync(filename, fileMemoryStream, "image/webp", cancellationToken)
                    .ConfigureAwait(false);

                await fileMemoryStream.DisposeAsync()
                    .ConfigureAwait(false);

                form.Image = filename;

                await _noticeRepository.AddAsync(form, cancellationToken)
                    .ConfigureAwait(false);

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
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return View();
            }
        }
    }
}
