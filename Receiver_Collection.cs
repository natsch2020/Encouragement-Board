using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Encouragement_Board
{
    [Serializable]
    class Receiver_Collection
    {
        [NonSerialized] private ObservableCollection<Receiver> receivers;
        private List<Receiver> receivers_list; //Used only for serialization

        public Receiver_Collection()
        {
            receivers = new ObservableCollection<Receiver>();
            receivers_list = new List<Receiver>();
        }

        public Receiver_Collection(List<Receiver> receivers) //Used to recreate the collection from a serialized copy
        {
            this.receivers = new ObservableCollection<Receiver>(receivers);
            receivers_list = receivers;
        }

        public ObservableCollection<Receiver> Receivers
        {
            get { return receivers; }
            set { receivers = value; }
        }

        public List<Receiver> Receivers_List
        {
            get { return receivers_list; }
            set { receivers_list = value; }
        }

        public void Add(Receiver receiver)
        {
            receivers.Add(receiver);
        }

        public void Remove(Receiver receiver)
        {
            receivers.Remove(receiver);
        }

        public void Serialize(string bin_file)
        {
            receivers_list = new List<Receiver>(receivers);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(bin_file, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, receivers_list);
            stream.Close();
        }

        public void Deserialize(string bin_file)
        {
            if (!(File.Exists(bin_file)))
            {
                return;
            }
            if (new FileInfo(bin_file).Length > 0)
            {
                return;
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(bin_file, FileMode.Open, FileAccess.Read, FileShare.Read);
            List<Receiver> r_list = (List<Receiver>)formatter.Deserialize(stream);
            stream.Close();

            receivers_list = r_list;
            receivers = new ObservableCollection<Receiver>(r_list);
        }

        public bool Contains(string fullname)
        {
            var matches = receivers.Where(p => string.Equals(p.Full_Name, fullname, StringComparison.CurrentCulture));

            if (matches.Any())
            {
                return true;
            }
            return false;
        }

        public Receiver? GetReceiver(string fullname)
        {
            var matches = receivers.Where(p => string.Equals(p.Full_Name, fullname, StringComparison.CurrentCulture));
            if (matches.Any())
            {
                return matches.First();
            }
            return null;
        }
    }
}
