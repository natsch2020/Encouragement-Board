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
    class Encourager_Collection
    {
        [NonSerialized] private ObservableCollection<Encourager> encouragers;
        private List<Encourager> encouragers_list;

        public Encourager_Collection()
        {
            encouragers = new ObservableCollection<Encourager>();
            encouragers_list = new List<Encourager>();
        }

        public Encourager_Collection(List<Encourager> encouragers)
        {
            this.encouragers = new ObservableCollection<Encourager>(encouragers);
            encouragers_list = encouragers;
        }

        public ObservableCollection<Encourager> Encouragers
        {
            get { return encouragers; }
            set { encouragers = value; }
        }

        public List<Encourager> Encouragers_List
        {
            get { return encouragers_list; }
            set { encouragers_list = value; }
        }

        public void Add(Encourager encourager)
        {
            encouragers.Add(encourager);
        }

        public void Remove(Encourager encourager)
        {
            encouragers.Remove(encourager);
        }

        public void Serialize(string bin_file)
        {
            encouragers_list = new List<Encourager>(encouragers);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(bin_file, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, encouragers_list);
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
            List<Encourager> p_list = (List<Encourager>)formatter.Deserialize(stream);
            stream.Close();

            encouragers_list = p_list;
            encouragers = new ObservableCollection<Encourager>(encouragers_list);
        }

        public bool Contains(string fullname)
        {
            var matches = encouragers.Where(p => string.Equals(p.Full_Name, fullname, StringComparison.CurrentCulture));

            if (matches.Any())
            {
                return true;
            }
            return false;
        }

        public Encourager? GetEncourager(string fullname)
        {
            var matches = encouragers.Where(p => string.Equals(p.Full_Name, fullname, StringComparison.CurrentCulture));
            if (matches.Any())
            {
                return matches.First();
            }
            return null;
        }
    }
}
