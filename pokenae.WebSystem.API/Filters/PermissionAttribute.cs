using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using pokenae.WebSystem.Core.Entities;
using System;
using System.Security.Claims;

namespace pokenae.WebSystem.API.Filters
{
    /// <summary>
    /// �A�N�V�������\�b�h�ɑ΂��錠�����w�肷��A�m�e�[�V����
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PermissionAttribute : Attribute, IFilterMetadata
    {
        public PermissionLevel RequiredPermission { get; }

        public PermissionAttribute(PermissionLevel requiredPermission)
        {
            RequiredPermission = requiredPermission;
        }
    }
}
