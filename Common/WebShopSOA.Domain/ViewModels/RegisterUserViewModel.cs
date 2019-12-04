using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopSOA.Domain.ViewModels
{
    public class RegisterUserViewModel
    {
        [RegularExpression(@"([A-Za-z][A-Za-z0-9]{2,255})", ErrorMessage = "Неверный формат имени пользователя")]
        [Display(Name = "Имя пользователя"), Required, MaxLength(255, ErrorMessage = "Максимальная длина 256 символов")]
        [Remote("IsNameFree", "Account", ErrorMessage = "Имя пользователя уже занято")]
        public string UserName { get; set; }

        [Display(Name = "электронная почта"), Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Введите корректный e-mail адрес")]
        public string Email { get; set; }

        [Display(Name = "Пароль"), Required,DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Подтверждение пароля"), Required, DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
