using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace WpfApparent
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        private static NotifyIcon trayIcon;

        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            RemoveTrayIcon();
            AddTrayIcon();
        }

        private void AddTrayIcon()
        {
            if (trayIcon != null)
            {
                return;
            }
            trayIcon = new NotifyIcon
            {
                Icon = System.Drawing.SystemIcons.WinLogo,
                Text = "NotifyIconStd"
            };
            trayIcon.Visible = true;

            ContextMenu menu = new ContextMenu();

            MenuItem refreshItem = new MenuItem();
            refreshItem.Text = "Refresh";
            refreshItem.Click += refreshItem_Click;


            MenuItem closeItem = new MenuItem();
            closeItem.Text = "Close";
            closeItem.Click += new EventHandler(delegate { this.Shutdown(); });

            MenuItem helpItem = new MenuItem();
            helpItem.Text = "Help";
            helpItem.Click += helpItem_Click;

            menu.MenuItems.Add(refreshItem);
            menu.MenuItems.Add(closeItem);
            menu.MenuItems.Add(helpItem);

            trayIcon.ContextMenu = menu;    //设置NotifyIcon的右键弹出菜单
        }

        private void refreshItem_Click(object sender, EventArgs e)
        {
            MainWindow.Close();
            new MainWindow().Show();
        }


        private void helpItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("F3：降低亮度\nF4：提高亮度\nAlt+R/G/B/T/H/N：增加/减少叠加在屏幕上的颜色\n注意：本程序不能额外提高屏幕亮度！", "帮助");
        }


        private void RemoveTrayIcon()
        {
            if (trayIcon != null)
            {
                trayIcon.Visible = false;
                trayIcon.Dispose();
                trayIcon = null;
            }
        }

        private void ApplicationExit(object sender, ExitEventArgs e)
        {
            RemoveTrayIcon();
        }
    }
}
