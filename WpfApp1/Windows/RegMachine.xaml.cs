using System;
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
using WpfApp.entities;

namespace WpfApp.Windows
{
    /// <summary>
    /// RegMachine.xaml 的交互逻辑
    /// </summary>
    public partial class RegMachine : Window
    {
        public RegMachine()
        {
            InitializeComponent();
            txt_mccode.Text = Tools.McCode;
            dp.SelectedDate = DateTime.Now.AddDays(7);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var end_date = dp.SelectedDate ?? DateTime.Now;
            txt_code.Text = new SymmetryEncrypt().Encrypto(end_date.AddDays(1).Ticks.ToString().PadLeft(Tools.LEN) + txt_mccode.Text);
        }
    }
}
