﻿using Beauty.WPF.Commands;
using Beauty.WPF.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Beauty.WPF.Infrastructure;
using System.Collections.ObjectModel;
using Beauty.Data.Models;
using Beauty.Data.Interfaces;
using Beauty.Data.UnitOfWorks;
using Beauty.Core.Interfaces;
using Beauty.Core.Services;
using Beauty.Core.Infrastructure;
using Beauty.WPF.Interfaces;
using Beauty.Core.Extensions;

namespace Beauty.WPF.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoginService loginService;

        private ICollection<Worker> workers;
        private Worker selectedWorker;

        public ICollection<Worker> Workers
        {
            get
            {
                return workers;
            }

            set
            {
                workers = value;
                OnPropertyChanged(nameof(Workers));
            }
        }

        public Worker SelectedWorker
        {
            get
            {
                return selectedWorker;
            }

            set
            {
                selectedWorker = value;
                OnPropertyChanged(nameof(SelectedWorker));
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            unitOfWork = new UnitOfWork();
            loginService = new LoginService(unitOfWork);

            LoginCommand = new ParameterizedCommand
            (
                LoginAsync,
                (parameter) =>
                {
                    var collectionSettings = parameter as ICollectionable;
                    var securitySettings = parameter as ISecurable;

                    if (collectionSettings is null && securitySettings is null)
                    {
                        return false;
                    }

                    return collectionSettings.HasItems && !securitySettings.IsPasswordEmpty;
                }
            );

            Task.Factory.StartNew(LoadPropertiesAsync);
        }

        public async void LoadPropertiesAsync()
        {
            var workers = await unitOfWork.Workers.FindAdministratorsAsync();
            Workers = new ObservableCollection<Worker>(workers);
        }

        public async void LoginAsync(object parameter)
        {
            var view = parameter as IView;
            var securitySettings = parameter as ISecurable;

            if (view is null && securitySettings is null)
            {
                return;
            }

            var password = securitySettings.SecurePassword.Unsecure();
            var loginResult = await loginService.LoginAsync(SelectedWorker, password);

            if (!loginResult.IsFailed)
            {
                Controller.Application.LoginDetails = loginResult as LoginDetails;
                Controller.Application.GoToView(ApplicationViews.MainView);
            }
            else
            {
                Controller.MessageService.ShowError("Вы ввели неверный пароль. Пожалуйста, повторите попытку ввода");
            }
        }
    }
}