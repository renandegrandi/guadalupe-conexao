﻿using Guadalupe.Conexao.Api.Core;
using Guadalupe.Conexao.Api.Domain;
using System;

namespace Guadalupe.Conexao.Api.Models.V1
{
    public class NoticeDto : IDto
    {
        public Guid Guid { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public DateTime Posted { get; set; }
        public Guid IdPostedBy { get; set; }
        public UserDto PostedBy { get; set; }
        public UserNoticeState State { get; set; }
    }
}