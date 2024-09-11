using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Encouragement_Board
{
    public partial class MainWindow : Window
    {
        Receiver_Collection rc;
        Encourager_Collection ec;

        public MainWindow()
        {
            InitializeComponent();

            rc = new Receiver_Collection();
            string r_filename = @"../../Receivers.bin";
            try
            {
                rc.Deserialize(r_filename);
            }
            catch
            {
                rc.Receivers = new ObservableCollection<Receiver>();
            }

            ec = new Encourager_Collection();
            string e_filename = @"../../Encouragers.bin";
            try
            {
                ec.Deserialize(e_filename);
            }
            catch
            {
                ec.Encouragers = new ObservableCollection<Encourager>();
            }

            Encouragement_Table.ItemsSource = rc.Receivers;
        }

        private void Add_ETime_Button_Click(object sender, RoutedEventArgs e)
        {
            string rec_fullname = Receiver_Textbox.Text;
            DateTime? date = Date_Picker.SelectedDate;
            string enc_fullname = Encourager_Textbox.Text;

            if (rec_fullname == "")
            {
                Receiver_Textbox.Background = Brushes.Red;
                return;
            }
            Receiver_Textbox.Background = Brushes.Transparent;

            if (date == null)
            {
                Date_Picker.Background = Brushes.Red;
                return;
            }
            Date_Picker.Background = Brushes.Transparent;

            if (enc_fullname == "")
            {
                Encourager_Textbox.Background = Brushes.Red;
                return;
            }
            Encourager_Textbox.Background = Brushes.Transparent;

            Encourager? encourager;
            if(ec.Contains(enc_fullname))
            {
                encourager = ec.GetEncourager(enc_fullname);
            }
            else
            {
                encourager = new Encourager(enc_fullname, 0, date);
                ec.Add(encourager);
            }

            Receiver? receiver;
            if(rc.Contains(rec_fullname))
            {
                receiver = rc.GetReceiver(rec_fullname);
                receiver.Add_ET(date, encourager);
            }
            else
            {
                receiver = new Receiver(rec_fullname, 1, date, encourager);
                rc.Add(receiver);
            }

            encourager.Add_ET(date);

            Encouragement_Table.Items.Refresh();

            rc.Serialize(@"../../Receivers.bin");
            ec.Serialize(@"../../Encouragers.bin");
        }

        private void Delete_ETime_Button_Click(object sender, RoutedEventArgs e)
        {
            string rec_fullname = Receiver_Textbox.Text;
            DateTime? date = Date_Picker.SelectedDate;
            string enc_fullname = Encourager_Textbox.Text;

            if (rec_fullname == "")
            {
                Receiver_Textbox.Background = Brushes.Red;
                return;
            }
            Receiver_Textbox.Background = Brushes.Transparent;

            if (date == null)
            {
                Date_Picker.Background = Brushes.Red;
                return;
            }
            Date_Picker.Background = Brushes.Transparent;

            if (enc_fullname == "")
            {
                Encourager_Textbox.Background = Brushes.Red;
                return;
            }
            Encourager_Textbox.Background = Brushes.Transparent;

            Receiver? receiver;
            if (rc.Contains(rec_fullname))
            {
                receiver = rc.GetReceiver(rec_fullname);
            }
            else
            {
                Receiver_Textbox.Background = Brushes.Red;
                return;
            }

            Encourager? encourager;
            if (ec.Contains(enc_fullname))
            {
                encourager = ec.GetEncourager(enc_fullname);
            }
            else
            {
                Encourager_Textbox.Background = Brushes.Red;
                return;
            }

            receiver.Delete_ET(date);

            encourager.Delete_ET(date);

            Encouragement_Table.Items.Refresh();

            rc.Serialize(@"../../Receivers.bin");
            ec.Serialize(@"../../Encouragers.bin");
        }

        private void Encouragement_Table_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();

            if (headername == "Dates")
            {
                e.Cancel = true;
            }

            if(headername == "Number_Received")
            {
                e.Column.Header = "Number Received";
                e.Column.DisplayIndex = 3;
            }

            if(headername == "Last_Encourager")
            {
                e.Column.Header = "Last Encourager";
                e.Column.DisplayIndex = 2;
            }

            if(headername == "Full_Name")
            {
                e.Column.Header = "Name";
            }
        }

        private void Remove_R_Button_Click(object sender, RoutedEventArgs e)
        {
            string rec_fullname;
            if (Encouragement_Table.SelectedCells.Count != 0)
            {
                rec_fullname = Encouragement_Table.SelectedCells[0].Item.ToString();
            }
            else
            {
                return;
            }

            Receiver? receiver;
            if (rc.Contains(rec_fullname))
            {
                receiver = rc.GetReceiver(rec_fullname);
            }
            else
            {
                return;
            }

            rc.Remove(receiver);

            rc.Serialize(@"../../Receivers.bin");
        }

        private void Clear_Counts_Button_Click(object sender, RoutedEventArgs e)
        {
            if(Encouragement_Table.Items.Count == 0)
            {
                return;
            }
            foreach(Receiver rec in rc.Receivers){
                rec.Number_Received = 0;
            }

            Encouragement_Table.Items.Refresh();

            rc.Serialize(@"../../Receivers.bin");
        }
    }
}