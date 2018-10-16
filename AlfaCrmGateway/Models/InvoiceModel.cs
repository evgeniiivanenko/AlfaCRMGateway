using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERIP.Sites.AlfaCrmGateway.Models
{
    public class InvoiceModel
    {
        /// <summary>
        /// String(32) 	API-ключ производителя услуг
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// String(30) 	Номер лицевого счета
        /// </summary>
        public string AccountNo { get; set; }

        /// <summary>
        /// Decimal(19,2) 	Сумма счета на оплату.
        /// Разделителем дробной и целой части является символ запятой
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Integer 	Код валюты
        /// </summary>
        public int Currency { get; set; }

        /// <summary>
        /// String(8) 	Дата истечения срока действия выставлена счета на оплату.
        /// Формат - yyyyMMdd
        /// </summary>
        public string Expiration { get; set; }

        /// <summary>
        /// String(1024) 	Назначение платежа
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// String(30) 	Фамилия
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// String(30) 	Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// String(30) 	Отчество
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// String(30) 	Город
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// String(30) 	Улица
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// String(10) 	Дом
        /// </summary>
        public string House { get; set; }

        /// <summary>
        /// String(10) 	Корпус
        /// </summary>
        public string Building { get; set; }

        /// <summary>
        /// String(10) 	Квартира
        /// </summary>
        public string Apartment { get; set; }

        /// <summary>
        /// Integer 	При оплате разрешено изменять ФИО плательщика
        /// 0 – нет, 1 – да
        /// </summary>
        public int IsNameEditable { get; set; }

        /// <summary>
        /// Integer 	При оплате разрешено изменять адрес плательщика
        /// 0 – нет, 1 – да
        /// </summary>
        public int IsAddressEditable { get; set; }

        /// <summary>
        /// Integer 	При оплате разрешено изменять сумму оплаты
        /// 0 – нет, 1 – да
        /// </summary>
        public int IsAmountEditable { get; set; }

        /// <summary>
        /// String(255) 	Адрес электронной почты, на который будет
        /// отправлено уведомление о выставлении счета
        /// </summary>
        public string EmailNotification { get; set; }

        /// <summary>
        /// String(13) 	Номер мобильного телефона, на который будет
        /// отправлено SMS-сообщение о выставлении счета
        /// </summary>
        public string SmsPhone { get; set; }

    }
}