using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace BadUIPhonePicker.ViewModels
{
    class MainViewModel : BindableBase
    {
        private readonly Random _random = new Random();

        private long _phoneNumber;
        private int _modifier;
        private int? _operatorIndex;

        public long PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        public int Modifier
        {
            get => _modifier;
            set => SetProperty(ref _modifier, value);
        }

        public int? OperatorIndex
        {
            get => _operatorIndex;
            set => SetProperty(ref _operatorIndex, value);
        }


        private DelegateCommand _loadedCommand;
        private DelegateCommand _selectionChangedCommand;

        public ICommand LoadedCommand =>
            _loadedCommand ??
            (_loadedCommand = new DelegateCommand(LoadedExecute));

        public ICommand OperatorChangedCommand =>
            _selectionChangedCommand ??
            (_selectionChangedCommand = new DelegateCommand(OperatorChangedExecute));


        private void LoadedExecute()
        {
            Modifier = _random.Next(1, 20);
        }
        private void OperatorChangedExecute()
        {
            PerformCalculations();
        }

        private void PerformCalculations()
        {
            if (OperatorIndex == -1)
                return;

            PhoneNumber = OperatorIndex switch
            {
                0 => PhoneNumber + Modifier,
                1 => PhoneNumber - Modifier,
                2 => PhoneNumber * Modifier,
                3 => PhoneNumber / Modifier,
                4 => (long)Math.Round(Math.Pow(PhoneNumber, Modifier)),
                5 => (long)Math.Round(Math.Pow(PhoneNumber, 1 / Modifier)),
                6 => (long)Math.Round(Math.Log(PhoneNumber, Modifier)),
                7 => PhoneNumber & Modifier,
#pragma warning disable CS0675 // Bitwise-or operator used on a sign-extended operand - supressed because who cares here
                8 => PhoneNumber | Modifier,
#pragma warning restore CS0675 // Bitwise-or operator used on a sign-extended operand
                9 => PhoneNumber ^ Modifier,
                10 => PhoneNumber << Modifier,
                11 => PhoneNumber >> Modifier,
                12 => Factorial(PhoneNumber),
                13 => PhoneNumber == long.MinValue ? long.MaxValue : Math.Abs(PhoneNumber),
                _ => PhoneNumber
            };

            Modifier = _random.Next(1, 20);
            OperatorIndex = -1;
        }

        private long Factorial(long f)
        {
            if (f == 0)
                return 1;
            else
                return f * Factorial(f - 1);
        }
    }
}
