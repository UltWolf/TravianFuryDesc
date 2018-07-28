﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravianFuryBoarClient.ViewModels;

namespace TravianFuryBoarClient.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginToolsView.xaml
    /// </summary>
    public partial class LoginToolsView : Window
    {
        public LoginToolsView()
        {
            InitializeComponent();
            this.DataContext = new LoginToolsViewModel(this);
        }
    }
}
