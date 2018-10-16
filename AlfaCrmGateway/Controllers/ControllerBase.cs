using System;
using System.Web.Mvc;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Services;
using ERIP.Sites.AlfaCrmGateway.Models;
using TRIINKOM.Infrastructure;

namespace ERIP.Sites.AlfaCrmGateway.Controllers
{
    public class ControllerBase : Controller
    {
        protected const string MessageSaveSuccess = @"Данные успешно сохранены";
        protected const string MessageAddSuccess = @"Данные успешно добавлены";
        protected const string MessageSaveFail = @"Проверьте правильность заполнения формы";
        protected const string StatusMessageSuccess = @"StatusMessageSuccess";
        protected const string StatusMessageFail = @"StatusMessageFail";
        protected IAppController AppController { get; private set; }
        //protected IAuthService AuthService { get; private set; }


        public ControllerBase(IAppController appController)
        {
            AppController = appController;

            if (appController == null)
                return;
            /*AuthService = appController.Get<IAuthService>();

            ViewBag.Login = (AuthService.CurrentUser != null) ? true : false;
            ViewBag.AuthService = AuthService;

            if (ViewBag.Login)
            {
                ViewBag.ClientFullName = String.Concat(AuthService.CurrentUser.Nickname);
            }*/
        }

        protected JsonResult ModelStateErrorJson()
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelValue in ModelState.Values)
                {
                    foreach (var error in modelValue.Errors)
                    {
                        return ErrorJson(ErrorCode.BadRequest, error.ErrorMessage);
                    }
                }
            }

            throw new InvalidOperationException("ModelState is valid");
        }

        protected JsonResult ErrorJson(ErrorCode errorCode, string errorMsg = null)
        {
            return ErrorJson(errorCode.ToString(), errorMsg);
        }

        protected JsonResult ErrorJson(string errorCode, string errorMsg = null)
        {
            switch (errorCode)
            {
                // =========================================================================================================
                // 400 - Bad Request (400XXXX, где XXXX - коды ошибок с 0001 по 9999)
                // =========================================================================================================
                case "BadRequest":
                    return Json(new BadRequestResultModel("Некорректный запрос",
                        4000003, errorMsg), JsonRequestBehavior.AllowGet);


                case "BadMultiplicity":
                    return Json(new BadRequestResultModel("Некорректный запрос",
                        4000004, errorMsg), JsonRequestBehavior.AllowGet);

                // =========================================================================================================
                // 404 - Not Found (404XXXX, где XXXX - коды ошибок с 0001 по 9999)
                // =========================================================================================================
                case "NotFoundPayment": // (404XXXX, где XXXX - коды ошибок с 0001 по 1000)
                    return Json(new NotFoundResultModel("Платеж не найден", 4040001, errorMsg), JsonRequestBehavior.AllowGet);

                case "NotFoundInvoice": // (404XXXX, где XXXX - коды ошибок с 1001 по 2000)
                    return Json(new NotFoundResultModel("Счет на оплату не найден", 4041002, errorMsg), JsonRequestBehavior.AllowGet);

                // =========================================================================================================
                // 500 - Internal Server Error (500XXXX, где XXXX - коды ошибок с 0001 по 9999)
                // =========================================================================================================
                default: // (5000000 - внутренняя ошибка по умолчанию)
                    return Json(new InternalServerErrorResultModel(errorCode, 5000000, errorMsg), JsonRequestBehavior.AllowGet);
            }
        }
    }
}